using System.Text;
using FluentAssertions;

namespace ConstantStream.Unit.Tests;

public class SubStreamTests
{
    [Fact]
    public void ChunkBy8_GivenAStrideStream_EachChunkIscorrect()
    {
        var expected = new List<string>{"abca", "bcab", "cabc", "abca"};
        var strideStream = new ConstantStrideStream(16, Encoding.UTF8.GetBytes("abc"));
        SubStream subStream = new SubStream(strideStream, 4);

        var reader = new StreamReader(subStream);
        reader.ReadToEnd().Should().BeEquivalentTo(expected[0]);

        subStream.MoveToNextChunk();
        reader.ReadToEnd().Should().BeEquivalentTo(expected[1]);

        subStream.MoveToNextChunk();
        reader.ReadToEnd().Should().BeEquivalentTo(expected[2]);

        subStream.MoveToNextChunk();
        reader.ReadToEnd().Should().BeEquivalentTo(expected[3]);
    }


}