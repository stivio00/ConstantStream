using FluentAssertions;
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
}