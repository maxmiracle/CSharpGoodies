using System;
using System.IO;
using System.Text;

namespace StreamGoodies
{
    /// <summary>
    /// The GenerateStream is a stream with the spcified length and based on repeated pattern. 
    /// </summary>
    public class GenerateStream : Stream
    {

        private long _position = 0;
        private readonly long _length;
        private readonly int _patternLength;
        private int _patternPosition = 0;
        private readonly byte[] _pattern;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="length">Length of the stream.</param>
        /// <param name="pattern">Byte array repeated </param>
        public GenerateStream(long length, byte[] pattern)
        {
            _length = length;
            _pattern = pattern;
            _patternLength = _pattern.Length;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position
        {
            get
            {
                return _position;
            }
            set { throw new NotImplementedException(); }
        }

        public override void Flush()
        {

        }


        /// <summary>
        /// Read pattern repeatively.
        /// </summary>
        /// <param name="buffer">Buffer to write data.</param>
        /// <param name="offset">Offset in the buffer.</param>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Number of bytes written.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {

            if (count > _patternLength) count = _patternLength;
            if (count > (_patternLength - _patternPosition)) count = _patternLength - _patternPosition;
            if (_position + count > _length) count = (int)(_length - _position);
            if (count > 0) Array.Copy(_pattern, _patternPosition, buffer, offset, count);
            if (_patternPosition + count < _patternLength) _patternPosition += count;
            else _patternPosition = 0;
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
