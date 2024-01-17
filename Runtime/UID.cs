using System;
using System.Runtime.InteropServices;

namespace UniqueIdentifier
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 16)]
    public unsafe struct UID : IEquatable<UID>, IEquatable<Guid>, IComparable<UID>, IComparable<Guid>
    {
        public static readonly UID Empty = Guid.Empty;

        [FieldOffset(0)] public Guid guid;

        [FieldOffset(0)] public fixed byte value[16];

        [FieldOffset(0)] public int _a;
        [FieldOffset(4)] public short _b;
        [FieldOffset(6)] public short _c;

        [FieldOffset(8)] public fixed byte bytes[8];

        [FieldOffset(8)] public byte _d;
        [FieldOffset(9)] public byte _e;
        [FieldOffset(10)] public byte _f;
        [FieldOffset(11)] public byte _g;
        [FieldOffset(12)] public byte _h;
        [FieldOffset(13)] public byte _i;
        [FieldOffset(14)] public byte _j;
        [FieldOffset(15)] public byte _k;

        public static UID NewUID() => Guid.NewGuid();

        public UID(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        {
            guid = default;

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

        public UID(int a, short b, short c, byte[] d) : this(a, b, c, d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7]) { }

        public UID(byte[] bytes)
        {
            guid = default;

            unsafe
            {
                _a = ((int)bytes[3] << 24) | ((int)bytes[2] << 16) | ((int)bytes[1] << 8) | bytes[0];
                _b = (short)(((int)bytes[5] << 8) | bytes[4]);
                _c = (short)(((int)bytes[7] << 8) | bytes[6]);
                _d = bytes[8];
                _e = bytes[9];
                _f = bytes[10];
                _g = bytes[11];
                _h = bytes[12];
                _i = bytes[13];
                _j = bytes[14];
                _k = bytes[15];
            }
        }

        public UID(Guid guid)
        {
            _a = default;
            _b = default;
            _c = default;

            _d  = default;
            _e  = default;
            _f  = default;
            _g  = default;
            _h  = default;
            _i  = default;
            _j  = default;
            _k  = default;

            this.guid = guid;
        }

        /// <summary>Implementation to mirror <see cref="Guid.GetHashCode"/>.</summary>
        /// <returns>The UID's hash.</returns>
        public override int GetHashCode()
        {
            return _a ^ (((int)_b << 16) | (int)(ushort)_c) ^ (((int)_f << 24) | _k);
        }

        public override bool Equals(object obj) => obj is UID UID && Equals(UID);
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
        public bool Equals(Guid other) => Equals(new UID(other));

        public int CompareTo(UID other) => guid.CompareTo(other);
        public int CompareTo(Guid other) => guid.CompareTo(other);

        public static bool operator ==(UID left, UID right) { return left.Equals(right); }
        public static bool operator !=(UID left, UID right) { return !(left == right); }

        public override string ToString() => Empty == this ? "UID.Empty" : ToString(_a, _b, _c, _d, _e, _f, _g, _h, _i, _j, _k);

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
            char _25 = HexToChar(_f);
            char _26 = HexToChar(_g >> 4);
            char _27 = HexToChar(_g);
            char _28 = HexToChar(_h >> 4);
            char _29 = HexToChar(_h);
            char _30 = HexToChar(_i >> 4);
            char _31 = HexToChar(_i);
            char _32 = HexToChar(_j >> 4);
            char _33 = HexToChar(_j);
            char _34 = HexToChar(_k >> 4);
            char _35 = HexToChar(_k);

            return string.Format(format, _00, _01, _02, _03, _04, _05, _06, _07, _09, _10, _11, _12, _14, _15, _16, _17, _19, _20, _21, _22, _24, _25, _26, _27, _28, _29, _30, _31, _32, _33, _34, _35);
        }

        internal static char HexToChar(int a)
        {
            a = a & 0xF;

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

        public static implicit operator UID(Guid guid) => new UID(guid);
        public static implicit operator Guid(UID uid) => uid.guid;
    }
}
