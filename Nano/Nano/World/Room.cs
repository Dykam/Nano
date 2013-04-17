using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.World
{
	abstract class Room
	{
		public Rectangle Location { get; private set; }
		public abstract void OnPlayerEnter();
		public abstract void OnPlayerLeave();
	}
}
