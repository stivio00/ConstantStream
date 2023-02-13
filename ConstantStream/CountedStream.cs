using System;
using System.IO;

namespace ConstantStream
{
    ///<Summary>Count the Bytes that passed throught</Summary>
    public class CountedStream : Stream
    {
        public int Count { get; private set; }
        private Stream _baseStream;

        public CountedStream(Stream baseStream, int chunkSize)
        {
            Count = 0;
            _baseStream = baseStream;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => Count;

        public override long Position { get => Count; set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int readed = _baseStream.Read(buffer, offset, count);
            Count += readed;

            return readed;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}

