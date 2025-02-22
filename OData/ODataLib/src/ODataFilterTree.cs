﻿using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;

namespace DotNetExtras.OData;
/// <summary>
/// Implements a binary tree that holds the OData filter expression.
/// </summary>
/// <typeparam name="T">
/// Data type of the primary object used as a model for the OData filter expression.
/// </typeparam>
/// <remarks>
/// Adapted from <see href="https://devblogs.microsoft.com/odata/tutorial-sample-using-odatauriparser-for-odata-v4/"/>.
/// See also <see href="https://stackoverflow.com/questions/69317761/how-use-odata-filters-with-swagger-in-an-asp-net-core-webapi-projec"/>
/// (could not make it work with dependency injection).
/// </remarks>
public class ODataFilterTree<T>
{
    /// <summary>
    /// Text of the OData filter expression.
    /// </summary>
    public string Expression { get; private set; }

    /// <summary>
    /// Root node of the expression tree.
    /// </summary>
    public ODataFilterNode Root { get; set; }

    /// <summary>
    /// Elements (data types) that are or may be reference by the OData filter expression.
    /// </summary>
    public List<IEdmSchemaElement> SchemaElements { get; private set; }

    /// <summary>
    /// List of all operators used in the OData filter expression.
    /// </summary>
    /// <remarks>
    /// May contain duplicates.
    /// </remarks>
    public List<string> Operators { get; private set; } = [];

    /// <summary>
    /// List of all properties used in the OData filter expression.
    /// </summary>
    /// <remarks>
    /// May contain duplicates.
    /// </remarks>
    public List<string> Properties { get; private set; } = [];

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="expression">
    /// Text of the OData filter expression.
    /// </param>
    public ODataFilterTree
    (
        string expression
    )
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(expression);

        Expression = expression;

        // The builder is responsible for mapping OData filter conditions to the object properties.
        ODataConventionModelBuilder builder = new();

        // UserFilter defines the properties which can be used in our filter (this class is defined below).
        // The UserFilter class properties also can be based on other complex types, but they will be
        // included implicitly, so no need to reference them.
        builder.AddEntityType(typeof(T));

        // EDM (Entity Data Model) encapsulates classes that will be used in the OData filters.
        IEdmModel model = builder.GetEdmModel();

        // Save all classes that our EDM schema recognizes.
        if (model == null || model.SchemaElements == null || !model.SchemaElements.Any())
        {
            throw new Exception("Cannot get EDM model or schema elements.");
        }

        SchemaElements = [.. model.SchemaElements];

        // If you define the EDM classes not under an explicitly defined namespace,
        // make sure you add 'Default.' to the full name because 'Default' would be the implicit
        // namespace created by the compiler; if you do not, these types will not be found.
        // string qualifiedName = "Default." + typeof(GroupFilter).FullName;
        string qualifiedName = typeof(T).FullName ?? "";

        // Now, lets use our filter class as the EDM type, so it can be used for OData filter handling.
        IEdmType type = model.FindDeclaredType(qualifiedName) ?? 
            throw new Exception($"Cannot find type '{qualifiedName}' in the OData schema model holding elements: " +
                string.Join(", ", SchemaElements.Select(e => e.FullName())) + ".");

        // This dictionary can include other OData parameters,
        // such as "$top", "$skip", "$count", "$select", "$orderby", "$search", etc.
        // but we are only interested in the filter.
        Dictionary<string, string> options = new()
        {
            {"$filter", expression}
        };

        try
        {
            ODataQueryOptionParser parser = new(model, type, null, options)
            {
                // By default, property names are case-sensitive,
                // so we need to explicitly specify them to be case-insensitive.
                Resolver = new ODataUriResolver() { EnableCaseInsensitive = true }
            };

            FilterClause clause = parser.ParseFilter();

            if (clause == null)
            {
                throw new Exception("Parsed filter clause is null.");
            }
            else if (clause.Expression == null)
            {
                throw new Exception("Parsed filter clause expression is null.");
            }
            else
            {
                try
                {
                    Root = new(clause.Expression, null, null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot initialize root node of the expression tree.", ex);
                }

                Operators   = GetOperators();
                Properties  = GetProperties();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Cannot parse filter expression.", ex);
        }
    }

    /// <summary>
    /// Prints operators and properties of the parsed OData filter expression tree.
    /// </summary>
    public void PrintDetails()
    {
        Console.WriteLine();
        Console.WriteLine("OPERATORS : " + string.Join(",", 
            Operators.Distinct().ToList().OrderBy(s => s, StringComparer.OrdinalIgnoreCase).ToList()));

        if (Properties == null || Properties.Count == 0) 
        {
            return;
        }

        Console.WriteLine("PROPERTIES: " + string.Join(",", 
            Properties.Distinct().ToList().OrderBy(s => s, StringComparer.OrdinalIgnoreCase).ToList()));
    }

    /// <summary>
    /// Prints schema elements (data types) that are or may be reference by the OData filter expression.
    /// </summary>
    public void PrintSchema()
    {
        Console.WriteLine("ODATA SCHEMA ELEMENTS:");
        foreach (IEdmSchemaElement element in SchemaElements)
        {
            Console.WriteLine($"- {element.FullName()}: {element.SchemaElementKind}");
        }
    }

    /// <summary>
    /// Prints parsed filter expression tree.
    /// </summary>
    /// <param name="withLabels">
    /// Specifies whether to print node labels.
    /// </param>
    /// <param name="withNodeKind">
    /// Specifies whether to print node types.
    /// </param>
    /// <param name="withDetails">
    /// Specifies whether to print operators and properties of the parsed OData filter expression tree..
    /// </param>
    /// <param name="lineLength">
    /// Specifies line length for the printed horizontal lines.
    /// </param>
    public void PrintTree
    (
        bool withLabels = false,
        bool withNodeKind = false,
        bool withDetails = false,
        int lineLength = 72
    )
    {
        Console.WriteLine(new string('-', lineLength));
        Console.WriteLine(withLabels ? "EXPRESSION: " + Expression : Expression);
        Console.WriteLine(new string('-', lineLength));

        Root.Print(withLabels, withNodeKind);

        if (withDetails)
        {
            PrintDetails();
        }
    }

    /// <summary>
    /// Prints tree to console.
    /// </summary>
    /// <param name="withSchema">
    /// Specifies whether to print schema elements (data types) that are or may be reference by the OData filter expression. 
    /// </param>
    /// <param name="withLabels">
    /// Specifies whether to print node labels.
    /// </param>
    /// <param name="withNodeKind">
    /// Specifies whether to print node types.
    /// </param>
    public void Print
    (
        bool withSchema =  false,
        bool withLabels = false,
        bool withNodeKind = false
    )
    {
        if (withSchema)
        {
            PrintSchema();
            Console.WriteLine();
        }

        PrintTree(withLabels, withNodeKind);
    }

    /// <summary>
    /// Gets the names of the operators in the tree.
    /// </summary>
    /// <returns>
    /// Names of the operators in the tree.
    /// </returns>
    private List<string> GetOperators()
    {
        List<string> operatorNames = [];
        ODataFilterTree<T>.GetOperators(Root, operatorNames);
        return operatorNames;
    }

    /// <summary>
    /// Recursively gets the names of the operators in the specified node and its subtrees.
    /// </summary>
    /// <param name="node">
    /// Starting node.
    /// </param>
    /// <param name="names">
    /// Names of the operators in the node and its subtree.
    /// </param>
    private static void GetOperators
    (
        ODataFilterNode? node, 
        List<string> names
    )
    {
        if (node == null)
        {
            return;
        }

        if (node.IsOperator && !string.IsNullOrEmpty(node.OperatorName))
        {
            names.Add(node.OperatorName);
        }

        ODataFilterTree<T>.GetOperators(node.Left, names);
        ODataFilterTree<T>.GetOperators(node.Right, names);
    }

    /// <summary>
    /// Gets the names of the properties in the tree.
    /// </summary>
    /// <returns>
    /// Names of the properties in the tree.
    /// </returns>
    private List<string> GetProperties()
    {
        List<string> propertyNames = [];
        GetProperties(Root, propertyNames);
        return propertyNames;
    }

    /// <summary>
    /// Recursively gets the names of the properties in the specified node and its subtrees.
    /// </summary>
    /// <param name="node">
    /// Starting node.
    /// </param>
    /// <param name="names">
    /// Names of the properties in the node and its subtree.
    /// </param>
    private static void GetProperties
    (
        ODataFilterNode? node, 
        List<string> names
    )
    {
        if (node == null)
        {
            return;
        }

        if (node.IsOperand && node.Property != null && !string.IsNullOrEmpty(node.Property))
        {
            names.Add(node.Property);
        }

        GetProperties(node.Left, names);
        GetProperties(node.Right, names);
    }
}
