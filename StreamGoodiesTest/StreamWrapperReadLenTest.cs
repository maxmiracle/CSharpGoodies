using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;


namespace StreamGoodies
{
    [TestClass]
    public class StreamWrapperReadLenTest
    {
        /// <summary>
        /// Test creating wrapper for Stream.
        /// </summary>
        [TestMethod]
        public void Read10bytes()
        {
            // Init
            var bytes = Enumerable.Range(0, 19).Select(x => (byte)x).ToArray();
            var memoryStream = new MemoryStream(bytes, false);

            // Act
            // Check wrapping from postion 5 with len = 10 bytes.
            memoryStream.Position = 5; // 
            var newStream = new StreamWrapperReadLen(memoryStream, 10);
            MemoryStream mem2 = new MemoryStream();
            newStream.CopyTo(mem2); // copy all data to mem2
            var mem2arr = mem2.ToArray();

            // Create result logically to check result from StreamWrappedReadLen
            var resultBytes = Enumerable.Range(5, 10).Select(x => (byte)x).ToArray();
            var resultStream = new MemoryStream(resultBytes, false);

            // Assert
            resultStream.Position = 0;
            mem2.Position = 0;
            byte[] hash1 = resultStream.SHA256();
            byte[] hash2 = mem2.SHA256();
            Assert.IsTrue(StreamOperation.Equals(hash1, hash2));
        }
    }
}
