using FluentAssertions;

namespace ConstantStream.Unit.Tests;

public class timedStreamsTests
{
    [Fact]
    public void ReadStream_ConstructSize3ContentLetterA_Reads3timesCharacterA()
    {
        var expected = "aaa";
        var sut = new ConstantByteStream(3, (byte)'a');
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