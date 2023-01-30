using FluentAssertions;

namespace ConstantStream.Unit.Tests;

public class ConstantStreamTests
{
    [Fact]
    public void ReadStream_ConstructSize3ContentLetterA_Reads3timesCharacterA()
    {
        var expected = "aaa";
        var sut = new ConstantStream(3, (byte)'a');
        var reader = new StreamReader(sut);

        var result = reader.ReadToEnd();

        "aaa".Should().Be(expected);

    }
}