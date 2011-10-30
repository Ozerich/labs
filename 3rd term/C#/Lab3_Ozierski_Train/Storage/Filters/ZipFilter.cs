using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Lab2_Ozierski_Train.Storage.Filters
{
    class ZipFilter : IFilter
    {
        public Stream Apply(Stream stream, FilterMode fm)
        {
            GZipStream zipstream = new GZipStream(stream, fm == FilterMode.Write ? CompressionMode.Compress : CompressionMode.Decompress);
            return zipstream;
        }
    }
}
