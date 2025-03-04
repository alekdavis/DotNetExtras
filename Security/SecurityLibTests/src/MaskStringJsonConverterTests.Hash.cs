using DotNetExtras.Security;

namespace SecurityTests;
public partial class MaskStringJsonConverterTests
{
    [Theory]
    [InlineData(null, null, HashType.SHA256)]
    [InlineData(null, null, HashType.SHA384)]
    [InlineData(null, null, HashType.SHA512)]
    [InlineData("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", HashType.SHA256)]
    [InlineData("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", HashType.SHA256, 4)]
    [InlineData("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", HashType.SHA256, 4, true)]
    [InlineData("", "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", HashType.SHA384)]
    [InlineData("", "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", HashType.SHA384, 8)]
    [InlineData("", "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", HashType.SHA384, 8, true)]
    [InlineData("", "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", HashType.SHA512)]
    [InlineData("", "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", HashType.SHA512, 2)]
    [InlineData("", "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", HashType.SHA512, 2, true)]
    [InlineData("hello", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", HashType.SHA256)]
    [InlineData("hello", "59e1748777448c69de6b800d7a33bbfb9ff1b463e44354c3553bcdb9c666fa90125a3c79f90397bdf5f6a13de828684f", HashType.SHA384)]
    [InlineData("hello", "9b71d224bd62f3785d96d46ad3ea3d73319bfbc2890caadae2dff72519673ca72323c3d99ba5c11d7c7acc6e14b8c5da0c4663475c2e5c3adef46f73bcdec043", HashType.SHA512)]
    public void HashMask_Write
    (
        string? input,
        string? expected,
        HashType hashType,
        int saltLength = 0,
        bool saveSalt = false

    )
    {
        HashMaskStringJsonConverter converter = new(hashType, saltLength, saveSalt);

#pragma warning disable CS8604 // Possible null reference argument.
        string? result = SerializeValue(converter, input);
#pragma warning restore CS8604 // Possible null reference argument.

        if (saltLength == 0)
        {
            Assert.Equal(expected, result);
        }
        else
        {
            Assert.NotEqual(expected, result);
        }
    }

    [Theory]
    [InlineData("null", null)]
    [InlineData("\"\"", "")]
    [InlineData("\"abc\"", "abc")]
    public void HashMask_Read
    (
        string? input,
        string? expected
    )
    {
        LiteralMaskStringJsonConverter converter = new();

#pragma warning disable CS8604 // Possible null reference argument.
        string? result = DeserializeValue(converter, input);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }
}
