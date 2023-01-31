# ConstantStream
[![NuGet version (ConstanStream)](https://img.shields.io/nuget/v/constantStream.svg?style=flat-square)](https://www.nuget.org/packages/ConstantStream)

A collection of fake streams for testing purposes.

## Types
The library have tree stream types:
* ConstantByteStream: A fake stream that generate a finite set of the same bytes.
* ConstantStrideStream: A fake stream that generate a byte stride in a loop.
* TimedStream: A fake constant byte stream that waits in determined positions.

For example:
```c#
// The constant byte stream
var cbs = ConstantByteStream(5, (byte) 'A'); 
(new StreamReader(cbs).ReadToEnd() // Should return  "AAAAA"

// The constant stride stream
ConstantStrideStream(10, Encoding.UTF8.GetBytes("ABC")); // Should read as "ABCABCABCA"

// The TimedStream
var timedStream = new TimedStream(1024, (byte)'c'); // Just like a constant byte stream

timedStream.Delays.Add(0, TimeSpan.FromMilliseconds(200)); // add a wait in position 0 of 200 ms
timedStream.Delays.Add(15, TimeSpan.FromMilliseconds(250)); // add a wait in position 15 of 250 ms
timedStream.Delays.Add(1000, TimeSpan.FromMinutes(1)); // add a wait in position 1000 of 1 min
// the timedStream should take about 1 minute + 450 ms + reading overhead(ms) to read in total
```

## Usage
```c#
using ConstantStream;

// Creates a 1 Mb stream of zeroes.
var zeroesStream = new ConstantByteStream(1024*1024, (byte)0);

// Creates a 1 Gb stream full of 's'.
var sStream = new ConstantByteStream(1024*1020*1020, (byte)'s');

// Handy factory method (From*)
var zeroesStreamEasy = ConstantByteStream.FromZeroes(1024*1020);
var zeroesStreamEasy = ConstantStrideStream.FromNumbers(1024*1020); // 01234567890123456...
```

## Info
The Stream doesnt have a base stream. Its a fake generator and provide a simple 
Stream that generates large amounts of data.
It can be used with stream readers like TextReader, copyTo(file), HttpBody, crypto readers, and compression readers.