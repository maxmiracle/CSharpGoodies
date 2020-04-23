using System;
using System.IO;

namespace StreamGoodies
{
    /// <summary>
    /// The stream wraps a stream. New stream let read length bytes.
    /// Initially it created for NetworkStream to read N bytes in upper layer of abstraction.
    /// </summary>
    public class StreamWrapperReadLen : Stream
    { 
        private long _position = 0;
        private readonly long _length;
        private readonly Stream _stream;

        public StreamWrapperReadLen(Stream stream, long length)
        {
            _stream = stream;
            _length = length;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position
        {
            get => _position;
            set => _position = value;
        }

        public override void Flush()
        {

        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_position + count > _length) count = (int)(_length - _position);
            var readBytes = _stream.Read(buffer, offset, count);
            _position += readBytes;
            return readBytes;
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
