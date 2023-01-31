using System;
using System.IO;
using System.Linq;

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

        public static ConstantStrideStream FromNumbers(int size)
        {
            var numbers = new char[] {'0','1','2','3','4','5','6','7','8','9'};
            
            return new ConstantStrideStream(size, numbers.Cast<byte>().ToArray());
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

