using System;
using System.IO;

namespace ConstantStream
{
    ///<Summary>Constant byte sequence stream that mimic a NetworkStream.</Summary>
    public class ConstantStrideStream : Stream
    {
        private int _position;
        private int _size;
        private byte[] _stride;
        private int _strideCursor;

        public ConstantStrideStream(int size, byte[] stride)
        {
            _position = 0;
            _size = size;
            _stride = stride;
            _strideCursor = 0;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;
        public override long Length => throw new NotImplementedException();

        public override long Position { get => _position; set => throw new NotImplementedException(); }

        public ConstantByteStream FromZeroes(int size)
        {
            return new ConstantByteStream(size, (byte)0);
        }

        public ConstantByteStream FromOnes(int size)
        {
            return new ConstantByteStream(size, (byte)1);
        }

        public ConstantByteStream FromCharacterA(int size)
        {
            return new ConstantByteStream(size, (byte)'a');
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int remaining = _size - _position;
            if (count == 0)
                return -1;

            if (count > remaining)
            {
                for (int i = 0; i < remaining; i++)
                {
                    buffer[i] = _stride[_strideCursor];
                    _strideCursor += 1;
                    _strideCursor %= _stride.Length;
                }

                _position += remaining;
                return remaining;
            }

            for (int i = 0; i < count; i++)
            {
                buffer[i] = _stride[_strideCursor];
                _strideCursor += 1;
                _strideCursor %= _stride.Length;
            }

            _position += count;

            return count;
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

