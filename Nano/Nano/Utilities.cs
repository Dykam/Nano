using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Nano
{
	static class Utilities
	{
		public static void Wrap(ref Int2 loc, Int2 bounds)
		{
			loc.X %= bounds.X;
			loc.Y %= bounds.Y;

			if (loc.X < 0) loc.X += bounds.X;
			if (loc.Y < 0) loc.Y += bounds.Y;
		}

		public static void Wrap(ref int x, int bounds)
		{
			x %= bounds;
			if (x < 0) x += bounds;
		}

		public static void WrapOffset(ref Int2 offset, Int2 bounds)
		{
			Wrap(ref offset, bounds);

			if (offset.X > 1) offset.X -= bounds.X;
			if (offset.Y > 1) offset.Y -= bounds.Y;
		}

		public static Int2 WrappedDifference(Int2 loc1, Int2 loc2, Int2 bounds)
		{
			var diff = loc1 - loc2;
			diff.X = Math.Abs(diff.X);
			diff.Y = Math.Abs(diff.Y);
			return Int2.Min(diff, bounds - diff);
		}

		public static bool AreNeighbours(Int2 loc1, Int2 loc2, Int2 bounds)
		{
			return WrappedDifference(loc1, loc2, bounds).LengthSquared() == 1;
		}
	}
}
