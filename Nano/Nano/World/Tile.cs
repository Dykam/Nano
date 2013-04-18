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
		public InanimateEntity LevelEntity;
		public bool IsWalkableBy(LivingEntity subject)
		{
			return LevelEntity == null || LevelEntity.Solid;
		}

		public float Cost(LivingEntity subject)
		{
			return 1;
		}
	}
}
