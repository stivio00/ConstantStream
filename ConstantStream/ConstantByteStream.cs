using System;
using System.IO;

namespace ConstantStream
{
    ///<Summary>Constant byte stream that mimic a NetworkStream.</Summary>
    public class ConstantByteStream : Stream
    {
        private int _position;
        private int _size;
        private byte _content;

        public ConstantByteStream(int size, byte content)
        {
            _position = 0;
            _size = size;
            _content = content;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => throw new NotImplementedException();

        public override long Position { get => _position; set => throw new NotImplementedException(); }

        public static ConstantByteStream FromZeroes(int size)
        {
            return new ConstantByteStream(size, (byte)0);
        }

        public static ConstantByteStream FromOnes(int size)
        {
            return new ConstantByteStream(size, (byte)1);
        }

        public static ConstantByteStream FromCharacterA(int size)
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
                return 0;

            if (count > remaining)
            {
                for (int i = 0; i < remaining; i++)
                    buffer[i] = _content;

                _position += remaining;
                return remaining;
            }

            for (int i = 0; i < count; i++)
                buffer[i] = _content;

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

