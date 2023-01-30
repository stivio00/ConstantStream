using System;
using System.IO;

namespace ConstantStream
{
public class ConstantStream : Stream
{
    private int _position;
    private int _size;
    private byte _content;

    public ConstantStream(int size, byte content)
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

    public override void Flush()
    {
        throw new NotImplementedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var remaining = _size - _position;

        if (remaining == 0)
            return -1;

        if (remaining < count)
        {
            for (int i = 0; i < remaining; i += 1)
                buffer[i] = _content;

            _position += remaining;
            return remaining;
        }

        for (int i = 0; i < count; i += 1)
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

