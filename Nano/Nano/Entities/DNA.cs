using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities.Status;
using Microsoft.Xna.Framework;
using Nano.Entities.Effects;

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

		public abstract bool Activate(LivingEntity activator, Vector2 aim);
	}

	abstract class AimSkillDNA : SkillDNA
	{
		public float Radius { get; private set; }
		public AimSkillDNA(float cooldown, float radius = 0)
			: base(cooldown)
		{
			Radius = radius;
		}
		public override bool Activate(LivingEntity activator, Vector2 aim)
		{
			var area = new Circle { Radius = Radius, Position = aim };
			var attacked = false;
			foreach (var entity in NanoGame.PlayState.Level.Entities.OfType<LivingEntity>()) {
				if (!Collision.Intersects(area, entity.BoundingBox))
					break;
				attacked |= Attack(activator, entity);
			}
			return true;
		}

		protected abstract bool Attack(LivingEntity attacker, LivingEntity defender);
	}

	abstract class AreaSkillDNA : SkillDNA
	{
		public float Radius { get; private set; }
		public AreaSkillDNA(float cooldown, float radius = 0)
			: base(cooldown)
		{
			Radius = radius;
		}
		public override bool Activate(LivingEntity activator, Vector2 aim)
		{
			var area = new Circle { Radius = Radius, Position = activator.Transform.Position };
			var attacked = false;
			foreach (var entity in NanoGame.PlayState.Level.Entities.OfType<LivingEntity>()) {
				//if (Vector2.Distance(area.Position, entity.Transform.Position) > Radius)
					//continue;
				if (!Collision.Intersects(area, entity.BoundingBox))
					continue;

				attacked |= Attack(activator, entity);
			}
			return true;
		}

		protected abstract bool Attack(LivingEntity attacker, LivingEntity defender);
	}

	class ShockWave : AreaSkillDNA
	{
		public ShockWave()
			: base(2000, 3)
		{

		}
		public override bool Activate(LivingEntity activator, Vector2 aim)
		{
			NanoGame.PlayState.Effects.Start(new ShockwaveEffect(Radius), activator.Transform.LocalPosition);
			return base.Activate(activator, aim);
		}
		protected override bool Attack(LivingEntity attacker, LivingEntity defender)
		{
			if (attacker == defender)
				return false;

			var distance = Vector2.DistanceSquared(attacker.Transform.LocalPosition, defender.Transform.LocalPosition);
			var fallOff = 5 * 5f;
			var strength = Math.Max(0, 1 - distance / fallOff);

			if (strength < 0)
				return false;

			defender.Damage(10 * strength);
			return true;
		}
	}

	class PropertyDNA : DNA
	{

	}
}
