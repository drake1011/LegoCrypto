using System;
using System.Text;

namespace LegoCrypto.Data.Crypto
{
    public static class CharCrypto
    {
        private const string PWDBase = "UUUUUUU(c) Copyright LEGO 2014AA";

        private static readonly byte[] baseBuff = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xb7,
                0xd5, 0xd7, 0xe6, 0xe7,
                0xba, 0x3c, 0xa8, 0xd8,
                0x75, 0x47, 0x68, 0xcf,
                0x23, 0xe9, 0xfe, 0xaa
            };

        //public CharCrypto(string uid)
        //{
        //    sUID = uid;
        //    Key = genkeybytes(sUID); 
        //}

        private static byte[] Genkeybytes(string uid)
        {
            byte[] buffer = new byte[16];
            Array.Copy(ScrambleByte(uid, 3, (byte[])baseBuff.Clone()), 0, buffer, 0, 4);
            Array.Copy(ScrambleByte(uid, 4, (byte[])baseBuff.Clone()), 0, buffer, 4, 4);
            Array.Copy(ScrambleByte(uid, 5, (byte[])baseBuff.Clone()), 0, buffer, 8, 4);
            Array.Copy(ScrambleByte(uid, 6, (byte[])baseBuff.Clone()), 0, buffer, 12, 4);
            return buffer;
        }

        public static string PWDGen(string uid)
        {
            byte[] buff = Encoding.ASCII.GetBytes(PWDBase);
            buff[30] = buff[31] = 0xAA;

            return Bitwise.ConvertByteArrayToHexString(ScrambleByte(uid, 8, buff));
        }

        public static string[] Encrypt(string uid, uint charId)
        {
            //if (Key == null) throw new Exception("Key not set!");

            var data = TEA.Encipher(new uint[] { charId, charId }, Genkeybytes(uid));

            // Get bytes in LE (reverse uint bytes)
            byte[] b = BitConverter.GetBytes(data[0]);
            byte[] b2 = BitConverter.GetBytes(data[1]);

            string s1 = Bitwise.ConvertByteArrayToHexString(b);
            string s2 = Bitwise.ConvertByteArrayToHexString(b2);

            return new string[] { s1, s2 };
        }

        public static uint Decrypt(string uid, string raw)
        {
            //if (Key == null) throw new Exception("Key not set!");

            byte[] Buff = new byte[8];

            Buff = Bitwise.ConvertHexStringToByteArray(raw);

            uint d1 = Bitwise.LE_To_UInt32(Buff, 0);
            uint d2 = Bitwise.LE_To_UInt32(Buff, 4);

            return (TEA.Decipher(new uint[] { d1, d2 }, Genkeybytes(uid))[0]);
        }

        private static byte[] ScrambleByte(string uid, int cnt, byte[] baseBuff)
        {
            var uidArray = Bitwise.ConvertHexStringToByteArray(uid);
            uidArray.CopyTo(baseBuff, 0);
            baseBuff[(cnt * 4) - 1] = 0xaa;

            uint v2 = 0;
            for (var i = 0; i < cnt; i++)
            {
                var v4 = Bitwise.Rotr32(v2, 25);
                var v5 = Bitwise.Rotr32(v2, 10);
                var b = Bitwise.LE_To_UInt32(baseBuff, i * 4);

                v2 = (b + v4 + v5 - v2) >> 0;
            }

            byte[] returnbuff = new byte[4];
            Bitwise.UInt32_To_LE(v2, returnbuff);
            return returnbuff;
        }

        public static string ReturnTokenHex(uint charId)
        {
            byte[] buff = new byte[4];

            Bitwise.UInt32_To_LE(charId, buff);

            return Bitwise.ConvertByteArrayToHexString(buff);
        }

        public static uint ReturnTokenUint(string tokenHex)
        {
            return Bitwise.LE_To_UInt32(Bitwise.ConvertHexStringToByteArray(tokenHex));
        }
    }
}
