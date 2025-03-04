using DotNetExtras.Common.Exceptions;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Exception_GetMessages()
    {
        Exception inner1 = new("Inner exception 1");
        Exception inner2 = new("Inner exception 2");

        AggregateException ae = new("Aggregate exception", inner1, inner2);
        Exception ex = new("Outer exception", ae);

        string messages = ex.GetMessages<Exception>();

        Assert.Equal("Outer exception. Aggregate exception (Inner exception 1) (Inner exception 2) Inner exception 1.", messages);

        ex = new("OUTER EXCEPTION", new Exception("INNER EXCEPTION 1", new Exception("INNER EXCEPTION 2")));

        messages = ex.GetMessages();

        Assert.Equal("OUTER EXCEPTION. INNER EXCEPTION 1. INNER EXCEPTION 2.", messages);

        string errMsg = " This is \t an error  \r\n\rmessage\r    with   irregular \n spacing \r";
        ex = new Exception(errMsg);

        messages = ex.GetMessages();

        Assert.Equal("This is an error message with irregular spacing.", messages);
    }

    [Fact]
    public void Exception_GetSafeMessages()
    {
        SafeException ex = new("Safe Outer Exception", new SafeException("Safe Inner Exception", new Exception("Unsafe Exception")));

        string messages1 = ex.GetSafeMessages();
        string messages2 = ex.GetMessages<SafeException>();

        Assert.Equal("Safe Outer Exception. Safe Inner Exception.", messages1);
        Assert.Equal(messages2, messages1);
    }
}
