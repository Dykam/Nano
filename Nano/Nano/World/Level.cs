using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using PathFinding;
using Engine;
using Nano.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Nano.World
{
	class Level : GameObject
	{
		public AStarSolver<Tile,LivingEntity> Solver { get; private set; }
		public Map<Tile> Map { get; private set; }
		public EntityManager Entities { get; private set; }
		public string Name { get; private set; }
		public SoundEffect BackgroundSound { get; private set; }
		public Level(string name, int width, int height, EntityManager entities, SoundEffect backgroundSound)
			: base("level")
		{
			Name = name;
			Map = new Map<Tile>(width, height);
			Entities = entities;
			entities.ParentObject = this;
			BackgroundSound = backgroundSound;
			Solver = new AStarSolver<Tile, LivingEntity>(Map, (start, current, goal) => {
				var diff = current - goal;
				diff.X = Math.Abs(diff.X);
				diff.Y = Math.Abs(diff.Y);

				/* A tiebreaker prevents A* from expanding all equal nodes at the same time. */
				var diffDirect = start - goal;
				diffDirect.X = Math.Abs(diffDirect.X);
				diffDirect.Y = Math.Abs(diffDirect.Y);
				var tieBreaker = Math.Abs(diff.X * diffDirect.Y - diffDirect.X * diff.Y);
				/* */

				return (diff.X + diff.Y) + tieBreaker / 1000;
			}, null, false);
		}

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			Entities.Update(gameTime);
			base.Update(gameTime);
		}

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Matrix transform)
		{
			Entities.Draw(spriteBatch, transform);
			base.Draw(spriteBatch, transform);
		}

		public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
		{
			Entities.HandleInput(inputHelper, gameTime);
			base.HandleInput(inputHelper, gameTime);
		}

		public override GameObject Find(string keyword)
		{
			return Entities.Find(keyword);
		}
		public override GameObject Find<T>()
		{
			return Entities.Find<T>();
		}
	}
}
