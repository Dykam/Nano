using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;

namespace Nano.Entities
{
	abstract class Entity : GameObject
	{
		public PlayState State { get; private set; }

		public EntityManager Manager { get; internal set; }
		public bool Solid { get; protected set; }
		public Entity()
			: base()
		{
			State = NanoGame.PlayState;
		}
	}
}
