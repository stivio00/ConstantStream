using System;
using System.IO;

namespace ConstantStream
{
    ///<Summary>Constant byte stream that mimic a NetworkStream.</Summary>
    public class SubStream : Stream
    {
        private int _position;
        private int _size;
        private Stream _baseStream;

        public SubStream(Stream baseStream, int maxChunk)
        {
            _position = 0;
            _size = maxChunk;
            _baseStream = baseStream;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _size;

        public override long Position { get => _position; set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int remaining = _position - count;
            int readed = _baseStream.Read(buffer, offset, Math.Min(count,remaining));
            _position += readed;
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

