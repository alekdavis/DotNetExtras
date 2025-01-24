using DotNetExtras.Common;

namespace CommonLibTests;

public class Info
{
    public string? X {  get; set; }

    public int? Y { get; set; }

    public bool? Z { get; set; }

    public string? ShortName { get; set; }

    public string? USName { get; set; }

    public string? USAName { get; set; }
}

public class Data
{
    public Info? Info { get; set; }

    public string? A { get; set; }

    public int? B { get; set; }

    public bool? C { get; set; }

    public Inner? Internal { get; set; }

    public class Inner
    {
        public Data? Data { get; set; }
    }
}

public class NameOfTests
{
    [Fact]
    public void NameOf_Full()
    {
        Assert.Equal("Data.Internal.Data", NameOf.Full(nameof(Data.Internal.Data)));
        Assert.Equal("data.internal.data", NameOf.Full(nameof(Data.Internal.Data), true));

        Data data = new()
        {
            Info = new(),

            Internal = new()
            {
                Data = new()
            }
        };

        Assert.Equal("data", NameOf.Full(data));
        Assert.Equal("data", NameOf.Full(data, true));

        Assert.Equal("data.A", NameOf.Full(data.A));
        Assert.Equal("data.a", NameOf.Full(data.A, true));

        Assert.Equal("data.Info", NameOf.Full(data.Info));
        Assert.Equal("data.info", NameOf.Full(data.Info, true));

        Assert.Equal("data.Info.X", NameOf.Full(data.Info?.X));
        Assert.Equal("data.info.x", NameOf.Full(data.Info?.X, true));

        Assert.Equal("data.Internal.Data.B", NameOf.Full(data.Internal?.Data?.B));
        Assert.Equal("data.internal.data.b", NameOf.Full(data.Internal?.Data?.B, true));

        Assert.Equal("data.Info.USAName", NameOf.Full(data.Info?.USAName));
        Assert.Equal("data.info.usaName", NameOf.Full(data.Info?.USAName, true));

        Assert.Equal("data.Info.USName", NameOf.Full(data.Info?.USName));
        Assert.Equal("data.info.usName", NameOf.Full(data.Info?.USName, true));
    }

    [Fact]
    public void NameOf_Long()
    {
        Assert.Equal("Internal.Data", NameOf.Long(nameof(Data.Internal.Data)));
        Assert.Equal("internal.data", NameOf.Long(nameof(Data.Internal.Data), true));

        Data data = new()
        {
            Info = new(),

            Internal = new()
            {
                Data = new()
            }
        };

        Assert.Equal("Data", NameOf.Long(nameof(Data)));
        Assert.Equal("data", NameOf.Long(nameof(Data), true));

        Assert.Equal("data", NameOf.Long(data));
        Assert.Equal("data", NameOf.Long(data, true));

        Assert.Equal("A", NameOf.Long(data.A));
        Assert.Equal("a", NameOf.Long(data.A, true));

        Assert.Equal("Info", NameOf.Long(data.Info));
        Assert.Equal("info", NameOf.Long(data.Info, true));

        Assert.Equal("Info.X", NameOf.Long(data.Info?.X));
        Assert.Equal("info.x", NameOf.Long(data.Info?.X, true));

        Assert.Equal("Internal.Data.B", NameOf.Long(data.Internal?.Data?.B));
        Assert.Equal("internal.data.b", NameOf.Long(data.Internal?.Data?.B, true));

        Assert.Equal("Info.USAName", NameOf.Long(data.Info?.USAName));
        Assert.Equal("info.usaName", NameOf.Long(data.Info?.USAName, true));

        Assert.Equal("Info.USName", NameOf.Long(data.Info?.USName));
        Assert.Equal("info.usName", NameOf.Long(data.Info?.USName, true));
    }

    [Fact]
    public void NameOf_Short()
    {
        Assert.Equal("Data", NameOf.Short(nameof(Data.Internal.Data)));
        Assert.Equal("data", NameOf.Short(nameof(Data.Internal.Data), true));

        Data data = new()
        {
            Info = new(),

            Internal = new()
            {
                Data = new()
            }
        };

        Assert.Equal("data", NameOf.Short(data));
        Assert.Equal("data", NameOf.Short(data, true));

        Assert.Equal("A", NameOf.Short(data.A));
        Assert.Equal("a", NameOf.Short(data.A, true));

        Assert.Equal("Info", NameOf.Short(data.Info));
        Assert.Equal("info", NameOf.Short(data.Info, true));

        Assert.Equal("X", NameOf.Short(data.Info?.X));
        Assert.Equal("x", NameOf.Short(data.Info?.X, true));

        Assert.Equal("B", NameOf.Short(data.Internal?.Data?.B));
        Assert.Equal("b", NameOf.Short(data.Internal?.Data?.B, true));

        Assert.Equal("USAName", NameOf.Short(data.Info?.USAName));
        Assert.Equal("usaName", NameOf.Short(data.Info?.USAName, true));

        Assert.Equal("USName", NameOf.Short(data.Info?.USName));
        Assert.Equal("usName", NameOf.Short(data.Info?.USName, true));
    }

    [Fact]
    public void NameOf_Skip()
    {
        Assert.Equal("Internal.Data", NameOf.Skip(nameof(Data.Internal.Data)));
        Assert.Equal("Internal.Data", NameOf.Skip(nameof(Data.Internal.Data), 1));
        Assert.Equal("internal.data", NameOf.Skip(nameof(Data.Internal.Data), 1, true));

        Data data = new()
        {
            Info = new(),

            Internal = new()
            {
                Data = new()
            }
        };

        Assert.Equal("", NameOf.Skip(data));
        Assert.Equal("", NameOf.Skip(data, 1));
        Assert.Equal("", NameOf.Skip(data, 1, false));
        Assert.Equal("", NameOf.Skip(data, 1, true));
        Assert.Equal("", NameOf.Skip(data, 2));
        Assert.Equal("", NameOf.Skip(data, 2, false));
        Assert.Equal("", NameOf.Skip(data, 2, true));

        Assert.Equal("A", NameOf.Skip(data.A));
        Assert.Equal("A", NameOf.Skip(data.A, 1));
        Assert.Equal("A", NameOf.Skip(data.A, 1, false));
        Assert.Equal("a", NameOf.Skip(data.A, 1, true));
        Assert.Equal("", NameOf.Skip(data.A, 2));
        Assert.Equal("", NameOf.Skip(data.A, 2, false));
        Assert.Equal("", NameOf.Skip(data.A, 2, true));
        Assert.Equal("", NameOf.Skip(data.A, 3));
        Assert.Equal("", NameOf.Skip(data.A, 3, false));
        Assert.Equal("", NameOf.Skip(data.A, 3, true));

        Assert.Equal("Info.X", NameOf.Skip(data.Info?.X));
        Assert.Equal("Info.X", NameOf.Skip(data.Info?.X, 1));
        Assert.Equal("Info.X", NameOf.Skip(data.Info?.X, 1, false));
        Assert.Equal("info.x", NameOf.Skip(data.Info?.X, 1, true));
        Assert.Equal("X", NameOf.Skip(data.Info?.X, 2));
        Assert.Equal("X", NameOf.Skip(data.Info?.X, 2, false));
        Assert.Equal("x", NameOf.Skip(data.Info?.X, 2, true));
        Assert.Equal("", NameOf.Skip(data.Info?.X, 3));
        Assert.Equal("", NameOf.Skip(data.Info?.X, 3, false));
        Assert.Equal("", NameOf.Skip(data.Info?.X, 3, true));
        Assert.Equal("", NameOf.Skip(data.Info?.X, 4));
        Assert.Equal("", NameOf.Skip(data.Info?.X, 4, false));
        Assert.Equal("", NameOf.Skip(data.Info?.X, 4, true));

        Assert.Equal("Internal.Data.B", NameOf.Skip(data.Internal?.Data?.B));
        Assert.Equal("Internal.Data.B", NameOf.Skip(data.Internal?.Data?.B, 1));
        Assert.Equal("Internal.Data.B", NameOf.Skip(data.Internal?.Data?.B, 1, false));
        Assert.Equal("internal.data.b", NameOf.Skip(data.Internal?.Data?.B, 1, true));
        Assert.Equal("Data.B", NameOf.Skip(data.Internal?.Data?.B, 2));
        Assert.Equal("Data.B", NameOf.Skip(data.Internal?.Data?.B, 2, false));
        Assert.Equal("data.b", NameOf.Skip(data.Internal?.Data?.B, 2, true));
        Assert.Equal("B", NameOf.Skip(data.Internal?.Data?.B, 3));
        Assert.Equal("B", NameOf.Skip(data.Internal?.Data?.B, 3, false));
        Assert.Equal("b", NameOf.Skip(data.Internal?.Data?.B, 3, true));
        Assert.Equal("", NameOf.Skip(data.Internal?.Data?.B, 4, false));
        Assert.Equal("", NameOf.Skip(data.Internal?.Data?.B, 4));
        Assert.Equal("", NameOf.Skip(data.Internal?.Data?.B, 4, true));
        Assert.Equal("", NameOf.Skip(data.Internal?.Data?.B, 5));
        Assert.Equal("", NameOf.Skip(data.Internal?.Data?.B, 5, false));
        Assert.Equal("", NameOf.Skip(data.Internal?.Data?.B, 5, true));

        Assert.Equal("info.usaName", NameOf.Skip(data.Info?.USAName, 1, true));
        Assert.Equal("usaName", NameOf.Skip(data.Info?.USAName, 2, true));

        Assert.Equal("info.usName", NameOf.Skip(data.Info?.USName, 1, true));
        Assert.Equal("usName", NameOf.Skip(data.Info?.USName, 2, true));
    }

    [Fact]
    public void NameOf_Keep()
    {
        Assert.Equal("Data", NameOf.Keep(nameof(Data.Internal.Data)));
        Assert.Equal("Data", NameOf.Keep(nameof(Data.Internal.Data), 1));
        Assert.Equal("data", NameOf.Keep(nameof(Data.Internal.Data), 1, true));

        Data data = new()
        {
            Info = new(),

            Internal = new()
            {
                Data = new()
            }
        };

        Assert.Equal("data", NameOf.Keep(data));
        Assert.Equal("data", NameOf.Keep(data, 1));
        Assert.Equal("data", NameOf.Keep(data, 1, false));
        Assert.Equal("data", NameOf.Keep(data, 1, true));
        Assert.Equal("data", NameOf.Keep(data, 2));
        Assert.Equal("data", NameOf.Keep(data, 2, false));
        Assert.Equal("data", NameOf.Keep(data, 2, true));

        Assert.Equal("A", NameOf.Keep(data.A));
        Assert.Equal("A", NameOf.Keep(data.A, 1));
        Assert.Equal("A", NameOf.Keep(data.A, 1, false));
        Assert.Equal("a", NameOf.Keep(data.A, 1, true));
        Assert.Equal("data.A", NameOf.Keep(data.A, 2));
        Assert.Equal("data.A", NameOf.Keep(data.A, 2, false));
        Assert.Equal("data.a", NameOf.Keep(data.A, 2, true));
        Assert.Equal("data.A", NameOf.Keep(data.A, 3));
        Assert.Equal("data.A", NameOf.Keep(data.A, 3, false));
        Assert.Equal("data.a", NameOf.Keep(data.A, 3, true));

        Assert.Equal("X", NameOf.Keep(data.Info?.X));
        Assert.Equal("X", NameOf.Keep(data.Info?.X, 1));
        Assert.Equal("X", NameOf.Keep(data.Info?.X, 1, false));
        Assert.Equal("x", NameOf.Keep(data.Info?.X, 1, true));
        Assert.Equal("Info.X", NameOf.Keep(data.Info?.X, 2));
        Assert.Equal("Info.X", NameOf.Keep(data.Info?.X, 2, false));
        Assert.Equal("info.x", NameOf.Keep(data.Info?.X, 2, true));
        Assert.Equal("data.Info.X", NameOf.Keep(data.Info?.X, 3));
        Assert.Equal("data.Info.X", NameOf.Keep(data.Info?.X, 3, false));
        Assert.Equal("data.info.x", NameOf.Keep(data.Info?.X, 3, true));
        Assert.Equal("data.Info.X", NameOf.Keep(data.Info?.X, 4));
        Assert.Equal("data.Info.X", NameOf.Keep(data.Info?.X, 4, false));
        Assert.Equal("data.info.x", NameOf.Keep(data.Info?.X, 4, true));

        Assert.Equal("B", NameOf.Keep(data.Internal?.Data?.B));
        Assert.Equal("B", NameOf.Keep(data.Internal?.Data?.B, 1));
        Assert.Equal("B", NameOf.Keep(data.Internal?.Data?.B, 1, false));
        Assert.Equal("b", NameOf.Keep(data.Internal?.Data?.B, 1, true));
        Assert.Equal("Data.B", NameOf.Keep(data.Internal?.Data?.B, 2));
        Assert.Equal("Data.B", NameOf.Keep(data.Internal?.Data?.B, 2, false));
        Assert.Equal("data.b", NameOf.Keep(data.Internal?.Data?.B, 2, true));
        Assert.Equal("Internal.Data.B", NameOf.Keep(data.Internal?.Data?.B, 3));
        Assert.Equal("Internal.Data.B", NameOf.Keep(data.Internal?.Data?.B, 3, false));
        Assert.Equal("internal.data.b", NameOf.Keep(data.Internal?.Data?.B, 3, true));
        Assert.Equal("data.Internal.Data.B", NameOf.Keep(data.Internal?.Data?.B, 4, false));
        Assert.Equal("data.Internal.Data.B", NameOf.Keep(data.Internal?.Data?.B, 4));
        Assert.Equal("data.internal.data.b", NameOf.Keep(data.Internal?.Data?.B, 4, true));
        Assert.Equal("data.Internal.Data.B", NameOf.Keep(data.Internal?.Data?.B, 5));
        Assert.Equal("data.Internal.Data.B", NameOf.Keep(data.Internal?.Data?.B, 5, false));
        Assert.Equal("data.internal.data.b", NameOf.Keep(data.Internal?.Data?.B, 5, true));

        Assert.Equal("usaName", NameOf.Keep(data.Info?.USAName, 1, true));
        Assert.Equal("info.usaName", NameOf.Keep(data.Info?.USAName, 2, true));

        Assert.Equal("usName", NameOf.Keep(data.Info?.USName, 1, true));
        Assert.Equal("info.usName", NameOf.Keep(data.Info?.USName, 2, true));
    }
}
