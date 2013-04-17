#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team
Copyright © 2013 Dykam

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
using System.Text;

namespace Microsoft.Xna.Framework
{
    public struct Int3 : IEquatable<Int3>
    {
        #region Private Fields

        private static  Int3 zero = new Int3(0, 0, 0);
        private static  Int3 one = new Int3(1, 1, 1);
        private static  Int3 unitX = new Int3(1, 0, 0);
        private static  Int3 unitY = new Int3(0, 1, 0);
        private static  Int3 unitZ = new Int3(0, 0, 1);
        private static  Int3 up = new Int3(0, 1, 0);
        private static  Int3 down = new Int3(0, -1, 0);
        private static  Int3 right = new Int3(1, 0, 0);
        private static Int3 left = new Int3(-1, 0, 0);
        private static Int3 forward = new Int3(0, 0, -1);
        private static Int3 backward = new Int3(0, 0, 1);

        #endregion Private Fields


        #region Public Fields

        public int X;
        public int Y;
        public int Z;

        #endregion Public Fields


        #region Properties

        public static Int3 Zero
        {
            get { return zero; }
        }

        public static Int3 One
        {
            get { return one; }
        }

        public static Int3 UnitX
        {
            get { return unitX; }
        }

        public static Int3 UnitY
        {
            get { return unitY; }
        }

        public static Int3 UnitZ
        {
            get { return unitZ; }
        }

        public static Int3 Up
        {
            get { return up; }
        }

        public static Int3 Down
        {
            get { return down; }
        }

        public static Int3 Right
        {
            get { return right; }
        }

        public static Int3 Left
        {
            get { return left; }
        }

        public static Int3 Forward
        {
            get { return forward; }
        }

        public static Int3 Backward
        {
            get { return backward; }
        }

        #endregion Properties


        #region Constructors

        public Int3(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }


        public Int3(int value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }


        public Int3(Int2 value, int z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }


        #endregion Constructors


        #region Public Methods

        public static Int3 Add(Int3 value1, Int3 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static void Add(ref Int3 value1, ref Int3 value2, out Int3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        public static Int3 Cross(Int3 int1, Int3 int2)
        {
            Cross(ref int1, ref int2, out int1);
            return int1;
        }

        public static void Cross(ref Int3 int1, ref Int3 int2, out Int3 result)
        {
            result = new Int3(int1.Y * int2.Z - int2.Y * int1.Z,
                                 -(int1.X * int2.Z - int2.X * int1.Z),
                                 int1.X * int2.Y - int2.X * int1.Y);
        }

        public static int Distance(Int3 int1, Int3 int2)
        {
            int result;
            DistanceSquared(ref int1, ref int2, out result);
            return (int)Math.Sqrt(result);
        }

        public static void Distance(ref Int3 value1, ref Int3 value2, out float result)
        {
            int intResult;
			DistanceSquared(ref value1, ref value2, out intResult);
			result = (int)Math.Sqrt(intResult);
        }

		public static int DistanceSquared(Int3 value1, Int3 value2)
        {
			int result;
            DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

		public static void DistanceSquared(ref Int3 value1, ref Int3 value2, out int result)
        {
            result = (value1.X - value2.X) * (value1.X - value2.X) +
                     (value1.Y - value2.Y) * (value1.Y - value2.Y) +
                     (value1.Z - value2.Z) * (value1.Z - value2.Z);
        }

        public static Int3 Divide(Int3 value1, Int3 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static Int3 Divide(Int3 value1, int value2)
        {
            int factor = 1 / value2;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }

        public static void Divide(ref Int3 value1, int divisor, out Int3 result)
        {
            int factor = 1 / divisor;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
            result.Z = value1.Z * factor;
        }

        public static void Divide(ref Int3 value1, ref Int3 value2, out Int3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        public static int Dot(Int3 int1, Int3 int2)
        {
            return int1.X * int2.X + int1.Y * int2.Y + int1.Z * int2.Z;
        }

        public static void Dot(ref Int3 int1, ref Int3 int2, out int result)
        {
            result = int1.X * int2.X + int1.Y * int2.Y + int1.Z * int2.Z;
        }

        public override bool Equals(object obj)
        {
            return (obj is Int3) ? this == (Int3)obj : false;
        }

        public bool Equals(Int3 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)(this.X + this.Y + this.Z);
        }

        public int Length()
        {
            int result;
            DistanceSquared(ref this, ref zero, out result);
            return (int)Math.Sqrt(result);
        }

        public int LengthSquared()
        {
            int result;
            DistanceSquared(ref this, ref zero, out result);
            return result;
        }

        public static Int3 Multiply(Int3 value1, Int3 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static Int3 Multiply(Int3 value1, int scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref Int3 value1, int scaleFactor, out Int3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        public static void Multiply(ref Int3 value1, ref Int3 value2, out Int3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        public static Int3 Negate(Int3 value)
        {
            value = new Int3(-value.X, -value.Y, -value.Z);
            return value;
        }

        public static void Negate(ref Int3 value, out Int3 result)
        {
            result = new Int3(-value.X, -value.Y, -value.Z);
        }

        public void Normalize()
        {
            Normalize(ref this, out this);
        }

        public static Int3 Normalize(Int3 int3)
        {
            Normalize(ref int3, out int3);
            return int3;
        }

        public static void Normalize(ref Int3 value, out Int3 result)
        {
            float distance;
			Distance(ref value, ref zero, out distance);
            float factor = 1f / distance;
            result.X = (int)(value.X * factor);
            result.Y = (int)(value.Y * factor);
            result.Z = (int)(value.Z * factor);
        }

        public static Int3 Reflect(Int3 int3, Int3 normal)
        {
            throw new NotImplementedException();
        }

		public static void Reflect(ref Int3 int3, ref Int3 normal, out Int3 result)
        {
            throw new NotImplementedException();
        }

        public static Int3 Subtract(Int3 value1, Int3 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static void Subtract(ref Int3 value1, ref Int3 value2, out Int3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append(" Z:");
            sb.Append(this.Z);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public methods


        #region Operators

        public static bool operator ==(Int3 value1, Int3 value2)
        {
            return value1.X == value2.X
                && value1.Y == value2.Y
                && value1.Z == value2.Z;
        }

        public static bool operator !=(Int3 value1, Int3 value2)
        {
            return !(value1 == value2);
        }

        public static Int3 operator +(Int3 value1, Int3 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static Int3 operator -(Int3 value)
        {
            value = new Int3(-value.X, -value.Y, -value.Z);
            return value;
        }

        public static Int3 operator -(Int3 value1, Int3 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static Int3 operator *(Int3 value1, Int3 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static Int3 operator *(Int3 value, int scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        public static Int3 operator *(int scaleFactor, Int3 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        public static Int3 operator /(Int3 value1, Int3 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static Int3 operator /(Int3 value, int divider)
        {
            int factor = 1 / divider;
            value.X *= factor;
            value.Y *= factor;
            value.Z *= factor;
            return value;
        }

        #endregion
    }
}