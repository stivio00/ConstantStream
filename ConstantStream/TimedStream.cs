using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConstantStream
{
    ///<Summary>Stream with deterministic time waits.</Summary>
    public class TimedStream : Stream
    {
        private int _position;
        private int _size;
        private byte _content;

        public Dictionary<int, TimeSpan> Delays { get; private set; }

        public TimedStream(int size, byte content)
        {
            _position = 0;
            _size = size;
            _content = content;
            Delays = new Dictionary<int, TimeSpan>();
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => throw new NotImplementedException();

        public override long Position { get => _position; set => throw new NotImplementedException(); }

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
                    CheckAndWaitForByte(_position+i);
                    buffer[i] = _content;
                }
                _position += remaining;
                return remaining;
            }

            for (int i = 0; i < count; i++)
            {
                CheckAndWaitForByte(_position+i);
                buffer[i] = _content;
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

        private void CheckAndWaitForByte(int position)
        {
            if (Delays.ContainsKey(position))
            {
                Thread.Sleep(Delays[position]);
            }
        }
    }
}

