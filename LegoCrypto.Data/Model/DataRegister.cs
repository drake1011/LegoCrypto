using System;
using System.Collections;

namespace LegoCrypto.Data.Model
{
    public enum DataRegister
    {
        Page35 = 0,
        Page36,
        Page37,
        Page38,
        Page43
    }

    public static class PageConstants
    {
        public const string DefaultEmpty = "00000000";
        public const string CharacterType = DefaultEmpty;
        public const string TokenType = "00010000";
    }

    public class DataRegisterCollection : IEnumerable
    {
        private readonly string[] _pages;

        public DataRegisterCollection() => _pages = new string[Enum.GetNames(typeof(DataRegister)).Length];

        public string this[DataRegister dp]
        {
            get => _pages[(int)dp];
            set => _pages[(int)dp] = value;
        }

        public IEnumerator GetEnumerator() => new DataRegisterEnumerator(_pages);
    }

    public class DataRegisterEnumerator : IEnumerator
    {
        private string[] _pages;
        private int _pos = -1;

        public DataRegisterEnumerator(string[] pages) => _pages = pages;

        public bool MoveNext()
        {
            _pos++;
            return (_pos < _pages.Length);
        }

        public void Reset() => _pos = -1;

        public object Current
        {
            get
            {
                try
                {
                    return _pages[_pos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
