using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Lab2_Ozierski_Train.Storage.Filters
{
    class CryptFilter : IFilter
    {
        public Stream Apply(Stream stream, FilterMode fm)
        {
            DES des = DES.Create();
            CryptoStream cs = new CryptoStream(stream, des.CreateEncryptor(), fm == FilterMode.Write ? CryptoStreamMode.Write : CryptoStreamMode.Read);
            return cs;
        }
    }
}
