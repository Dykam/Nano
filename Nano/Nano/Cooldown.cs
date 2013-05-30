using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano
{
	class Cooldown
	{
		TimeSpan nextTick;
		public TimeSpan Time { get; private set; }
		public Cooldown(TimeSpan time)
		{
			Time = time;
		}
		public Cooldown(double seconds)
			: this(TimeSpan.FromSeconds(seconds))
		{
		}

		public bool TryTick(GameTime gameTime)
		{
			if (gameTime.TotalGameTime < nextTick)
				return false;
			nextTick = gameTime.TotalGameTime + Time;
			return true;
		}
	}
}
