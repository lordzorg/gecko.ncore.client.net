using System;
using System.IO;
using System.Linq;
using System.Text;
using Gecko.NCore.Client.Documents.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Documents.V2
{
	[TestClass]
	public class ChunkStreamTests
	{
		[TestMethod]
		public void Read_ReadBufferSizeLargerThanSourceContent()
		{
			const int sourceContentSize = 500;
			const int readBufferSize = 1024;
			const int chunkSize = 20;
			
			ReadAndAssertContent(sourceContentSize, chunkSize, readBufferSize);
		}

		[TestMethod]
		public void Read_ReadBufferSizeMultipleOfContent()
		{
			const int sourceContentSize = 256;
			const int readBufferSize = 128;
			const int chunkSize = 64;
			
			ReadAndAssertContent(sourceContentSize, chunkSize, readBufferSize);
		}

		[TestMethod]
		public void Read_ReadBufferSizeSmallerThanContent()
		{
			const int sourceContentSize = 256;
			const int readBufferSize = 128;
			const int chunkSize = 20;
			
			ReadAndAssertContent(sourceContentSize, chunkSize, readBufferSize);
		}

		[TestMethod]
		public void Read_ReadChunkLargerThanReadBuffer()
		{
			const int sourceContentSize = 600;
			const int readBufferSize = 128;
			const int chunkSize = 256;
			
			ReadAndAssertContent(sourceContentSize, chunkSize, readBufferSize);
		}

		[TestMethod]
		public void Read_ChunkSizeLargerThanSourceContentSize()
		{
			const int sourceContentSize = 50;
			const int readBufferSize = 128;
			const int chunkSize = 256;
			
			ReadAndAssertContent(sourceContentSize, chunkSize, readBufferSize);
		}

		[TestMethod]
		public void Read_FromOffset()
		{
			const int sourceContentSize = 100;
			const int readBufferSize = 128;
			const int chunkSize = 50;
			const int offset = 20;

			var sourceString = string.Join("", Enumerable.Repeat("x", sourceContentSize));
			var sourceBytes = Encoding.UTF8.GetBytes(sourceString);
			var position = 0;
			Func<byte[]> readNextChunk = () =>
			{
				var bytes = sourceBytes.Skip(position).Take(chunkSize).ToArray();
				position = position + chunkSize;
				return bytes;
			};

			var chunkStream = new ChunkStream(readNextChunk);

			var readBufferString = string.Join("", Enumerable.Repeat("_", readBufferSize));
			var readBuffer = Encoding.UTF8.GetBytes(readBufferString);
			chunkStream.Read(readBuffer, offset, readBufferSize - offset);

			var resultString = Encoding.UTF8.GetString(readBuffer);
			Assert.AreEqual(20, resultString.IndexOf('x'));
			Assert.AreEqual(sourceContentSize + offset - 1, resultString.LastIndexOf('x'));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Read_CountMinusOffsetLargerThanBuffer_ThrowException()
		{
			var stream = new ChunkStream(() => new byte[0]);
			stream.Read(new byte[100], 20, 81);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Read_BufferIsNull_ThrowException()
		{
			var stream = new ChunkStream(() => new byte[0]);
			stream.Read(null, 0, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Read_CountIsNegative_ThrowException()
		{
			var stream = new ChunkStream(() => new byte[0]);
			stream.Read(new byte[0], 0, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Read_OffsetIsNegative_ThrowException()
		{
			var stream = new ChunkStream(() => new byte[0]);
			stream.Read(new byte[0], -1, 0);
		}

		[TestMethod]
		public void Length()
		{
			var stream = new ChunkStream(new DocumentsClient(), new EphorteIdentity(), Guid.NewGuid(), 256);
			Assert.AreEqual(256, stream.Length);
		}

		private static void ReadAndAssertContent(int sourceContentSize, int chunkSize, int readBufferSize)
		{
			var sourceString = string.Join("", Enumerable.Repeat("x", sourceContentSize));
			string result = ReadChunkStream(sourceString, chunkSize, readBufferSize);
			Assert.AreEqual(sourceString, result);
		}

		private static string ReadChunkStream(string sourceString, int chunkSize, int readBufferSize)
		{
			var sourceBytes = Encoding.UTF8.GetBytes(sourceString);
			var position = 0;
			Func<byte[]> readNextChunk = () =>
			{
				var bytes = sourceBytes.Skip(position).Take(chunkSize).ToArray();
				position = position + chunkSize;
				return bytes;
			};

			// Read buffer size larger than total source content size
			var streamReader = new StreamReader(new ChunkStream(readNextChunk), Encoding.UTF8, false, readBufferSize);
			return streamReader.ReadToEnd();
		}
	}
}
