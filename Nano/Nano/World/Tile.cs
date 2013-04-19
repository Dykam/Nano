using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using Nano.Entities;

namespace Nano.World
{
	class Tile : Node, ISolverTile<Entity>
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
		public bool IsWalkableBy(Entity subject)
		{
			return LevelEntity == null || !LevelEntity.Solid;
		}

		public float Cost(Entity subject)
		{
			return 1;
		}
	}
}
