using DotNetExtras.Common.Extensions;
using DotNetExtras.Security;

namespace SecurityTests;
public class JsonLiteralMaskAttributeTests
{
    public class Sample1
    {
        public string? Value { get; set; }

        [JsonLiteralMask("~~~~~~")]
        public string? SecretA { get; set; }
    }

    public class Sample2
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [JsonLiteralMask(null)]
        public string? Secret1 { get; set; }

        [JsonLiteralMask("")]
        public string? Secret2 { get; set; }

        [JsonLiteralMask("***masked3***")]
        public string? Secret3 { get; set; }

        [JsonLiteralMask("***masked4***")]
        public string? Secret4 { get; set; }

        [JsonLiteralMask("***masked5***")]
        public string? Secret5 { get; set; }

        public Sample1? Inner { get; set; }
    }

    [Fact]
    public void ToJson()
    {
        int    id      = 123;
        string value   = "something";
        string name    = "whatever";
        string secret  = "secret";
        string secret1 = "secret1";
        string secret2 = "secret2";
        string secret3 = "secret3";

        Sample2 sample = new()
        {
            Id = id,
            Name = name,
            Secret1 = secret1,
            Secret2 = secret2,
            Secret3 = secret3,
            Secret4 = null,
            Secret5 = "",

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

        Assert.Null(sampleClone?.Secret1);
        Assert.Equal("", sampleClone?.Secret2);
        Assert.Equal("***masked3***", sampleClone?.Secret3);
        Assert.Null(sampleClone?.Secret4);
        Assert.Equal("***masked5***", sampleClone?.Secret5);

        Assert.NotNull(sampleClone?.Inner);

        Assert.Equal(value, sampleClone?.Inner?.Value);
        Assert.Equal("~~~~~~", sampleClone?.Inner?.SecretA);
    }
}