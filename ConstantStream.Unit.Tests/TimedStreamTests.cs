using FluentAssertions;

namespace ConstantStream.Unit.Tests;

public class timedStreamsTests
{
    [Fact]
    public void ReadTimeStream_With5secondsDelay_ShouldTakeAtLeat5seconds()
    {
        var sut = new TimedStream(3, (byte)'a');
        sut.Delays.Add(0, TimeSpan.FromSeconds(5));
        var reader = new StreamReader(sut);

        Action readingAction = () => reader.ReadToEnd();

        readingAction.ExecutionTime().Should().BeGreaterThan(TimeSpan.FromSeconds(5));
    }
}