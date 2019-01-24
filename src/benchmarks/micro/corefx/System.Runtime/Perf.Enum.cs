// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace System.Tests
{
    [BenchmarkCategory(Categories.CoreFX)]
    public class Perf_Enum
    {
        #region Enums
        [Flags]
        public enum Colors
        {
            Red = 0x1,
            Orange = 0x2,
            Yellow = 0x4,
            Green = 0x8,
            Blue = 0x10
        }

        public enum ByteEnum : byte
        {
            Min = byte.MinValue,
            One = 1,
            Two = 2,
            Max = byte.MaxValue
        }

        public enum SByteEnum : sbyte
        {
            Min = sbyte.MinValue,
            One = 1,
            Two = 2,
            Max = sbyte.MaxValue
        }

        public enum UInt16Enum : ushort
        {
            Min = ushort.MinValue,
            One = 1,
            Two = 2,
            Max = ushort.MaxValue
        }

        public enum Int16Enum : short
        {
            Min = short.MinValue,
            One = 1,
            Two = 2,
            Max = short.MaxValue
        }

        public enum UInt32Enum : uint
        {
            Min = uint.MinValue,
            One = 1,
            Two = 2,
            Max = uint.MaxValue
        }

        public enum Int32Enum : int
        {
            Min = int.MinValue,
            One = 1,
            Two = 2,
            Max = int.MaxValue
        }

        public enum UInt64Enum : ulong
        {
            Min = ulong.MinValue,
            One = 1,
            Two = 2,
            Max = ulong.MaxValue
        }

        public enum Int64Enum : long
        {
            Min = long.MinValue,
            One = 1,
            Two = 2,
            Max = long.MaxValue
        }

        // 36 members
        public enum BigContiguousEnum
        {
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z
        }

        // 36 members
        public enum BigNonContiguousEnum
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            A = 10,
            B = 12,
            C = 14,
            D = 16,
            E = 18,
            F = 20,
            G = 22,
            H = 24,
            I = 26,
            J = 28,
            K = 30,
            L = 32,
            M = 34,
            N = 36,
            O = 38,
            P = 40,
            Q = 42,
            R = 44,
            S = 46,
            T = 48,
            U = 50,
            V = 52,
            W = 54,
            X = 56,
            Y = 58,
            Z = 60
        }

        // 3 members
        public enum SmallContiguousEnum
        {
            Zero,
            One,
            Two
        }

        // 3 members
        public enum SmallNonContiguousEnum
        {
            Zero = 0,
            Two = 2,
            Four = 4
        }
        #endregion

        [Benchmark]
        //[Arguments(Colors.Green | Colors.Red, Colors.Green)] // BenchmarkDotNet Issue #1020
        //public bool HasFlagTrue(Enum value, Enum flag) => value.HasFlag(flag);
        public bool HasFlagTrue() => (Colors.Green | Colors.Red).HasFlag(Colors.Green);

        [Benchmark]
        //[Arguments(Colors.Green | Colors.Red, Colors.Blue)] // BenchmarkDotNet Issue #1020
        //public bool HasFlagFalse(Enum value, Enum flag) => value.HasFlag(flag);
        public bool HasFlagFalse() => (Colors.Green | Colors.Red).HasFlag(Colors.Blue);

        #region IsDefined
        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), BigContiguousEnum.G)]
        [Arguments(typeof(BigNonContiguousEnum), BigNonContiguousEnum.G)]
        [Arguments(typeof(SmallContiguousEnum), SmallContiguousEnum.Two)]
        [Arguments(typeof(SmallNonContiguousEnum), SmallNonContiguousEnum.Two)]
        public bool IsDefinedEnumTrue(Type enumType, object value) => Enum.IsDefined(enumType, value);

        [Benchmark]
        //[Arguments(typeof(BigContiguousEnum), (BigContiguousEnum)36)] // BenchmarkDotNet Issue #1020
        //[Arguments(typeof(BigNonContiguousEnum), (BigNonContiguousEnum)11)]
        //[Arguments(typeof(SmallContiguousEnum), (SmallContiguousEnum)3)]
        //[Arguments(typeof(SmallNonContiguousEnum), (SmallNonContiguousEnum)1)]
        //public bool IsDefinedEnumFalse(Type enumType, object value) => Enum.IsDefined(enumType, value);
        [Arguments(typeof(BigContiguousEnum))]
        public bool IsDefinedEnumFalse(Type enumType) => Enum.IsDefined(enumType, (BigContiguousEnum)36);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), (int)BigContiguousEnum.G)]
        [Arguments(typeof(BigNonContiguousEnum), (int)BigNonContiguousEnum.G)]
        [Arguments(typeof(SmallContiguousEnum), (int)SmallContiguousEnum.Two)]
        [Arguments(typeof(SmallNonContiguousEnum), (int)SmallNonContiguousEnum.Two)]
        public bool IsDefinedUnderlyingTrue(Type enumType, object value) => Enum.IsDefined(enumType, value);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), 36)]
        [Arguments(typeof(BigNonContiguousEnum), 11)]
        [Arguments(typeof(SmallContiguousEnum), 3)]
        [Arguments(typeof(SmallNonContiguousEnum), 1)]
        public bool IsDefinedUnderlyingFalse(Type enumType, object value) => Enum.IsDefined(enumType, value);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), "G")]
        [Arguments(typeof(SmallContiguousEnum), "Two")]
        public bool IsDefinedStringTrue(Type enumType, object value) => Enum.IsDefined(enumType, value);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), "GG")]
        [Arguments(typeof(SmallContiguousEnum), "Three")]
        public bool IsDefinedStringFalse(Type enumType, object value) => Enum.IsDefined(enumType, value);
        #endregion

        [Benchmark]
        [Arguments(typeof(Int32Enum))]
        public Type GetUnderlyingType(Type enumType) => Enum.GetUnderlyingType(enumType);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum))]
        [Arguments(typeof(SmallContiguousEnum))]
        public Array GetValues(Type enumType) => Enum.GetValues(enumType);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum))]
        [Arguments(typeof(SmallContiguousEnum))]
        public string[] GetNames(Type enumType) => Enum.GetNames(enumType);

        #region GetName
        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), BigContiguousEnum.G)]
        [Arguments(typeof(SmallContiguousEnum), SmallContiguousEnum.One)]
        public string GetNameEnumDefined(Type enumType, object value) => Enum.GetName(enumType, value);

        [Benchmark]
        //[Arguments(typeof(BigContiguousEnum), (BigContiguousEnum)36)] // BenchmarkDotNet Issue #1020
        //[Arguments(typeof(SmallContiguousEnum), (SmallContiguousEnum)3)]
        //public string GetNameEnumUndefined(Type enumType, object value) => Enum.GetName(enumType, value);
        [Arguments(typeof(BigContiguousEnum))]
        public string GetNameEnumUndefined(Type enumType) => Enum.GetName(enumType, (BigContiguousEnum)36);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), (int)BigContiguousEnum.G)]
        [Arguments(typeof(SmallContiguousEnum), (int)SmallContiguousEnum.One)]
        public string GetNameUnderlyingDefined(Type enumType, object value) => Enum.GetName(enumType, value);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), 36)]
        [Arguments(typeof(SmallContiguousEnum), 3)]
        public string GetNameUnderlyingUndefined(Type enumType, object value) => Enum.GetName(enumType, value);
        #endregion

        #region ToObject
        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), 7)]
        public object ToObject(Type enumType, object value) => Enum.ToObject(enumType, value);

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), 7)]
        public object ToObjectInt32(Type enumType, int value) => Enum.ToObject(enumType, value);
        #endregion

        #region Equals
        [Benchmark]
        [Arguments(BigContiguousEnum.G, BigContiguousEnum.G)]
        public bool EqualsTrue(Enum value, object obj) => value.Equals(obj);

        [Benchmark]
        [Arguments(BigContiguousEnum.G, BigContiguousEnum.H)]
        public bool EqualsFalse(Enum value, object obj) => value.Equals(obj);

        [Benchmark]
        [Arguments(BigContiguousEnum.G)]
        public bool EqualsNullFalse(Enum value) => value.Equals(null);

        [Benchmark]
        [Arguments(BigContiguousEnum.G, (int)BigContiguousEnum.G)]
        public bool EqualsUnderlyingFalse(Enum value, object obj) => value.Equals(obj);
        #endregion

        [Benchmark]
        [Arguments(BigContiguousEnum.N)]
        public int GetHashCode(Enum value) => value.GetHashCode();

        #region ToString
        [Benchmark]
        [Arguments(BigContiguousEnum.C)]
        [Arguments(SmallContiguousEnum.One)]
        public string ToStringDefined(Enum value) => value.ToString();

        [Benchmark]
        //[Arguments((BigContiguousEnum)36)] // BenchmarkDotNet Issue #1020
        //[Arguments((SmallContiguousEnum)3)]
        //public string ToStringUndefined(Enum value) => value.ToString();
        public string ToStringUndefined() => ((BigContiguousEnum)36).ToString();

        [Benchmark]
        //[Arguments(Colors.Red | Colors.Orange | Colors.Yellow | Colors.Green | Colors.Blue)] // BenchmarkDotNet Issue #1020
        //public string ToStringFlags5(Enum value) => value.ToString();
        public string ToStringFlags5() => (Colors.Red | Colors.Orange | Colors.Yellow | Colors.Green | Colors.Blue).ToString();

        [Benchmark]
        //[Arguments(Colors.Orange | Colors.Yellow | Colors.Green)] // BenchmarkDotNet Issue #1020
        //public string ToStringFlags3(Enum value) => value.ToString();
        public string ToStringFlags3() => (Colors.Orange | Colors.Yellow | Colors.Green).ToString();

        [Benchmark]
        [Arguments(Colors.Red)]
        public string ToStringFlags1(Enum value) => value.ToString();

        [Benchmark]
        //[Arguments((Colors)0x20)] // BenchmarkDotNet Issue #1020
        //public string ToStringFlagsInvalidFlagCombination(Enum value) => value.ToString();
        public string ToStringFlagsInvalidFlagCombination() => ((Colors)0x20).ToString();

        [Benchmark]
        [Arguments(BigContiguousEnum.C, "D")]
        [Arguments(BigContiguousEnum.C, "X")]
        [Arguments(BigContiguousEnum.C, "G")]
        //[Arguments(Colors.Green | Colors.Yellow | Colors.Orange, "F")] // BenchmarkDotNet Issue #1020
        public string ToStringFormat(Enum value, string format) => value.ToString(format);

        [Benchmark]
        public string ToStringFormatF() => (Colors.Green | Colors.Yellow | Colors.Orange).ToString("F");
        #endregion

        #region Format
        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), BigContiguousEnum.C, "D")]
        [Arguments(typeof(BigContiguousEnum), BigContiguousEnum.C, "X")]
        [Arguments(typeof(BigContiguousEnum), BigContiguousEnum.C, "G")]
        //[Arguments(typeof(Colors), Colors.Green | Colors.Yellow | Colors.Orange, "F")] // BenchmarkDotNet Issue #1020
        public string Format(Type enumType, Enum value, string format) => Enum.Format(enumType, value, format);

        [Benchmark]
        [Arguments(typeof(Colors))]
        public string FormatF(Type enumType) => Enum.Format(enumType, Colors.Green | Colors.Yellow | Colors.Orange, "F");
        #endregion

        [Benchmark]
        [Arguments(BigContiguousEnum.C, BigContiguousEnum.E)]
        public int CompareToEnum(Enum value, object target) => value.CompareTo(target);

        [Benchmark]
        [Arguments(BigContiguousEnum.C)]
        public int CompareToNull(Enum value) => value.CompareTo(null);

        [Benchmark]
        [Arguments(Int32Enum.One)]
        public TypeCode GetTypeCode(Enum value) => value.GetTypeCode();

        #region IConvertible
        [Benchmark]
        [Arguments(ByteEnum.Max)]
        public byte ToByte(IConvertible value) => value.ToByte(null);

        [Benchmark]
        [Arguments(SByteEnum.Max)]
        public sbyte ToSByte(IConvertible value) => value.ToSByte(null);

        [Benchmark]
        [Arguments(Int16Enum.Max)]
        public short ToInt16(IConvertible value) => value.ToInt16(null);

        [Benchmark]
        [Arguments(UInt16Enum.Max)]
        public ushort ToUInt16(IConvertible value) => value.ToUInt16(null);

        [Benchmark]
        [Arguments(Int32Enum.Max)]
        public int ToInt32(IConvertible value) => value.ToInt32(null);

        [Benchmark]
        [Arguments(UInt32Enum.Max)]
        public uint ToUInt32(IConvertible value) => value.ToUInt32(null);

        [Benchmark]
        [Arguments(Int64Enum.Max)]
        public long ToInt64(IConvertible value) => value.ToInt64(null);

        [Benchmark]
        [Arguments(UInt64Enum.Max)]
        public ulong ToUInt64(IConvertible value) => value.ToUInt64(null);
        #endregion

        #region Parsing
        [Benchmark]
        [Arguments(typeof(Colors))]
        public object ParseFlagsNonGeneric(Type enumType) => Enum.Parse(enumType, "Red, Orange, Yellow, Green, Blue");

        [Benchmark]
        public Colors ParseFlagsGeneric() => Enum.Parse<Colors>("Red, Orange, Yellow, Green, Blue");

        [Benchmark]
        [Arguments(typeof(ByteEnum), "Two")]
        [Arguments(typeof(SByteEnum), "Two")]
        [Arguments(typeof(UInt16Enum), "Two")]
        [Arguments(typeof(Int16Enum), "Two")]
        [Arguments(typeof(UInt32Enum), "Two")]
        [Arguments(typeof(Int32Enum), "Two")]
        [Arguments(typeof(UInt64Enum), "Two")]
        [Arguments(typeof(Int64Enum), "Two")]
        [Arguments(typeof(BigContiguousEnum), "C")]
        [Arguments(typeof(SmallContiguousEnum), "One")]
        public object ParseNameNonGeneric(Type enumType, string value) => Enum.Parse(enumType, value);

        [Benchmark]
        [Arguments(typeof(ByteEnum))]
        [Arguments(typeof(SByteEnum))]
        [Arguments(typeof(UInt16Enum))]
        [Arguments(typeof(Int16Enum))]
        [Arguments(typeof(UInt32Enum))]
        [Arguments(typeof(Int32Enum))]
        [Arguments(typeof(UInt64Enum))]
        [Arguments(typeof(Int64Enum))]
        public object ParseValueNonGeneric(Type enumType) => Enum.Parse(enumType, "2");

        [Benchmark]
        public ByteEnum ParseNameGenericByte() => Enum.Parse<ByteEnum>("Two");

        [Benchmark]
        public SByteEnum ParseNameGenericSByte() => Enum.Parse<SByteEnum>("Two");

        [Benchmark]
        public UInt16Enum ParseNameGenericUInt16() => Enum.Parse<UInt16Enum>("Two");

        [Benchmark]
        public Int16Enum ParseNameGenericInt16() => Enum.Parse<Int16Enum>("Two");

        [Benchmark]
        public UInt32Enum ParseNameGenericUInt32() => Enum.Parse<UInt32Enum>("Two");

        [Benchmark]
        public Int32Enum ParseNameGenericInt32() => Enum.Parse<Int32Enum>("Two");

        [Benchmark]
        public UInt64Enum ParseNameGenericUInt64() => Enum.Parse<UInt64Enum>("Two");

        [Benchmark]
        public Int64Enum ParseNameGenericInt64() => Enum.Parse<Int64Enum>("Two");

        [Benchmark]
        public BigContiguousEnum ParseNameGenericBigContiguous() => Enum.Parse<BigContiguousEnum>("C");

        [Benchmark]
        public SmallContiguousEnum ParseNameGenericSmallContiguous() => Enum.Parse<SmallContiguousEnum>("One");

        [Benchmark]
        public ByteEnum ParseValueGenericByte() => Enum.Parse<ByteEnum>("2");

        [Benchmark]
        public SByteEnum ParseValueGenericSByte() => Enum.Parse<SByteEnum>("2");

        [Benchmark]
        public UInt16Enum ParseValueGenericUInt16() => Enum.Parse<UInt16Enum>("2");

        [Benchmark]
        public Int16Enum ParseValueGenericInt16() => Enum.Parse<Int16Enum>("2");

        [Benchmark]
        public UInt32Enum ParseValueGenericUInt32() => Enum.Parse<UInt32Enum>("2");

        [Benchmark]
        public Int32Enum ParseValueGenericInt32() => Enum.Parse<Int32Enum>("2");

        [Benchmark]
        public UInt64Enum ParseValueGenericUInt64() => Enum.Parse<UInt64Enum>("2");

        [Benchmark]
        public Int64Enum ParseValueGenericInt64() => Enum.Parse<Int64Enum>("2");

        [Benchmark]
        public bool TryParseOverflowGeneric() => Enum.TryParse<DayOfWeek>("9223372036854775807", out _); // long.MaxValue

        [Benchmark]
        public bool TryParseMissingGenericBigContiguous() => Enum.TryParse<BigContiguousEnum>("ABC", out _);

        [Benchmark]
        public bool TryParseMissingGenericSmallContiguous() => Enum.TryParse<SmallContiguousEnum>("ABC", out _);

        [Benchmark]
        [Arguments(typeof(DayOfWeek))]
        public bool TryParseOverflowNonGeneric(Type enumType) => Enum.TryParse(enumType, "9223372036854775807", out _); // long.MaxValue

        [Benchmark]
        [Arguments(typeof(BigContiguousEnum), "ABC")]
        [Arguments(typeof(SmallContiguousEnum), "ABC")]
        public bool TryParseMissingNonGeneric(Type enumType, string value) => Enum.TryParse(enumType, value, out _);
        #endregion

        #region HybridSearchCutoff
        // Last found cutoff was 32

        //[Benchmark]
        [ArgumentsSource(nameof(HybridSearchArguments))]
        public int LinearSearch(int[] values, int value, int length)
        {
            for (int i = length - 1; i >= 0; --i)
            {
                if (values[i] == value)
                {
                    return i;
                }
                if (values[i] < value)
                {
                    return ~(i + 1);
                }
            }
            return -1; // == ~0
        }

        //[Benchmark]
        [ArgumentsSource(nameof(HybridSearchArguments))]
        public int BinarySearch(int[] values, int value, int length) => Array.BinarySearch(values, 0, length, value);

        public IEnumerable<object[]> HybridSearchArguments()
        {
            for (int i = 28; i <= 36; ++i)
            {
                int[] values = new int[i];
                for (int j = 0; j < i; ++j)
                {
                    values[j] = j;
                }
                yield return new object[] { values, 0, i };
            }
        }
        #endregion
    }
}
