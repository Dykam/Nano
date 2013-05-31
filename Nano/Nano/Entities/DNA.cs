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
		public abstract bool HasTargets(LivingEntity activator, Vector2 aim);
	}

	abstract class AreaSkillDNA : SkillDNA
	{
		// cache
		LivingEntity cache_activator;
		Vector2 cache_aim, cache_activatorPos;
		LivingEntity[] cache_targets;

		public float Radius { get; private set; }
		public AreaSkillDNA(float cooldown, float radius = 0)
			: base(cooldown)
		{
			Radius = radius;
		}
		public override bool Activate(LivingEntity activator, Vector2 aim)
		{
			HasTargets(activator, aim);
			foreach (var target in cache_targets) {
				Attack(activator, activator.Transform.LocalPosition, target);
			}
			return true;
		}

		public override bool HasTargets(LivingEntity activator, Vector2 aim)
		{
			if (cache_activator == activator && cache_aim == aim && cache_activatorPos == activator.Transform.LocalPosition)
				return cache_targets.Length > 0;
			var area = new Circle { Radius = Radius, Position = activator.Transform.Position };
			cache_targets =
				NanoGame.PlayState.Level.Entities
				.OfType<LivingEntity>()
				.Where(entity => entity is PlayerEntity != activator is PlayerEntity)
				.Where(entity => Collision.Intersects(area, entity.BoundingBox))
				.ToArray();
			cache_activator = activator;
			cache_aim = aim;
			cache_activatorPos = activator.Transform.LocalPosition;
			return cache_targets.Length > 0;
		}

		protected abstract bool Attack(LivingEntity attacker, Vector2 position, LivingEntity defender);
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
		protected override bool Attack(LivingEntity attacker, Vector2 position, LivingEntity defender)
		{
			if (attacker == defender)
				return false;

			var distance = Vector2.DistanceSquared(position, defender.Transform.LocalPosition);
			var fallOff = 5 * 5f;
			var strength = Math.Max(0, 1 - distance / fallOff);

			if (strength < 0)
				return false;

			defender.Damage(10 * strength);
			return true;
		}
	}

	class TouchOfDeath : AreaSkillDNA
	{
		public TouchOfDeath()
			: base(1000, 0.5f)
		{

		}

		public override bool Activate(LivingEntity activator, Vector2 aim)
		{
			NanoGame.PlayState.Effects.Start(new ShockwaveEffect(Radius), activator.Transform.LocalPosition);
			return base.Activate(activator, aim);
		}

		protected override bool Attack(LivingEntity attacker, Vector2 position, LivingEntity defender)
		{
			if (defender == attacker)
				return false;

			defender.Damage(attacker.Strength);
			return true;
		}
	}

	class BulletWave : SkillDNA
	{
		static Vector2[] directions = new[] {
			 Vector2.UnitX,
			 Vector2.UnitX + Vector2.UnitY,
			 Vector2.UnitY,
			-Vector2.UnitX + Vector2.UnitY,
			-Vector2.UnitX,
			-Vector2.UnitX - Vector2.UnitY,
			-Vector2.UnitY,
			 Vector2.UnitX - Vector2.UnitY
		};
		public BulletWave()
			: base(1000)
		{

		}
		public override bool Activate(LivingEntity activator, Vector2 aim)
		{
			foreach(Vector2 direction in directions) {
				Bullet b = new Bullet(activator, activator.Transform.LocalPosition + direction, activator.Transform.LocalPosition);
				activator.State.Level.Entities.Add(b);
			}
			return true;
		}

		public override bool HasTargets(LivingEntity activator, Vector2 aim)
		{
			return true;
		}
	}

	class PropertyDNA : DNA
	{

	}
}
