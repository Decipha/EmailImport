using System;
using System.IO;

namespace EmailImport
{
    public static class FileSignatures
    {
        public static Boolean IsPdf(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsPdf(stream);
            }
        }

        public static Boolean IsPdf(Stream stream)
        {
            // PDF (optionally ends with 0x25, 0x25, 0x45, 0x4f, 0x46 plus any combination of 0x0d, 0x0a)
            return CompareBytes(stream, 0, new byte[] { 0x25, 0x50, 0x44, 0x46 });
        }

        public static Boolean IsJpg(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsJpg(stream);
            }
        }

        public static Boolean IsJpg(Stream stream)
        {
            return CompareBytes(stream, 0, new byte[] { 0xff, 0xd8 });
        }

        public static Boolean IsTif(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsTif(stream);
            }
        }

        public static Boolean IsTif(Stream stream)
        {
            return (CompareBytes(stream, 0, new byte[] { 0x49, 0x49, 0x2a, 0x00 }) || CompareBytes(stream, 0, new byte[] { 0x4d, 0x4d, 0x00, 0x2a }));
        }

        public static Boolean IsPng(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsPng(stream);
            }
        }

        public static Boolean IsPng(Stream stream)
        {
            return CompareBytes(stream, 0, new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a });
        }

        public static Boolean IsGif(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsGif(stream);
            }
        }

        public static Boolean IsGif(Stream stream)
        {
            return CompareBytes(stream, 0, new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 });
        }

        public static Boolean IsBmp(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsBmp(stream);
            }
        }

        public static Boolean IsBmp(Stream stream)
        {
            return CompareBytes(stream, 0, new byte[] { 0x42, 0x4d });
        }

        public static Boolean IsThumbsDb(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return IsThumbsDb(stream);
            }
        }

        public static Boolean IsThumbsDb(Stream stream)
        {
            return (CompareBytes(stream, 512, new byte[] { 0xfd, 0xff, 0xff, 0xff }) && CompareBytes(stream, 524, new byte[] { 0x04, 0x00, 0x00, 0x00 }));
        }

        public static Boolean CompareBytes(Stream stream, int offset, Byte[] bytes)
        {
            if (bytes == null || stream == null)
                return false;

            if (bytes.Length > stream.Length)
                return false;

            stream.Seek(offset, SeekOrigin.Begin);

            foreach (int b in bytes)
            {
                if (stream.ReadByte() != b)
                    return false;
            }

            return true;
        }
    }
}
