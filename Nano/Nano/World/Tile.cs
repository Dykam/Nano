using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using Nano.Entities;

namespace Nano.World
{
	class Tile : Node, ISolverTile<LivingEntity>
	{
		public bool IsWall;
		public bool IsWalkableBy(LivingEntity subject)
		{
			return IsWall;
		}

		public float Cost(LivingEntity subject)
		{
			throw new NotImplementedException();
		}
	}
}
