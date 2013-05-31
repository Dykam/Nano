using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities.Status;
using Microsoft.Xna.Framework;
using Nano.Entities.Effects;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

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
		public int Strength { get; set; }
		SoundEffect hitEntity;

		public LivingEntity(float maxHealth, float speed, int strength)
		{
			statusses = new List<EntityStatus>();
			DNA = new DNACollection();
			MaxHealth = Health = maxHealth;
			Speed = speed;
			Strength = strength;
			if (hitEntity == null) {
				hitEntity = State.nanoGame.Content.Load<SoundEffect>("Sound/hitEntity");
			}
		}

		public override void Update(GameTime gameTime)
		{
			DNA.Update(gameTime);
			base.Update(gameTime);
		}

		public virtual void Damage(float hp)
		{
			Health = Math.Max(0, Health - hp);
			if (Health == 0) { Die(); return; }
			State.Effects.Start(new DamageEffect(hp), this);
			hitEntity.Play(1f, 0, MathHelper.Clamp(Vector2.Transform(Transform.LocalPosition, State.GameToScreenUnits).X / State.nanoGame.Window.ClientBounds.Width * 2 - 1, -1, 1));
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

        public virtual void RemoveStatus(EntityStatus status)
        {
            statusses.Remove(status);
        }

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Matrix transform)
		{
			if (HasDefaultTexture) {
				spriteBatch.Draw(Texture, new Color(1f, Health / MaxHealth, Health / MaxHealth, Opacity * (Health / MaxHealth / 2f + 0.5f)), Transform, transform);
			}
		}

		public virtual void Die()
		{
			Manager.Remove(this);
		}
    }
}
