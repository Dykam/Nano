using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities;

namespace Nano.World.LevelTiles
{
	class BloodClot : LivingEntity
	{
		public BloodClot()
			: base(10, 0, 0)
		{
			Solid = true;
			Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/BloodClot");
		}
	}
}
