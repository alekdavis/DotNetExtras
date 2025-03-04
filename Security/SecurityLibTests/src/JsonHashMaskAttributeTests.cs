using DotNetExtras.Common.Extensions;
using DotNetExtras.Security;

namespace SecurityTests;
public class JsonHashMaskAttributeTests
{
    public class Sample1
    {
        public string? Value { get; set; }

        [JsonHashMask(HashType.SHA256)]
        public string? SecretA { get; set; }
    }

    public class Sample2
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [JsonHashMask(HashType.SHA256)]
        public string? Secret0 { get; set; }

        [JsonHashMask(HashType.SHA256)]
        public string? Secret1 { get; set; }

        [JsonHashMask(HashType.SHA384)]
        public string? Secret2 { get; set; }

        [JsonHashMask(HashType.SHA512)]
        public string? Secret3 { get; set; }

        [JsonHashMask(HashType.SHA256, 4)]
        public string? Secret4 { get; set; }

        [JsonHashMask(HashType.SHA256, 4, true)]
        public string? Secret5 { get; set; }

        [JsonHashMask(HashType.SHA256, 4, true)]
        public string? Secret6 { get; set; }

        public Sample1? Inner { get; set; }
    }

    [Fact]
    public void ToJson()
    {
        int    id      = 123;
        string value   = "something";
        string name    = "whatever";
        string secret  = "secret";

        Sample2 sample = new()
        {
            Id = id,
            Name = name,
            Secret0 = null,
            Secret1 = secret,
            Secret2 = secret,
            Secret3 = secret,
            Secret4 = secret,
            Secret5 = secret,
            Secret6 = secret,

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

        Assert.Null(sampleClone?.Secret0);
        Assert.Equal("2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b", sampleClone?.Secret1);
        Assert.Equal("58a775ba4112be3005ae4407ce757d88fda71d40497bb8026ecac54d4e3ffc7232ce8de3ab5acb30ae39760fee7c53ed", sampleClone?.Secret2);
        Assert.Equal("bd2b1aaf7ef4f09be9f52ce2d8d599674d81aa9d6a4421696dc4d93dd0619d682ce56b4d64a9ef097761ced99e0f67265b5f76085e5b0ee7ca4696b2ad6fe2b2", sampleClone?.Secret3);
        Assert.NotEqual(sampleClone?.Secret1, sampleClone?.Secret4);
        Assert.Equal(64, sampleClone?.Secret4?.Length);
        Assert.Equal(72, sampleClone?.Secret5?.Length);
        Assert.Equal(72, sampleClone?.Secret6?.Length);

        Assert.NotNull(sampleClone?.Inner);

        Assert.Equal(value, sampleClone?.Inner?.Value);
        Assert.Equal("2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b", sampleClone?.Inner?.SecretA);
    }
}