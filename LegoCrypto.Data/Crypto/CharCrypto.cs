using System;
using System.Text;
using SysLib.Bitwise;

namespace LegoCrypto.Data.Crypto
{
    internal static class CharCrypto
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

        private static byte[] Genkeybytes(string uid)
        {
            byte[] buffer = new byte[16];
            Array.Copy(ScrambleByte(uid, 3, (byte[])baseBuff.Clone()), 0, buffer, 0, 4);
            Array.Copy(ScrambleByte(uid, 4, (byte[])baseBuff.Clone()), 0, buffer, 4, 4);
            Array.Copy(ScrambleByte(uid, 5, (byte[])baseBuff.Clone()), 0, buffer, 8, 4);
            Array.Copy(ScrambleByte(uid, 6, (byte[])baseBuff.Clone()), 0, buffer, 12, 4);
            return buffer;
        }

        internal static string PWDGen(string uid)
        {
            var buffer = Encoding.ASCII.GetBytes(PWDBase);
            buffer[30] = buffer[31] = 0xAA;
            return ScrambleByte(uid, 8, buffer).ToHex();
        }

        internal static string[] Encrypt(string uid, uint charId)
        {
            var data = TEA.Encipher(new uint[] { charId, charId }, Genkeybytes(uid));
            var b = data[0].ToBytes();
            var b2 = data[1].ToBytes();
            return new string[] { b.ToHex(), b2.ToHex() };
        }

        internal static uint Decrypt(string uid, string raw)
        {
            var buffer = raw.ToBytes();
            var d1 = buffer.ToUint(0);
            var d2 = buffer.ToUint(4);
            return (TEA.Decipher(new uint[] { d1, d2 }, Genkeybytes(uid))[0]);
        }

        private static byte[] ScrambleByte(string uid, int cnt, byte[] baseBuff)
        {
            uid.ToBytes().CopyTo(baseBuff, 0);
            baseBuff[(cnt * 4) - 1] = 0xaa;

            uint v2 = 0;
            for (var i = 0; i < cnt; i++)
            {
                var v4 = ByteConverter.Rotr32(v2, 25);
                var v5 = ByteConverter.Rotr32(v2, 10);
                var b = baseBuff.ToUint( i * 4);

                v2 = (b + v4 + v5 - v2) >> 0;
            }
            return v2.ToBytes();
        }

        internal static string ReturnTokenHex(uint charId)
        {
            return HexConverter.BytesToHex(charId.ToBytes());
        }

        internal static uint ReturnTokenUint(string tokenHex)
        {
            return tokenHex.ToBytes().ToUint();
        }
    }
}
