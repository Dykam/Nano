using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Nano.Entities.Status
{
	abstract class EntityStatus
	{
		public LivingEntity Entity { get; private set; }
		public EntityStatus(LivingEntity entity)
		{
			Entity = entity;
		}

		public abstract void Activate();
	}
}
