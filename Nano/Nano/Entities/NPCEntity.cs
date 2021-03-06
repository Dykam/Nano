﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using Microsoft.Xna.Framework;
namespace Nano.Entities
{
	class NPCEntity : LivingEntity
	{
		public bool Essential { get; set; }
		Queue<Int2> path;
		public bool HasPath { get { return path != null && path.Count != 0; } }
		public TimeSpan LastSearch { get; private set; }
		public NPCEntity(float maxHealth, float speed, int strength)
			: base(maxHealth, speed, strength)
		{

		}

		protected int BuildPath(Int2 target, GameTime gameTime)
		{
			var vfrom = BoundingBox.Center;
			var from = new Int2((int)vfrom.X, (int)vfrom.Y);
			var searched = State.Level.Solver.Search(from, this, target);
			if (searched != null) {
				path = new Queue<Int2>(searched);
			}
			LastSearch = gameTime.TotalGameTime;
			return searched != null ? searched.Steps : Int16.MaxValue;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (Stunned || !HasPath)
				return;
			float distanceLeft = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			while (distanceLeft > 0 && path.Count > 0) {
				var target = new Vector2(path.Peek().X, path.Peek().Y) + (Vector2.One - Vector2.One * Transform.LocalScale) / 2;
				var movement = target - Transform.LocalPosition;
				var distance = movement.Length();
				distance = Math.Min(distance, distanceLeft);
				if (distance == 0) {
					path.Dequeue();
					continue;
				}
				movement.Normalize();
				movement *= distance;
				Transform.LocalPosition += movement;
				distanceLeft -= distance;
				if (Vector2.DistanceSquared(target, Transform.LocalPosition) < 0.01) {
					path.Dequeue();
				}
			}
		}
	}
}
