using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;

namespace Nano.Entities
{
	abstract class Entity : GameObject
	{
		public EntityManager Manager { get; internal set; }
		public Entity()
			: base("entity")
		{
		}
	}
}
