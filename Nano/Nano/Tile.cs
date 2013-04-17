using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Nano
{
	class Tile
	{
		public TileSheet Sheet { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public Tile(TileSheet sheet, int x, int y)
		{
			Sheet = sheet;
			X = x;
			Y = y;
		}
	}
}
