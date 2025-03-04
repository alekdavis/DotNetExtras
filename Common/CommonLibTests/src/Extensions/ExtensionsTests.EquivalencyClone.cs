using CommonLibTests.Models;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Object_IsEquivalentTo_Boolean()
    {
        Assert.True(true.IsEquivalentTo(true));
        Assert.True(false.IsEquivalentTo(false));
        Assert.False(true.IsEquivalentTo(false));
        Assert.False(false.IsEquivalentTo(true));
        Assert.False(true.IsEquivalentTo(null));
        Assert.False(false.IsEquivalentTo(null));
        Assert.True(true.IsEquivalentTo("true"));
        Assert.True(false.IsEquivalentTo("false"));
        Assert.False(true.IsEquivalentTo("false"));
        Assert.False(false.IsEquivalentTo("true"));
        Assert.True("true".IsEquivalentTo(true));
        Assert.True("false".IsEquivalentTo(false));
        Assert.False("true".IsEquivalentTo(false));
        Assert.False("false".IsEquivalentTo(true));
        Assert.True(true.IsEquivalentTo(1));
        Assert.False(true.IsEquivalentTo(0));
        Assert.True(false.IsEquivalentTo(0));
        Assert.False(false.IsEquivalentTo(1));
        Assert.False(true.IsEquivalentTo(2));
        Assert.False(false.IsEquivalentTo(2));
        Assert.False(2.IsEquivalentTo(true));
        Assert.False(2.IsEquivalentTo(false));

        bool? b1 = null;

        Assert.True(b1.IsEquivalentTo(null));
        Assert.False(b1.IsEquivalentTo(true));
        Assert.False(b1.IsEquivalentTo(false));
        Assert.False(true.IsEquivalentTo(b1));
        Assert.False(false.IsEquivalentTo(b1));

        b1 = true;

        Assert.True(b1.IsEquivalentTo(true));
        Assert.False(b1.IsEquivalentTo(false));
        Assert.True(true.IsEquivalentTo(b1));
        Assert.False(false.IsEquivalentTo(b1));
        
        b1 = null;
        bool? b2 = null;

        Assert.True(b1.IsEquivalentTo(b2));
        Assert.True(b2.IsEquivalentTo(b1));

        b1 = true;

        Assert.False(b1.IsEquivalentTo(b2));
        Assert.False(b2.IsEquivalentTo(b1));

        b2 = true;

        Assert.True(b1.IsEquivalentTo(b2));
        Assert.True(b2.IsEquivalentTo(b1));

        b2 = false;

        Assert.False(b1.IsEquivalentTo(b2));
        Assert.False(b2.IsEquivalentTo(b1));

        bool b3 = true;

        Assert.True(b3.IsEquivalentTo(true));
        Assert.False(b3.IsEquivalentTo(false));
        Assert.False(b3.IsEquivalentTo(null));
        Assert.True(b3.IsEquivalentTo(b1));
        Assert.True(b1.IsEquivalentTo(b3));

        Assert.False(b2.IsEquivalentTo(b1));
        Assert.False(b1.IsEquivalentTo(b2));

        bool b4 = true;

        Assert.True(b4.IsEquivalentTo(b3));
        Assert.True(b3.IsEquivalentTo(b4));

        b4 = false;

        Assert.False(b4.IsEquivalentTo(b3));
        Assert.False(b3.IsEquivalentTo(b4));
    }

    [Fact]
    public void Object_IsEquivalentTo_Enum()
    {
        PhoneType e1 = PhoneType.Personal;
        PhoneType e2 = PhoneType.Personal;

        Assert.True(e1.IsEquivalentTo(e2));
        Assert.True(e1.IsEquivalentTo("personal"));
        Assert.True(e1.IsEquivalentTo("Personal"));
        Assert.True(e1.IsEquivalentTo(0));
        Assert.False(e1.IsEquivalentTo(1));
        Assert.False(e1.IsEquivalentTo(null));

        e2 = PhoneType.Business;

        Assert.False(e1.IsEquivalentTo(e2));

        PhoneType? e3 = null;

        Assert.False(e1.IsEquivalentTo(e3));

        e3 = PhoneType.Business;

        Assert.False(e1.IsEquivalentTo(e3));
        Assert.False(e3.IsEquivalentTo(e1));
        Assert.True(e2.IsEquivalentTo(e3));
        Assert.True(e3.IsEquivalentTo(e2));
    }

    [Fact]
    public void Object_IsEquivalentTo_String()
    {
        string s1 = "ABC";
        string s2 = "ABC";

        Assert.True(s1.IsEquivalentTo(s2));
        Assert.True(s2.IsEquivalentTo(s1));
        Assert.True(s1.IsEquivalentTo("ABC"));
        Assert.True("ABC".IsEquivalentTo(s1));

        Assert.False(s1.IsEquivalentTo(null));

        s2 = "";
        Assert.False(s1.IsEquivalentTo(s2));
        Assert.False(s2.IsEquivalentTo(s1));
        Assert.False(s2.IsEquivalentTo("ABC"));
        Assert.False("ABC".IsEquivalentTo(s2));

        s2 = "abc";
        Assert.False(s1.IsEquivalentTo(s2));
        Assert.False(s2.IsEquivalentTo(s1));
        Assert.False(s2.IsEquivalentTo("ABC"));
        Assert.False("ABC".IsEquivalentTo(s2));

        string? s3 = null;
        string? s4 = null;

        Assert.True(s3.IsEquivalentTo(s4));
        Assert.True(s3.IsEquivalentTo(null));

        s3 = "ABC";
        Assert.True(s3.IsEquivalentTo(s1));
        Assert.True(s1.IsEquivalentTo(s3));
    }

    [Fact]
    public void Object_IsEquivalentTo_Integer()
    {
        short n1 = 1;

        Assert.True(n1.IsEquivalentTo(1));
        Assert.True(1.IsEquivalentTo(n1));
        Assert.False(n1.IsEquivalentTo(0));
        Assert.False(0.IsEquivalentTo(n1));
        Assert.False(n1.IsEquivalentTo(-1));
        Assert.False((-1).IsEquivalentTo(n1));

        short n2 = 1;

        Assert.True(n1.IsEquivalentTo(n2));
        Assert.True(n2.IsEquivalentTo(n1));

        n2 = 2;

        Assert.False(n1.IsEquivalentTo(n2));
        Assert.False(n2.IsEquivalentTo(n1));

        int n3 = 1;

        Assert.True(n3.IsEquivalentTo(1));
        Assert.True(1.IsEquivalentTo(n3));
        Assert.False(n3.IsEquivalentTo(0));
        Assert.False(0.IsEquivalentTo(n3));
        Assert.False(n3.IsEquivalentTo(-1));
        Assert.False((-1).IsEquivalentTo(n3));

        Assert.True(n3.IsEquivalentTo(n1));
        Assert.True(n1.IsEquivalentTo(n3));
        Assert.False(n3.IsEquivalentTo(n2));
        Assert.False(n2.IsEquivalentTo(n3));

        int n4 = 1;

        Assert.True(n3.IsEquivalentTo(n4));
        Assert.True(n4.IsEquivalentTo(n3));

        n4 = 2;

        Assert.False(n3.IsEquivalentTo(n4));
        Assert.False(n4.IsEquivalentTo(n3));

        long n5 = 1;

        Assert.True(n5.IsEquivalentTo(1));
        Assert.True(1.IsEquivalentTo(n5));
        Assert.False(n5.IsEquivalentTo(0));
        Assert.False(0.IsEquivalentTo(n5));
        Assert.False(n5.IsEquivalentTo(-1));
        Assert.False((-1).IsEquivalentTo(n5));

        Assert.True(n5.IsEquivalentTo(n1));
        Assert.True(n1.IsEquivalentTo(n5));
        Assert.False(n5.IsEquivalentTo(n2));
        Assert.False(n2.IsEquivalentTo(n5));

        Assert.True(n5.IsEquivalentTo(n3));
        Assert.True(n3.IsEquivalentTo(n5));
        Assert.False(n5.IsEquivalentTo(n4));
        Assert.False(n4.IsEquivalentTo(n5));

        long n6 = 1;

        Assert.True(n5.IsEquivalentTo(n6));
        Assert.True(n6.IsEquivalentTo(n5));

        n6 = 2;

        Assert.False(n5.IsEquivalentTo(n6));
        Assert.False(n6.IsEquivalentTo(n5));

        ushort n7 = 1;

        Assert.True(n7.IsEquivalentTo(1));
        Assert.True(1.IsEquivalentTo(n7));
        Assert.False(n7.IsEquivalentTo(0));
        Assert.False(0.IsEquivalentTo(n7));
        Assert.False(n7.IsEquivalentTo(-1));
        Assert.False((-1).IsEquivalentTo(n7));

        Assert.True(n7.IsEquivalentTo(n1));
        Assert.True(n1.IsEquivalentTo(n7));
        Assert.False(n7.IsEquivalentTo(n2));
        Assert.False(n2.IsEquivalentTo(n7));

        Assert.True(n7.IsEquivalentTo(n3));
        Assert.True(n3.IsEquivalentTo(n7));
        Assert.False(n7.IsEquivalentTo(n4));
        Assert.False(n4.IsEquivalentTo(n7));

        Assert.True(n7.IsEquivalentTo(n5));
        Assert.True(n5.IsEquivalentTo(n7));
        Assert.False(n7.IsEquivalentTo(n6));
        Assert.False(n6.IsEquivalentTo(n7));

        ushort n8 = 1;

        Assert.True(n7.IsEquivalentTo(n8));
        Assert.True(n8.IsEquivalentTo(n7));

        n8 = 2;

        Assert.False(n7.IsEquivalentTo(n8));
        Assert.False(n8.IsEquivalentTo(n7));

        uint n9 = 1;

        Assert.True(n9.IsEquivalentTo(1));
        Assert.True(1.IsEquivalentTo(n9));
        Assert.False(n9.IsEquivalentTo(0));
        Assert.False(0.IsEquivalentTo(n9));
        Assert.False(n9.IsEquivalentTo(-1));
        Assert.False((-1).IsEquivalentTo(n9));

        Assert.True(n9.IsEquivalentTo(n1));
        Assert.True(n1.IsEquivalentTo(n9));
        Assert.False(n9.IsEquivalentTo(n2));
        Assert.False(n2.IsEquivalentTo(n9));

        Assert.True(n9.IsEquivalentTo(n3));
        Assert.True(n3.IsEquivalentTo(n9));
        Assert.False(n9.IsEquivalentTo(n4));
        Assert.False(n4.IsEquivalentTo(n9));

        Assert.True(n9.IsEquivalentTo(n5));
        Assert.True(n5.IsEquivalentTo(n9));
        Assert.False(n9.IsEquivalentTo(n6));
        Assert.False(n6.IsEquivalentTo(n9));

        Assert.True(n9.IsEquivalentTo(n7));
        Assert.True(n7.IsEquivalentTo(n9));
        Assert.False(n9.IsEquivalentTo(n8));
        Assert.False(n8.IsEquivalentTo(n9));

        ushort n10 = 1;

        Assert.True(n9.IsEquivalentTo(n10));
        Assert.True(n10.IsEquivalentTo(n9));

        n10 = 2;

        Assert.False(n9.IsEquivalentTo(n10));
        Assert.False(n10.IsEquivalentTo(n9));

        uint n11 = 1;

        Assert.True(n11.IsEquivalentTo(1));
        Assert.True(1.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(0));
        Assert.False(0.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(-1));
        Assert.False((-1).IsEquivalentTo(n11));

        Assert.True(n11.IsEquivalentTo(n1));
        Assert.True(n1.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(n2));
        Assert.False(n2.IsEquivalentTo(n11));

        Assert.True(n11.IsEquivalentTo(n3));
        Assert.True(n3.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(n4));
        Assert.False(n4.IsEquivalentTo(n11));

        Assert.True(n11.IsEquivalentTo(n5));
        Assert.True(n5.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(n6));
        Assert.False(n6.IsEquivalentTo(n11));

        Assert.True(n11.IsEquivalentTo(n7));
        Assert.True(n7.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(n8));
        Assert.False(n8.IsEquivalentTo(n11));

        Assert.True(n11.IsEquivalentTo(n9));
        Assert.True(n9.IsEquivalentTo(n11));
        Assert.False(n11.IsEquivalentTo(n10));
        Assert.False(n10.IsEquivalentTo(n11));

        ushort n12 = 1;

        Assert.True(n11.IsEquivalentTo(n12));
        Assert.True(n12.IsEquivalentTo(n11));

        n12 = 2;

        Assert.False(n11.IsEquivalentTo(n12));
        Assert.False(n12.IsEquivalentTo(n11));
    }

    [Fact]
    public void Object_IsEquivalentTo_DateTime()
    {
        string s;

        DateTime d1 = new(2020, 1, 2, 3, 4, 5, 678);
        DateTime d2 = new(2020, 1, 2, 3, 4, 5, 678);

        Assert.True(d1.IsEquivalentTo(d2));
        Assert.True(d2.IsEquivalentTo(d1));

        DateTime d3 = new(2020, 1, 2, 3, 4, 5, 789);

        Assert.False(d1.IsEquivalentTo(d3));
        Assert.False(d3.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d1.IsEquivalentTo(s));
        Assert.True(s.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:00";

        Assert.True(d1.IsEquivalentTo(s));
        Assert.True(s.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.False(d1.IsEquivalentTo(s));
        Assert.False(s.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsEquivalentTo(s));
        Assert.False(s.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.789Z";

        Assert.False(d1.IsEquivalentTo(s));
        Assert.False(s.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.789+00:00";

        Assert.False(d1.IsEquivalentTo(s));
        Assert.False(s.IsEquivalentTo(d1));
    }

    [Fact]
    public void Object_IsEquivalentTo_DateTimeOffset()
    {
        string s;

        DateTimeOffset d1 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));
        DateTimeOffset d2 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));

        Assert.True(d1.IsEquivalentTo(d2));
        Assert.True(d2.IsEquivalentTo(d1));

        DateTimeOffset d3 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(-30));

        Assert.False(d1.IsEquivalentTo(d3));
        Assert.False(d3.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.True(d1.IsEquivalentTo(s));
        Assert.True(s.IsEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsEquivalentTo(s));
        Assert.False(s.IsEquivalentTo(d1));
        Assert.True(d3.IsEquivalentTo(s));
        Assert.True(s.IsEquivalentTo(d3));

        DateTimeOffset d4 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromSeconds(0));
        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d4.IsEquivalentTo(s));
        Assert.True(s.IsEquivalentTo(d4));

        s = "2020-01-02T03:04:05.876Z";

        Assert.False(d4.IsEquivalentTo(s));
        Assert.False(s.IsEquivalentTo(d4));
    }

    [Fact]
    public void Object_IsEquivalentTo_ArrayNumeric()
    {
        int[] aInt1 = [1, 2, 3,];
        int[] aInt2 = [1, 2, 3,];

        Assert.True(aInt1.IsEquivalentTo(aInt2));
        Assert.True(aInt2.IsEquivalentTo(aInt1));

        int[] aInt3 = [1, 2];

        Assert.False(aInt1.IsEquivalentTo(aInt3));
        Assert.False(aInt3.IsEquivalentTo(aInt1));

        int[] aInt4 = [3, 2, 1];

        Assert.False(aInt1.IsEquivalentTo(aInt4));
        Assert.False(aInt4.IsEquivalentTo(aInt1));

        long[] aLong1 = [1, 2, 3];

        Assert.True(aInt1.IsEquivalentTo(aLong1));
        Assert.True(aLong1.IsEquivalentTo(aInt1));

        long[] aLong2 = [1, 2];

        Assert.False(aInt1.IsEquivalentTo(aLong2));
        Assert.False(aLong2.IsEquivalentTo(aInt1));

        long[] aLong3 = [3, 2, 1];

        Assert.False(aInt1.IsEquivalentTo(aLong3));
        Assert.False(aLong3.IsEquivalentTo(aInt1));

        short[] aShort1 = [1, 2, 3];

        Assert.True(aInt1.IsEquivalentTo(aShort1));
        Assert.True(aShort1.IsEquivalentTo(aInt1));

        short[] aShort2 = [1, 2];

        Assert.False(aInt1.IsEquivalentTo(aShort2));
        Assert.False(aShort2.IsEquivalentTo(aInt1));

        short[] aShort3 = [3, 2, 1];

        Assert.False(aInt1.IsEquivalentTo(aShort3));
        Assert.False(aShort3.IsEquivalentTo(aInt1));
    }

    [Fact]
    public void Object_IsEquivalentTo_StringCollections()
    {
        string[] a1 = ["one", "two", "three",];
        string[] a2 = ["one", "two", "three",];

        Assert.True(a1.IsEquivalentTo(a2));
        Assert.True(a2.IsEquivalentTo(a1));

        string[] a3 = ["one", "two",];

        Assert.False(a1.IsEquivalentTo(a3));
        Assert.False(a3.IsEquivalentTo(a1));

        string[] a4 = ["three", "two", "one"];

        Assert.False(a1.IsEquivalentTo(a4));
        Assert.False(a4.IsEquivalentTo(a1));

        string[] a5 = ["One", "Two", "Three"];

        Assert.False(a1.IsEquivalentTo(a5));
        Assert.False(a5.IsEquivalentTo(a1));

        List<string> l1 = ["one", "two", "three",];
        List<string> l2 = ["one", "two", "three",];

        Assert.True(l1.IsEquivalentTo(l2));
        Assert.True(l2.IsEquivalentTo(l1));
        Assert.True(l1.IsEquivalentTo(a1));
        Assert.True(a1.IsEquivalentTo(l1));

        List<string> l3 = ["one", "two",];

        Assert.False(l1.IsEquivalentTo(l3));
        Assert.False(l3.IsEquivalentTo(l1));

        Assert.False(l3.IsEquivalentTo(a1));
        Assert.False(a1.IsEquivalentTo(l3));

        List<string> l4 = ["three", "two", "one"];

        Assert.False(l1.IsEquivalentTo(l4));
        Assert.False(l4.IsEquivalentTo(l1));

        Assert.False(l4.IsEquivalentTo(a1));
        Assert.False(a1.IsEquivalentTo(l4));
    }

    [Fact]
    public void Object_IsEquivalentTo_Dictionary()
    {
        Dictionary<string, string> d1 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "six"
        };
        Dictionary<string, string> d2 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "six"
        };

        Assert.True(d1.IsEquivalentTo(d2));
        Assert.True(d2.IsEquivalentTo(d1));

        Dictionary<string, string> d3 = new()
        {
            ["one"] = "two",
            ["three"] = "four"
        };

        Assert.False(d1.IsEquivalentTo(d3));
        Assert.False(d3.IsEquivalentTo(d1));

        Dictionary<string, string> d4 = new()
        {
            ["one"] = "two",
            ["three"] = "ten",
            ["five"] = "six"
        };

        Assert.False(d1.IsEquivalentTo(d4));
        Assert.False(d4.IsEquivalentTo(d1));

        Dictionary<string, string> d5 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "Six"
        };

        Assert.False(d1.IsEquivalentTo(d5));
        Assert.False(d5.IsEquivalentTo(d1));
    }

    [Fact]
    public void Object_IsEquivalentTo_ObjectClone()
    {
        User u1 = new()
        {
            Name = new()
            {
                Surname = "Johnson",
                MiddleName = "Jack",
                GivenName = "John"
            },

            Mail = "john.johnson@email.com",

            OtherMail = ["jack.johnson@email.com", "jjohnson@email.com", "jj@email.com"],

            LuckyNumbers = [13, 57, 95, 38],

            PasswordExpirationDate = DateTime.Parse("2012/12/31 23:59:59.999"),

            SocialAccounts = new()
            {
                { "Facebook", new SocialAccount() { Provider = "Facebook", Account = "jack.johnson@email.com", Enabled = true} },
                { "Microsoft", new SocialAccount() { Provider = "Microsoft", Account = "jack.johnson@email.com", Enabled = true} },
                { "Google", new SocialAccount() { Provider = "Google", Account = "jack.johnson@email.com", Enabled = true} },
            },

            Phones =
            [
                new Phone { IsMobile = true , IsPrimary = true, Number = "+13334445566", Type = PhoneType.Personal },
                new Phone { IsMobile = false , IsPrimary = false, Number = "+13334445577", Type = PhoneType.Personal },
                new Phone { IsMobile = false , IsPrimary = false, Number = "+13334445588", Type = PhoneType.Business },
                new Phone { IsMobile = false, IsPrimary = false, Number = "+13334445599", Type = PhoneType.Other },
                new Phone { IsMobile = true, IsPrimary = false, Number = "+13334445500", Type = PhoneType.Other },
            ],

            Tags = new()
            {
                ["greeting"] = "hello",
                ["color"] = "red",
                ["shape"] = "oval"
            }
        };

        User u2 = u1.Clone() ?? new();

        Assert.True(u1.IsEquivalentTo(u2));
        Assert.True(u2.IsEquivalentTo(u1));

        u2.Tags?.Remove("greeting");

        Assert.False(u2.IsEquivalentTo(u1));
        Assert.True(u2.IsEquivalentTo(u1, true));
        Assert.False(u1.IsEquivalentTo(u2));
        Assert.False(u1.IsEquivalentTo(u2, true));

        u2.Tags?.Clear();

        Assert.False(u2.IsEquivalentTo(u1));
        Assert.True(u2.IsEquivalentTo(u1, true));
        Assert.False(u1.IsEquivalentTo(u2));
        Assert.False(u1.IsEquivalentTo(u2, true));

        u2.Tags = new()
        {
            ["greeting"] = "hello",
            ["color"] = "red",
            ["shape"] = "oval"
        };
        Assert.True(u1.IsEquivalentTo(u2));
        Assert.True(u2.IsEquivalentTo(u1));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hi";
        }

        Assert.False(u1.IsEquivalentTo(u2));
        Assert.False(u2.IsEquivalentTo(u1));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hello";
        }

        Assert.True(u1.IsEquivalentTo(u2));
        Assert.True(u2.IsEquivalentTo(u1));

        u2.SocialAccounts?.Remove("Facebook");

        Assert.False(u2.IsEquivalentTo(u1));
        Assert.True(u2.IsEquivalentTo(u1, true));
        Assert.False(u1.IsEquivalentTo(u2));
        Assert.False(u1.IsEquivalentTo(u2, true));

        u2.SocialAccounts?.Add("Facebook", new()
        {
            Provider = "Facebook",
            Account = "jack.johnson@email.com",
            Enabled = true
        });
        Assert.True(u1.IsEquivalentTo(u2));
        Assert.True(u2.IsEquivalentTo(u1));

        if (u2.SocialAccounts?["Facebook"] != null)
        {
            u2.SocialAccounts["Facebook"].Enabled = false;
        }

        Assert.False(u1.IsEquivalentTo(u2));
        Assert.False(u2.IsEquivalentTo(u1));
    }
}
