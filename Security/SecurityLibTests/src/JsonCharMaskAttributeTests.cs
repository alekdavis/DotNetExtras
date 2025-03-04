using DotNetExtras.Common.Extensions;
using DotNetExtras.Security;

namespace SecurityTests;
public class JsonCharMaskAttributeTests
{
    public class Sample1
    {
        public string? Value { get; set; }

        [JsonCharMask('~')]
        public string? SecretA { get; set; }
    }

    public class Sample2
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [JsonCharMask()]
        public string? Secret1 { get; set; }

        [JsonCharMask('#')]
        public string? Secret2 { get; set; }

        [JsonCharMask('*', 3, 0)]
        public string? Secret3 { get; set; }

        [JsonCharMask('*', 0, 3)]
        public string? Secret4 { get; set; }

        [JsonCharMask('*', 2, 2)]
        public string? Secret5 { get; set; }

        public Sample1? Inner { get; set; }
    }

    [Fact]
    public void ToJson()
    {
        int    id    = 123;
        string value = "something";
        string name  = "whatever";
        string secret= "secret";

        Sample2 sample = new()
        {
            Id = id,
            Name = name,
            Secret1 = secret,
            Secret2 = secret,
            Secret3 = secret,
            Secret4 = secret,
            Secret5 = secret,

            Inner = new Sample1()
            {
                Value   = value,
                SecretA = secret
            }
        };

        string json = sample.ToJson();

        Sample2? sampleClone = json.FromJson<Sample2>();

        Assert.NotNull(sampleClone);

        Assert.Equal(id, sampleClone?.Id);
        Assert.Equal(name, sampleClone?.Name);

        Assert.Equal("******", sampleClone?.Secret1);
        Assert.Equal("######", sampleClone?.Secret2);
        Assert.Equal("sec***", sampleClone?.Secret3);
        Assert.Equal("***ret", sampleClone?.Secret4);
        Assert.Equal("se**et", sampleClone?.Secret5);

        Assert.NotNull(sampleClone?.Inner);

        Assert.Equal(value, sampleClone?.Inner?.Value);
        Assert.Equal("~~~~~~", sampleClone?.Inner?.SecretA);
    }
}
