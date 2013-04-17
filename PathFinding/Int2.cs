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
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	public struct Int2 : IEquatable<Int2>
	{
		#region Private Fields

		private static Int2 zeroVector = new Int2(0, 0);
		private static Int2 unitVector = new Int2(1, 1);
		private static Int2 unitXVector = new Int2(1, 0);
		private static Int2 unitYVector = new Int2(0, 1);

		#endregion Private Fields


		#region Public Fields

		public int X;
		public int Y;

		#endregion Public Fields


		#region Properties

		public static Int2 Zero
		{
			get { return zeroVector; }
		}

		public static Int2 One
		{
			get { return unitVector; }
		}

		public static Int2 UnitX
		{
			get { return unitXVector; }
		}

		public static Int2 UnitY
		{
			get { return unitYVector; }
		}

		#endregion Properties


		#region Constructors

		public Int2(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public Int2(int value)
		{
			this.X = value;
			this.Y = value;
		}

		#endregion Constructors


		#region Public Methods

		public static Int2 Add(Int2 value1, Int2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		public static void Add(ref Int2 value1, ref Int2 value2, out Int2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static int Distance(Int2 value1, Int2 value2)
		{
			int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (int)Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static void Distance(ref Int2 value1, ref Int2 value2, out int result)
		{
			int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (int)Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static int DistanceSquared(Int2 value1, Int2 value2)
		{
			int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (v1 * v1) + (v2 * v2);
		}

		public static void DistanceSquared(ref Int2 value1, ref Int2 value2, out int result)
		{
			int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (v1 * v1) + (v2 * v2);
		}

		public static Int2 Divide(Int2 value1, Int2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		public static void Divide(ref Int2 value1, ref Int2 value2, out Int2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static Int2 Divide(Int2 value1, int divider)
		{
			int factor = 1 / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		public static void Divide(ref Int2 value1, int divider, out Int2 result)
		{
			int factor = 1 / divider;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
		}

		public static int Dot(Int2 value1, Int2 value2)
		{
			return (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void Dot(ref Int2 value1, ref Int2 value2, out int result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is Int2) {
				return Equals((Int2)this);
			}

			return false;
		}

		public bool Equals(Int2 other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode();
		}

		public int Length()
		{
			return (int)Math.Sqrt((X * X) + (Y * Y));
		}

		public int LengthSquared()
		{
			return (X * X) + (Y * Y);
		}

		public static Int2 Max(Int2 value1, Int2 value2)
		{
			return new Int2(value1.X > value2.X ? value1.X : value2.X,
							   value1.Y > value2.Y ? value1.Y : value2.Y);
		}

		public static void Max(ref Int2 value1, ref Int2 value2, out Int2 result)
		{
			result.X = value1.X > value2.X ? value1.X : value2.X;
			result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
		}

		public static Int2 Min(Int2 value1, Int2 value2)
		{
			return new Int2(value1.X < value2.X ? value1.X : value2.X,
							   value1.Y < value2.Y ? value1.Y : value2.Y);
		}

		public static void Min(ref Int2 value1, ref Int2 value2, out Int2 result)
		{
			result.X = value1.X < value2.X ? value1.X : value2.X;
			result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
		}

		public static Int2 Multiply(Int2 value1, Int2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		public static Int2 Multiply(Int2 value1, int scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		public static void Multiply(ref Int2 value1, int scaleFactor, out Int2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Multiply(ref Int2 value1, ref Int2 value2, out Int2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		public static Int2 Negate(Int2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		public static void Negate(ref Int2 value, out Int2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public void Normalize()
		{
			float val = 1.0f / (float)Math.Sqrt((X * X) + (Y * Y));
			X = (int)(X * val);
			Y = (int)(Y * val);
		}

		public static Int2 Normalize(Int2 value)
		{
			float val = 1.0f / (float)Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			value.X = (int)(value.X * val);
			value.Y = (int)(value.Y * val);
			return value;
		}

		public static void Normalize(ref Int2 value, out Int2 result)
		{
			float val = 1.0f / (float)Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			result.X = (int)(value.X * val);
			result.Y = (int)(value.Y * val);
		}

		public static Int2 Subtract(Int2 value1, Int2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		public static void Subtract(ref Int2 value1, ref Int2 value2, out Int2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[] { 
				this.X.ToString(currentCulture), this.Y.ToString(currentCulture) });
		}

		#endregion Public Methods


		#region Operators

		public static Int2 operator -(Int2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}


		public static bool operator ==(Int2 value1, Int2 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}


		public static bool operator !=(Int2 value1, Int2 value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y;
		}


		public static Int2 operator +(Int2 value1, Int2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}


		public static Int2 operator -(Int2 value1, Int2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}


		public static Int2 operator *(Int2 value1, Int2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}


		public static Int2 operator *(Int2 value, int scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}


		public static Int2 operator *(int scaleFactor, Int2 value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}


		public static Int2 operator /(Int2 value1, Int2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}


		public static Int2 operator /(Int2 value1, int divider)
		{
			float factor = 1f / divider;
			value1.X = (int)(value1.X * factor);
			value1.Y = (int)(value1.Y * factor);
			return value1;
		}

		#endregion Operators
	}
}