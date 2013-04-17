using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities
{
	abstract class LivingEntity : Entity
	{
		public float Health { get; private set; }
		public bool Alive { get { return Health > 0; } }

		public virtual void Damage(float hp)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) Die();
		}

		public virtual void Die()
		{
			Manager.Remove(this);
		}
	}
}
