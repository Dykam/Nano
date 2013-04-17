using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities.Status;
using Nano.Entities.Actions;
using Microsoft.Xna.Framework;

namespace Nano.Entities
{
	abstract class LivingEntity : Entity
	{
		List<EntityStatus> statusses;
		public float Health { get; protected set; }
		public float MaxHealth { get; protected set; }
		public bool Alive { get { return Health > 0; } }

		public LivingEntity()
		{
			statusses = new List<EntityStatus>();
		}

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
			statusses.Add(status);
			status.Activate();
		}

		public virtual void Die()
		{
			Manager.Remove(this);
		}
	}
}
