using CommonLibTests.Models;
using DotNetExtras.Common;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Object_GetPropertyValue()
    {
        string id = "1234567890";
        string givenName = "John";
        string surname = "Doe";
        string managerSponsorSurname = "Smith";
        DateTime expirationDate = new(2023, 10, 1);
        DateTimeOffset expirationOffset = new(new DateTime(2023, 10, 1), TimeSpan.FromMinutes(90));

        Employee employee = new() 
        { 
            Id = id,
            Name = new Name { GivenName = givenName, Surname = surname },
            ExpirationDate = expirationDate,
            ExpirationOffset = expirationOffset,
            Manager = new()
            {
                Sponsor = new()
                {
                    Name = new()
                    {
                        Surname = managerSponsorSurname
                    }
                }
            }
        };

        Assert.Equal(id, employee.GetPropertyValue(NameOf.Long(nameof(Employee.Id))));
        Assert.Equal(expirationDate, employee.GetPropertyValue(NameOf.Long(nameof(Employee.ExpirationDate))));
        Assert.Equal(expirationOffset, employee.GetPropertyValue(NameOf.Long(nameof(Employee.ExpirationOffset))));
        Assert.Equal(givenName, employee.GetPropertyValue(NameOf.Long(nameof(Employee.Name.GivenName))));
        Assert.Equal(surname, employee.GetPropertyValue(NameOf.Long(nameof(Employee.Name.Surname))));
        Assert.Equal(managerSponsorSurname, employee.GetPropertyValue(NameOf.Long(nameof(Employee.Manager.Sponsor.Name.Surname))));
    }

    [Fact]
    public void Object_SetPropertyValue()
    {
        string id = "1234567890";
        string givenName = "John";
        string surname = "Doe";
        string managerSponsorSurname = "Smith";
        DateTime expirationDate = new(2023, 10, 1);
        DateTimeOffset expirationOffset = new(new DateTime(2023, 10, 1), TimeSpan.FromMinutes(90));

        Employee employee = new();

        employee.SetPropertyValue(NameOf.Long(nameof(Employee.Id)), id);
        employee.SetPropertyValue(NameOf.Long(nameof(Employee.ExpirationDate)), expirationDate);
        employee.SetPropertyValue(NameOf.Long(nameof(Employee.ExpirationOffset)), expirationOffset);
        employee.SetPropertyValue(NameOf.Long(nameof(Employee.Name.GivenName)), givenName);
        employee.SetPropertyValue(NameOf.Long(nameof(Employee.Name.Surname)), surname);
        employee.SetPropertyValue(NameOf.Long(nameof(Employee.Manager.Sponsor.Name.Surname)), managerSponsorSurname);

        Assert.Equal(id, employee.Id);
        Assert.Equal(expirationDate, employee.ExpirationDate);
        Assert.Equal(expirationOffset, employee.ExpirationOffset);
        Assert.Equal(givenName, employee.Name?.GivenName);
        Assert.Equal(surname, employee.Name?.Surname);
        Assert.Equal(managerSponsorSurname, employee.Manager?.Sponsor?.Name?.Surname);
    }
}
