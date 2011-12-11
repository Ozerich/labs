using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Lab2_Ozierski_Train.Storage.Filters
{
    public class CryptFilter : IFilter
    {
        private byte[] iv;
        private byte[] key;

        public CryptFilter(byte[] IV, byte[] Key)
        {
            iv = IV;
            key = Key;
        }

        public Stream Apply(Stream stream, FilterMode fm)
        {
            Rijndael des = Rijndael.Create();
            des.IV = iv;
            des.Key = key;
            ICryptoTransform crypt = fm == FilterMode.Write ? des.CreateEncryptor() : des.CreateDecryptor();
            CryptoStream cs = new CryptoStream(stream, crypt, fm == FilterMode.Write ? CryptoStreamMode.Write : CryptoStreamMode.Read);
            return cs;
        }
    }
}
