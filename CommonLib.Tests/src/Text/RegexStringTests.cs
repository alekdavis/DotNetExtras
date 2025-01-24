using DotNetExtras.Text;

namespace CommonLibTests.Text;
public class RegexStringTests
{
    [Fact]
    public void Guid()
    {
        Assert.Matches(RegexString.Guid, "231CE3F7-9640-45AE-8C6F-06D329911DD9");
        Assert.Matches(RegexString.Guid, "231ce3f7-9640-45ae-8c6f-06d329911dd9");
        Assert.Matches(RegexString.Guid, "231CE3F7964045AE8C6F06D329911DD9");
        Assert.Matches(RegexString.Guid, "231ce3f7964045ae8c6f06d329911dd9");

        Assert.Matches(RegexString.Guid, "{231CE3F7-9640-45AE-8C6F-06D329911DD9}");
        Assert.Matches(RegexString.Guid, "{231ce3f7-9640-45ae-8c6f-06d329911dd9}");
        Assert.Matches(RegexString.Guid, "{231CE3F7964045AE8C6F06D329911DD9}");
        Assert.Matches(RegexString.Guid, "{231ce3f7964045ae8c6f06d329911dd9}");

        Assert.Matches(RegexString.Guid, "(231CE3F7-9640-45AE-8C6F-06D329911DD9)");
        Assert.Matches(RegexString.Guid, "(231ce3f7-9640-45ae-8c6f-06d329911dd9)");
        Assert.Matches(RegexString.Guid, "(231CE3F7964045AE8C6F06D329911DD9)");
        Assert.Matches(RegexString.Guid, "(231ce3f7964045ae8c6f06d329911dd9)");

        Assert.DoesNotMatch(RegexString.Guid, "231CE3F7-9640-45AE-8C6F-06D329911DD90");
        Assert.DoesNotMatch(RegexString.Guid, "12345678:1234-1234-1234-123456789012");
        Assert.DoesNotMatch(RegexString.Guid, "G31CE3F7-9640-45AE-8C6F-06D329911DD9");
    }

    [Fact]
    public void EmailAddress()
    {
        Assert.Matches(RegexString.EmailAddress, "joe.doe@mail.com");
        Assert.Matches(RegexString.EmailAddress, "joe_doe@mail.com");
        Assert.Matches(RegexString.EmailAddress, "joe-doe@mail.com");
        Assert.Matches(RegexString.EmailAddress, "joe.doe123@mail.com");
        Assert.Matches(RegexString.EmailAddress, "joe.doe@mail.co.uk");
        Assert.Matches(RegexString.EmailAddress, "joe.doe@mail-domain.com");
        Assert.Matches(RegexString.EmailAddress, "joe.doe+alias@mail.com");
        Assert.Matches(RegexString.EmailAddress, "joe.doe@sub.mail.com");
        Assert.Matches(RegexString.EmailAddress, "joe.doe@123.123.123.123");
        Assert.Matches(RegexString.EmailAddress, "joe.doe@sub.mail-domain.com");

        Assert.DoesNotMatch(RegexString.EmailAddress, "joe.doe@mail");
        Assert.DoesNotMatch(RegexString.EmailAddress, "joe.doe@mail..com");
        Assert.DoesNotMatch(RegexString.EmailAddress, "joe.doe@.mail.com");
        Assert.DoesNotMatch(RegexString.EmailAddress, "joe.doe@mail,com");
        Assert.DoesNotMatch(RegexString.EmailAddress, "joe.doe@mail@domain.com");
        Assert.DoesNotMatch(RegexString.EmailAddress, "joe.doe@mail_domain.com");
    }
}
