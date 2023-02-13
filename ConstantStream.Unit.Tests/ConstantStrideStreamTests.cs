using FluentAssertions;
using System.Security.Cryptography;
using System.Text;

namespace ConstantStream.Unit.Tests;

public class ConstantStrideStreamTests
{
    [Fact]
    public void ReadStream_ConstructSize3ContentLetterA_Reads3timesCharacterA()
    {
        var expected = "ABCABCABCA";
        var sut = new ConstantStrideStream(10, Encoding.UTF8.GetBytes("ABC"));
        var reader = new StreamReader(sut);

        var result = reader.ReadToEnd();

        result.Should().Be(expected);
    }

    [Fact]
    public void ReadStream_ConstructAnEmptyStream_ReadsSizeShouldBeZero()
    {
        var expected = "";
        var sut = new ConstantByteStream(0, (byte)'x');
        var reader = new StreamReader(sut);

        var result = reader.ReadToEnd();

        result.Should().Be(expected);
    }

    [Fact( Skip = "Only to generate table in README.md")]
    public void ComputeWellKnowHash()
    {
        Dictionary<string, ConstantStrideStream> tests = new(){
            ["1 kb"] =  ConstantStrideStream.FromAlphabet(1024),
            ["1 mb"] =  ConstantStrideStream.FromAlphabet(1024*1024),
            ["10 mb"] = ConstantStrideStream.FromAlphabet(10* 1024*1024),
            ["100 mb"] = ConstantStrideStream.FromAlphabet(100* 1024*1024),
            ["500 mb"] = ConstantStrideStream.FromAlphabet(500* 1024*1024),
            ["1 gb"] = ConstantStrideStream.FromAlphabet(1024* 1024*1024),
        };
         
        foreach (var label in tests.Keys)
        {
            //using var hasher = MD5.Create();
            using var hasher = SHA1.Create();

            var hashedBytes = hasher.ComputeHash(tests[label]);
            var hex = Convert.ToHexString(hashedBytes);
            var base64 = Convert.ToBase64String(hashedBytes);
            var md = $" {label}    {hex} | {base64} ";
        }
    }
}