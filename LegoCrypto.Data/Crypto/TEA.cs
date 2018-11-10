using SysLib.Bitwise;

namespace LegoCrypto.Data.Crypto
{
    public static class TEA
    {
        private const uint
            delta = 0x9e3779b9,
            d_sum = 0xC6EF3720;

        //expanded key cache
        private static uint k0, k1, k2, k3;

        private static void SetKey(byte[] key)
        {
            k0 = key.ToUint(0);
            k1 = key.ToUint(4);
            k2 = key.ToUint(8);
            k3 = key.ToUint(12);
        }

        public static uint[] Encipher(uint[] v, byte[] key)
        {
            SetKey(key);
            /* set up */
            uint v0 = v[0];
            uint v1 = v[1];
            uint sum = 0;

            for (int i = 0; i < 32; i++)
            {
                sum += delta;
                v0 += ((v1 << 4) + k0) ^ (v1 + sum) ^ ((v1 >> 5) + k1);
                v1 += ((v0 << 4) + k2) ^ (v0 + sum) ^ ((v0 >> 5) + k3);
            }

            v[0] = v0;
            v[1] = v1;

            return v;
        }

        public static uint[] Decipher(uint[] v, byte[] key)
        {
            SetKey(key);
            /* set up */
            uint v0 = v[0];
            uint v1 = v[1];
            uint sum = d_sum;

            for (int i = 0; i < 32; i++)
            {
                v1 -= ((v0 << 4) + k2) ^ (v0 + sum) ^ ((v0 >> 5) + k3);
                v0 -= ((v1 << 4) + k0) ^ (v1 + sum) ^ ((v1 >> 5) + k1);
                sum -= delta;
            }

            v[0] = v0;
            v[1] = v1;

            return v;
        }
    }
}
