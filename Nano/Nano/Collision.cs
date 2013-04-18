using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano
{
	static class Collision
	{
		public static bool Intersects(Circle circle, Rectangle rect)
		{
			var circleDistance = new Vector2(Math.Abs(circle.Position.X - rect.X), Math.Abs(circle.Position.Y - rect.Y));
			
			if (circleDistance.X > (rect.Width / 2 + circle.Radius)) { return false; }
			if (circleDistance.Y > (rect.Height / 2 + circle.Radius)) { return false; }

			if (circleDistance.X <= (rect.Width / 2)) { return true; }
			if (circleDistance.Y <= (rect.Height / 2)) { return true; }

			var cornerDistance_sq = Vector2.DistanceSquared(circleDistance, new Vector2(rect.Width, rect.Height) / 2);

			return (cornerDistance_sq <= (circle.Radius * circle.Radius));
		}
	}
}
