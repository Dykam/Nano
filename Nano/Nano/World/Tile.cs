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
		public bool IsWalkableBy(LivingEntity subject)
		{
			throw new NotImplementedException();
		}

		public float Cost(LivingEntity subject)
		{
			throw new NotImplementedException();
		}
	}
}
