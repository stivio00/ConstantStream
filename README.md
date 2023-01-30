# ConstantStream
A constant stream for testing purposes

## Usage
```c#
// creates a 1 Mb stream of zeroes.
var zeroesStream = new ConstantStream(1024*1000, (byte)0);

// creates a 1 Gb stream full of 's'.
var sStream = new ConstantStream(1024*1000*1000, (byte)'s');

```

## Info
The Stream doesnt have a base stream. Its a fake generator.