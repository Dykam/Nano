using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities.Status;
using Microsoft.Xna.Framework;

namespace Nano.Entities
{
	abstract class DNA
	{
	}

	abstract class SkillDNA : DNA
	{
		public float Cooldown { get; private set; }

		public SkillDNA(float cooldown)
		{
			Cooldown = cooldown;
		}

		public abstract bool Activate(LivingEntity activator, Vector2 aim, PlayState state);
	}

	abstract class AimSkillDNA : SkillDNA
	{
		public float Radius { get; private set; }
		public AimSkillDNA(float cooldown, float radius = 0)
			: base(cooldown)
		{
			Radius = radius;
		}
		public override bool Activate(LivingEntity activator, Vector2 aim, PlayState state)
		{
			var area = new Circle { Radius = Radius, Position = aim };
			var attacked = false;
			foreach (var entity in state.Level.Entities.OfType<LivingEntity>()) {
				if (!Collision.Intersects(area, entity.BoundingBox))
					break;

				Attack(activator, entity);
				attacked = true;
			}
			return true;
		}

		protected abstract void Attack(LivingEntity attacker, LivingEntity defender);
	}

	abstract class AreaSkillDNA : SkillDNA
	{
		public float Radius { get; private set; }
		public AreaSkillDNA(float cooldown, float radius = 0)
			: base(cooldown)
		{
			Radius = radius;
		}
		public override bool Activate(LivingEntity activator, Vector2 aim, PlayState state)
		{
			return false;
		}

		protected abstract void Attack(LivingEntity attacker, LivingEntity defender);
	}

	class PropertyDNA : DNA
	{

	}
}
