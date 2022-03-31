using System;
using System.Runtime.InteropServices;

namespace UniqueIdentifier
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UID : IEquatable<UID>, IComparable, IComparable<UID>
    {
        public UID(Guid guid)
        {
            unsafe
            {
                byte* b = (byte *)&guid;

                _a = ((int)b[3] << 24) | ((int)b[2] << 16) | ((int)b[1] << 8) | b[0];
                _b = (short)(((int)b[5] << 8) | b[4]);
                _c = (short)(((int)b[7] << 8) | b[6]);
                _d = b[8];
                _e = b[9];
                _f = b[10];
                _g = b[11];
                _h = b[12];
                _i = b[13];
                _j = b[14];
                _k = b[15];
            }
        }

        public UID(byte[] b)
        {
            if (b == null)
                throw new ArgumentNullException("b");
            if (b.Length != 16)
                throw new ArgumentException("Unity.UID(byte[] b) requires 'b' size of 16 bytes.");

            _a = ((int)b[3] << 24) | ((int)b[2] << 16) | ((int)b[1] << 8) | b[0];
            _b = (short)(((int)b[5] << 8) | b[4]);
            _c = (short)(((int)b[7] << 8) | b[6]);
            _d = b[8];
            _e = b[9];
            _f = b[10];
            _g = b[11];
            _h = b[12];
            _i = b[13];
            _j = b[14];
            _k = b[15];
        }

#if UNITY_2021
        public UID(ReadOnlySpan<byte> b) { this = new Guid(b); }
#endif // UNITY_2021

        public UID(string g) { this = new Guid(g); }

        public UID(int a, short b, short c, byte[] d)
        {
            if (d == null)
                throw new ArgumentNullException("d");
            if (d.Length != 8)
                throw new ArgumentException("Unity.UID(int a, short b, short c, byte[] d) requires 'd' size of 8 bytes.");

            _a = a;
            _b = b;
            _c = c;
            _d = d[0];
            _e = d[1];
            _f = d[2];
            _g = d[3];
            _h = d[4];
            _i = d[5];
            _j = d[6];
            _k = d[7];
        }

        public UID(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
            _e = e;
            _f = f;
            _g = g;
            _h = h;
            _i = i;
            _j = j;
            _k = k;
        }

        public UID(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        {
            _a = (int)a;
            _b = (short)b;
            _c = (short)c;
            _d = d;
            _e = e;
            _f = f;
            _g = g;
            _h = h;
            _i = i;
            _j = j;
            _k = k;
        }

        /// <summary>Implementation to mirror <see cref="Guid.GetHashCode"/>.</summary>
        /// <returns>The UID's hash.</returns>
        public override int GetHashCode()
        {
            return _a ^ (((int)_b << 16) | (int)(ushort)_c) ^ (((int)_f << 24) | _k);
        }

        public override bool Equals(object obj) { return obj is UID UID && Equals(UID); }

        public bool Equals(UID other)
        {
            return _a == other._a &&
                   _b == other._b &&
                   _c == other._c &&
                   _d == other._d &&
                   _e == other._e &&
                   _f == other._f &&
                   _g == other._g &&
                   _h == other._h &&
                   _i == other._i &&
                   _j == other._j &&
                   _k == other._k;
        }

        public override string ToString()
        {
            return ToString(_a, _b, _c, _d, _e, _f, _g, _h, _i, _j, _k);     
        }

        /// <summary>dddddddd-dddd-dddd-dddd-dddddddddddd</summary>
        /// <returns>The formating id.</returns>
        internal static string ToString(int _a, short _b, short _c, byte _d, byte _e, byte _f, byte _g, byte _h, byte _i, byte _j, byte _k)
        {
            //                      A  A  A  A  A  A  A  A - B  B   B   B -  C   C   C   C -  D   D   E   E -  F   F   G   G   H   H   I  I    J   J   K   K
            const string format = "{0}{1}{2}{3}{4}{5}{6}{7}-{8}{9}{10}{11}-{12}{13}{14}{15}-{16}{17}{18}{19}-{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}";

            char _00 = HexToChar(_a >> 28);
            char _01 = HexToChar(_a >> 24);
            char _02 = HexToChar(_a >> 20);
            char _03 = HexToChar(_a >> 16);
            char _04 = HexToChar(_a >> 12);
            char _05 = HexToChar(_a >> 8);
            char _06 = HexToChar(_a >> 4);
            char _07 = HexToChar(_a);
            //char _08 = '-';
            char _09 = HexToChar(_b >> 12);
            char _10 = HexToChar(_b >> 8);
            char _11 = HexToChar(_b >> 4);
            char _12 = HexToChar(_b);
            //char _13 = '-';
            char _14 = HexToChar(_c >> 12);
            char _15 = HexToChar(_c >> 8);
            char _16 = HexToChar(_c >> 4);
            char _17 = HexToChar(_c);
            //char _18 = '-';
            char _19 = HexToChar(_d >> 4);
            char _20 = HexToChar(_d);
            char _21 = HexToChar(_e >> 4);
            char _22 = HexToChar(_e);
            //char _23 = '-';
            char _24 = HexToChar(_f >> 4);
            char _25 = HexToChar(_f     );
            char _26 = HexToChar(_g >> 4);
            char _27 = HexToChar(_g     );
            char _28 = HexToChar(_h >> 4);
            char _29 = HexToChar(_h     );
            char _30 = HexToChar(_i >> 4);
            char _31 = HexToChar(_i     );
            char _32 = HexToChar(_j >> 4);
            char _33 = HexToChar(_j     );
            char _34 = HexToChar(_k >> 4);
            char _35 = HexToChar(_k     );

            return string.Format(format, _00, _01, _02, _03, _04, _05, _06, _07, _09, _10, _11, _12, _14, _15, _16, _17, _19, _20, _21, _22, _24, _25, _26, _27, _28, _29, _30, _31, _32, _33, _34, _35);
        }

        internal static char HexToChar(int a)
        {
            a = a & 0xf;

            return (char)((a > 9) ? a - 10 + 0x61 : a + 0x30);
        }

        public static bool TryParse(string value, out UID uid)
        {
            if (Guid.TryParse(value, out Guid guid))
            {
                uid = guid;
                
                return true;
            }

            uid = default(UID);
            
            return false;
        }

        public static implicit operator UID(Guid guid) { return new UID(guid); }

        public static bool operator ==(UID left, UID right) { return left.Equals(right); }
        public static bool operator !=(UID left, UID right) { return !(left == right); }

        public static UID NewUID() { return Guid.NewGuid(); }

        public int CompareTo(object obj)
        {
            if (obj is UID uid)
                return CompareTo(uid);

            throw new ArgumentException("Object is not a UID");
        }

        public int CompareTo(UID other)
        {
            if (_a < other._a) return -1;
            else if (_a > other._a) return 1;

            if (_b < other._b) return -1;
            else if (_b > other._b) return 1;

            if (_c < other._c) return -1;
            else if (_c > other._c) return 1;

            if (_d < other._d) return -1;
            else if (_d > other._d) return 1;

            if (_e < other._e) return -1;
            else if (_e > other._e) return 1;

            if (_f < other._f) return -1;
            else if (_f > other._f) return 1;

            if (_g < other._g) return -1;
            else if (_g > other._g) return 1;

            if (_h < other._h) return -1;
            else if (_h > other._h) return 1;

            if (_i < other._i) return -1;
            else if (_i > other._i) return 1;

            if (_j < other._j) return -1;
            else if (_j > other._j) return 1;

            return _k < other._k ? -1 : _k > other._k ? 1 : 0;
        }

        public static readonly UID Empty = Guid.Empty;

        public int      _a;
        public short    _b;
        public short    _c;
        public byte     _d;
        public byte     _e;
        public byte     _f;
        public byte     _g;
        public byte     _h;
        public byte     _i;
        public byte     _j;
        public byte     _k;
    }
}
