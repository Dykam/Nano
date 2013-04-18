using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities.Status;
using Microsoft.Xna.Framework;
using Nano.Entities.Effects;

namespace Nano.Entities
{
	abstract class LivingEntity : Entity
	{
		List<EntityStatus> statusses;
		public float Health { get; protected set; }
		public float MaxHealth { get; protected set; }
		public bool Alive { get { return Health > 0; } }
        public float Speed { get; set; }
        public bool Stunned { get; set; }
		public DNACollection DNA { get; private set; }

		public LivingEntity(float maxHealth, float speed)
		{
			statusses = new List<EntityStatus>();
			DNA = new DNACollection();
			MaxHealth = Health = maxHealth;
			Speed = speed;
		}

		public override void Update(GameTime gameTime)
		{
			DNA.Update(gameTime);
			base.Update(gameTime);
		}

		public virtual void Damage(float hp)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) Die();
			State.Effects.Start(new DamageEffect(hp), this);
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

        internal void RemoveStatus(EntityStatus status)
        {
            statusses.Remove(status);
        }

		public virtual void Die()
		{
			Manager.Remove(this);
		}
    }
}
