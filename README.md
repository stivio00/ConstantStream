# ConstantStream
[![NuGet version (ConstanStream)](https://img.shields.io/nuget/v/constantStream.svg?style=flat-square)](https://www.nuget.org/packages/ConstantStream)

A collection of fake streams for testing purposes.

## Types
The library have three fake stream types:
* ConstantByteStream: A fake stream that generate a finite set of the same bytes.
* ConstantStrideStream: A fake stream that generate a byte stride in a loop.
* TimedStream: A fake constant byte stream that waits in determined positions.

Utility streams:
* SubStream: a substream is a small window to the base stream.
* CountedStream: record all the bytes readed.

For example:
```c#
// The constant byte stream
var cbs = ConstantByteStream(5, (byte) 'A'); 
// Should return  "AAAAA"
string text = (new StreamReader(cbs)).ReadToEnd() ;

// The constant stride stream
// Should read as "ABCABCABCA"
ConstantStrideStream(10, Encoding.UTF8.GetBytes("ABC")); 

// The TimedStream: just like a constant byte stream
var timedStream = new TimedStream(1024, (byte)'c'); 

// add a wait in position 0 of 200 ms
timedStream.Delays.Add(0, TimeSpan.FromMilliseconds(200)); 
// add a wait in position 15 of 250 ms
timedStream.Delays.Add(15, TimeSpan.FromMilliseconds(250)); 
// add a wait in position 1000 of 1 min
timedStream.Delays.Add(1000, TimeSpan.FromMinutes(1)); 
// the timedStream should take about 1 minute + 450 ms + reading overhead(ms) to read in total
```

## Usage
```c#
using ConstantStream;

// Creates a 1 Mb stream of zeroes.
var zeroesStream = new ConstantByteStream(1024*1024, (byte)0);

// Creates a 1 Gb stream full of 's'.
var sStream = new ConstantByteStream(1024*1024*1024, (byte)'s');

// Handy factory methods (From*) for ConstantByteStream
// 1 Mb of zeroes
var zeroesStreamEasy = ConstantByteStream.FromZeroes(1024*1024);
// 1 Kb of ones 
var onesStreamEasy = ConstantByteStream.FromOnes(1024); 
// 42 bytes of 'a'
var onesStreamEasy = ConstantByteStream.FromFromA(42); 


// Handy factory methods (From*) for ConstantStrideStream
// 01234567890123456... numbers from 0 to 9 in a loop
var zeroesStreamEasy = ConstantStrideStream.FromNumbers(1024*1024); 
// abcdefghijkl.... alphabet in a loop
var zeroesStreamEasy = ConstantStrideStream.FromAlphabet(1024); 
```

## Info
The Stream doesnt have a base stream. Its a fake generator and provide a simple 
Stream that generates large amounts of data.
It can be used with stream readers like TextReader, copyTo(file), HttpBody, crypto readers, and compression readers.

## Well Know Hashes
| ConstantStream | Size | MD5 Hex | MD5 Base64 | SHA1 Hex | SHA1 Base65 |
| --- | --- | --- | --- | --- | --- |
| FromAlphabet | 1 KB   | 930053E6D45C93FF2666C98BCD9610A1 | kwBT5tRck/8mZsmLzZYQoQ== | C167BE12C4F34DA6C4854C46A85627116B5E95FA | wWe+EsTzTabEhUxGqFYnEWtelfo= |
| FromAlphabet | 1 MB   | B63BA06DE0E8A9626D5BCF27E93BF32D | tjugbeDoqWJtW88n6TvzLQ== | DD89D1965604BD939EC68A6CA4552788F0EB1F88 | 3YnRllYEvZOexopspFUniPDrH4g= |
| FromAlphabet | 10 MB  | D3344B5243DB57263EF171B989C41BE4 | 0zRLUkPbVyY+8XG5icQb5A== | 2EB34A9141CE511DB12FA1BD24EB3F2E774D126C | LrNKkUHOUR2xL6G9JOs/LndNEmw= |
| FromAlphabet | 100 MB | E19E759A14BF22353BAB93550B87472A | 4Z51mhS/IjU7q5NVC4dHKg== | B3BED9CEDB044AC81E6774D5EEBA5FBE6BCDF954 | s77ZztsESsgeZ3TV7rpfvmvN+VQ= |
| FromAlphabet | 500 MB | C4A4DF0923DEE72ED2A3518F65EDA1F5 | xKTfCSPe5y7So1GPZe2h9Q== | 70048729EBB60D5EAA2A8CFC523AF1367282F6DD | cASHKeu2DV6qKoz8UjrxNnKC9t0= |
| FromAlphabet | 1 GB   | B472DC36AB28968EFBB4EC626BC98702 | tHLcNqsolo77tOxia8mHAg== | 0AC6BC4031B4E87FE60E1AAD28BA40C429F4F010 | Csa8QDG06H/mDhqtKLpAxCn08BA= |