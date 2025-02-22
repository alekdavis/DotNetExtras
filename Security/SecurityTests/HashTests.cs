using DotNetExtras.Security;

namespace SecurityTests;
public class HashTests
{
    [Fact]
    public void Generate_Success()
    {
        string? plainText;
        string? hashText;
        string? salt;

        plainText = "hello";
        hashText = Hash.Generate(HashType.SHA256, plainText);
        Assert.Equal("2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", hashText);
        hashText = Hash.Generate(HashType.SHA384, plainText);
        Assert.Equal("59e1748777448c69de6b800d7a33bbfb9ff1b463e44354c3553bcdb9c666fa90125a3c79f90397bdf5f6a13de828684f", hashText);
        hashText = Hash.Generate(HashType.SHA512, plainText);
        Assert.Equal("9b71d224bd62f3785d96d46ad3ea3d73319bfbc2890caadae2dff72519673ca72323c3d99ba5c11d7c7acc6e14b8c5da0c4663475c2e5c3adef46f73bcdec043", hashText);

        plainText = "";
        hashText = Hash.Generate(HashType.SHA256, plainText);
        Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", hashText);
        hashText = Hash.Generate(HashType.SHA384, plainText);
        Assert.Equal("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", hashText);
        hashText = Hash.Generate(HashType.SHA512, plainText);
        Assert.Equal("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", hashText);
  
        salt = "abcdef";

        plainText = "hello";
        hashText = Hash.Generate(HashType.SHA256, plainText, salt);
        Assert.Equal("da519bb78a3ac7f31ad576871b301eb18bf346ad96580111ad13b2e3fd9bd83a", hashText);
        hashText = Hash.Generate(HashType.SHA384, plainText, salt);
        Assert.Equal("17fa36c5c2e0c5cfa88fe1ee9862b775317221a203d2c5411e3c0d96cc8ee1c8bcb546d2f58907ece0997d96d883696e", hashText);
        hashText = Hash.Generate(HashType.SHA512, plainText, salt);
        Assert.Equal("f9b18d03cf3e80b5958ba77b600de8b177542036a0da7fe4ea5025ca5a12da1c199b5b6edd08c51bd97ffb343754005588ccf0fd148d7f7b182a8442ab4942bd", hashText);

        plainText = "";
        hashText = Hash.Generate(HashType.SHA256, plainText, salt);
        Assert.Equal("bef57ec7f53a6d40beb640a780a639c83bc29ac8a9816f1fc6c5c6dcd93c4721", hashText);
        hashText = Hash.Generate(HashType.SHA384, plainText, salt);
        Assert.Equal("c6a4c65b227e7387b9c3e839d44869c4cfca3ef583dea64117859b808c1e3d8ae689e1e314eeef52a6ffe22681aa11f5", hashText);
        hashText = Hash.Generate(HashType.SHA512, plainText, salt);
        Assert.Equal("e32ef19623e8ed9d267f657a81944b3d07adbb768518068e88435745564e8d4150a0a703be2a7d88b61e3d390c2bb97e2d4c311fdc69d6b1267f05f59aa920e7", hashText);

        plainText = "";
        hashText = Hash.Generate(HashType.SHA256, plainText, salt);
        Assert.Equal("bef57ec7f53a6d40beb640a780a639c83bc29ac8a9816f1fc6c5c6dcd93c4721", hashText);
        hashText = Hash.Generate(HashType.SHA384, plainText, salt);
        Assert.Equal("c6a4c65b227e7387b9c3e839d44869c4cfca3ef583dea64117859b808c1e3d8ae689e1e314eeef52a6ffe22681aa11f5", hashText);
        hashText = Hash.Generate(HashType.SHA512, plainText, salt);
        Assert.Equal("e32ef19623e8ed9d267f657a81944b3d07adbb768518068e88435745564e8d4150a0a703be2a7d88b61e3d390c2bb97e2d4c311fdc69d6b1267f05f59aa920e7", hashText);
    }

    [Theory]
    [InlineData(HashType.SHA256, "hello", 0)]
    [InlineData(HashType.SHA256, "hello", 4)]
    [InlineData(HashType.SHA384, "hello", 0)]
    [InlineData(HashType.SHA384, "hello", 8)]
    [InlineData(HashType.SHA512, "hello", 0)]
    [InlineData(HashType.SHA512, "hello", 12)]
    public void ToHashVerify_Success
    (
        HashType hashType,
        string plainText,
        int saltLength
    )
    {
        string hashText = plainText.ToHash(hashType, saltLength);

        Assert.True(Hash.Verify(hashType, plainText, hashText));
        Assert.False(Hash.Verify(hashType, plainText + "a", hashText));
        Assert.False(Hash.Verify(hashType, plainText, hashText + "34"));
    }
}
