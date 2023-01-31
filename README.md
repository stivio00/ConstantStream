# ConstantStream
A constant streams for testing purposes.

## Types
The library have two Stream types:
* ConstantByteStream : a fake stream that generate a finite set of the same bytes.
* ConstantStrideStream : a fake stream that generate a byte stride in a loop.

For example:
ConstantByteStream(5, (byte) 'A') -> "AAAAA"
ConstantStrideStream(10, Encoding.UTF8.GetBytes("ABC")) -> "ABCABCABCA"

## Usage
```c#
// Creates a 1 Mb stream of zeroes.
var zeroesStream = new ConstantByteStream(1024*1000, (byte)0);

// Creates a 1 Gb stream full of 's'.
var sStream = new ConstantByteStream(1024*1000*1000, (byte)'s');

// Handy factory method (From*)
var zeroesStreamEasy = ConstantByteStream.FromZeroes(1024*1000);
 
```

## Info
The Stream doesnt have a base stream. Its a fake generator and provide a simple 
Stream that generates large amounts of data.