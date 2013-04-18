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
		Entity levelEntity;
		public Entity LevelEntity
		{
			get
			{
				verifyEntity();
				return levelEntity;
			}
			set
			{
				levelEntity = value;
			}
		}
		void verifyEntity()
		{
			if (levelEntity is LivingEntity && !((LivingEntity)levelEntity).Alive)
				levelEntity = null;
		}
		public bool IsWalkableBy(LivingEntity subject)
		{
			return LevelEntity == null || !LevelEntity.Solid;
		}

		public float Cost(LivingEntity subject)
		{
			return 1;
		}
	}
}
