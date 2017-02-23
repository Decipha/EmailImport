using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace EmailImport.Conversion
{
    internal class ImageProcessingArgs
    {
        internal enum OperationType
        {
            File,
            Bitmap,
            Stream,
            Concat
        }

        internal OperationType Operation { get; private set; }
        internal Object ImageData { get; private set; }
        internal ImageConversionOptions Options { get; private set; }

        internal Exception ExceptionObject { get; set; }
        internal ManualResetEvent SyncEvent { get; private set; }
        internal List<PageInfo> Pages { get; private set; }

        private ImageProcessingArgs(ImageConversionOptions options)
        {
            Pages = new List<PageInfo>();
            Options = options;
            SyncEvent = new ManualResetEvent(false);
        }

        public ImageProcessingArgs(String fileName, ImageConversionOptions options)
            : this(options)
        {
            ImageData = fileName;
            Operation = OperationType.File;
        }

        public ImageProcessingArgs(Stream stream, ImageConversionOptions options)
            : this(options)
        {
            ImageData = stream;
            Operation = OperationType.Stream;
        }

        public ImageProcessingArgs(Bitmap bitmap, ImageConversionOptions options)
            : this(options)
        {
            ImageData = bitmap;
            Operation = OperationType.Bitmap;
        }

        public ImageProcessingArgs(String fileName, IEnumerable<PageInfo> pages)
            : this(null)
        {
            ImageData = fileName;
            Pages.AddRange(pages);
            Operation = OperationType.Concat;
        }
    }
}
