using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities.Status;
using Nano.Entities.Actions;

namespace Nano.Entities
{
	abstract class LivingEntity : Entity
	{
		List<EntityStatus> statuses;
		public float Health { get; protected set; }
		public float MaxHealth { get; protected set; }
		public bool Alive { get { return Health > 0; } }

		public virtual void Damage(float hp)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) Die();
		}
		public virtual void Damage(float hp, Entity cause)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) Die();
		}
		public virtual void Damage(float hp, EntityStatus cause)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) Die();
		}
		public virtual void Damage(float hp, EntityAttack cause)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) Die();
		}
		public virtual void Heal(float hp)
		{
			Health = Math.Min(MaxHealth, Health + hp);
		}
		public virtual void AddStatus(EntityStatus status)
		{
			statuses.Add(status);
			status.Activate();
		}

		public virtual void Die()
		{
			Manager.Remove(this);
		}
	}
}
