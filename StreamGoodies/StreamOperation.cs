using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StreamGoodies
{
    /// <summary>
    /// Operations with a stream.
    /// </summary>
    public static class StreamOperation
    {
        /// <summary>
        /// Calculate hash SHA256 for given stream.
        /// </summary>
        /// <param name="stream">Stream to calculate hash.</param>
        /// <returns>Computed hash code. (32 bytes)</returns>
        public static byte[] SHA256(this Stream stream)
        {
            HashAlgorithm hasher = System.Security.Cryptography.SHA256.Create();
            hasher.Initialize();

            stream.Position = 0;
            int chunkSize = 256;
            byte[] buffer = new byte[chunkSize];
            int readBytes;
            do
            {
                readBytes = stream.Read(buffer, 0, chunkSize);
                hasher.TransformBlock(buffer, 0, readBytes, null, 0);
            } while (readBytes > 0);
            hasher.TransformFinalBlock(new byte[0], 0, 0);
            return hasher.Hash;
        }

        public static bool Equals(byte[] a1, byte[] a2)
        {
            if (a1 == a2)
            {
                return true;
            }
            if ((a1 != null) && (a2 != null))
            {
                if (a1.Length != a2.Length)
                {
                    return false;
                }
                for (int i = 0; i < a1.Length; i++)
                {
                    if (a1[i] != a2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
