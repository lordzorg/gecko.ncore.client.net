using System;
using System.IO;

namespace Gecko.NCore.Client.Documents.V2
{
	public class ChunkStream : Stream
	{
		private readonly DocumentsClient _client;
		private readonly Func<byte[]> _readNextChunk;
		private int _chunkPosition;
		private byte[] _readChunk = new byte[0];
		private readonly EphorteIdentity _ephorteIdentity;
		private readonly Guid _chunkToken;
		private readonly long _length;

		public ChunkStream(Func<byte[]> readNextChunk)
		{
			_readNextChunk = readNextChunk;
		}

		public ChunkStream(DocumentsClient client, EphorteIdentity ephorteIdentity, Guid chunkToken, int contentLength)
			: this(() => client.ReadDocumentChunk(ephorteIdentity, chunkToken))
		{
			_client = client;
			_ephorteIdentity = ephorteIdentity;
			_chunkToken = chunkToken;
			_length = contentLength;
		}

		public override void Flush()
		{}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
				throw new ArgumentNullException("buffer");

			if (count < 0)
				throw new ArgumentOutOfRangeException("count", "Count should not be negative");

			if (offset < 0)
				throw new ArgumentOutOfRangeException("offset", "Offset should not be negative");

			if (count + offset > buffer.Length)
				throw new ArgumentException("The sum of offset and count is larger than the buffer length");

			var numOutputBufferBytesCopied = 0;

			do
			{
				bool everythingReadFromReadChunk = _readChunk.Length == _chunkPosition;
				if (everythingReadFromReadChunk)
				{
					_readChunk = _readNextChunk();
					_chunkPosition = 0;
				}

				if (_readChunk.Length == 0)
					return numOutputBufferBytesCopied;

				var numChunkBytesLeft = _readChunk.Length - _chunkPosition;
				var numOutputBufferBytesLeft = count - numOutputBufferBytesCopied;
				var numBytesToCopy = Math.Min(numChunkBytesLeft, numOutputBufferBytesLeft);
				Buffer.BlockCopy(_readChunk, _chunkPosition, buffer, numOutputBufferBytesCopied + offset, numBytesToCopy);
				numOutputBufferBytesCopied += numBytesToCopy;
				_chunkPosition += numBytesToCopy;
			} while (numOutputBufferBytesCopied < count);

			return numOutputBufferBytesCopied;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override long Length
		{
			get { return _length; }
		}

		public override long Position { get; set; }

		protected override void Dispose(bool disposing)
		{
			_client.EndDocumentRead(_ephorteIdentity, _chunkToken);
			using(_client){}
		}
	}
}