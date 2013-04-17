using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using PathFinding;

namespace Nano.World
{
	class Level : GameObject
	{
		public Map<Tile> Map { get; private set; }
		public Level(int width, int height)
			: base("level")
		{
			Map = new Map<Tile>(width, height);
		}
	}
}
