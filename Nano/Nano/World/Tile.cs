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
		public Entity LevelEntity;
		void verifyEntity()
		{
			if (LevelEntity is LivingEntity && !((LivingEntity)LevelEntity).Alive)
				LevelEntity = null;
		}
		public bool IsWalkableBy(LivingEntity subject)
		{
			verifyEntity();
			return LevelEntity == null || !LevelEntity.Solid;
		}

		public float Cost(LivingEntity subject)
		{
			return 1;
		}
	}
}
