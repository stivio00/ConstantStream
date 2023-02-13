using System;
using System.IO;

namespace ConstantStream
{
    ///<Summary>Constant byte stream that mimic a NetworkStream.</Summary>
    public class SubStream : Stream
    {
        private int _position;
        private int _size;
        private int _chunkReaded;
        private Stream _baseStream;
        public bool IsChunkEnded { get; private set; }

        public SubStream(Stream baseStream, int maxChunk)
        {
            _position = 0;
            _chunkReaded = 0;
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
            int remaining = _size - _position;
            if (remaining == 0) 
            {
                IsChunkEnded = true;
                return 0;
            }

            int readed = _baseStream.Read(buffer, offset, Math.Min(count,remaining));
            IsChunkEnded = readed == 0;
            if (!IsChunkEnded)
            {
                _chunkReaded += readed;
                _position += readed;
                return readed;
            }
            
            return 0;
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

        public void MoveToNextChunk()
        {
            if (!IsChunkEnded){
                throw new Exception("The stream have not ended. Resetting will cause bad behavior");
            }

            _chunkReaded = 0;
            _position = 0;
        }
        
    }
}

