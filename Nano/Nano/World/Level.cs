using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using PathFinding;
using Engine;
using Nano.Entities;

namespace Nano.World
{
	class Level : GameObject
	{
		public Map<Tile> Map { get; private set; }
		public EntityManager Entities { get; private set; }
		public Level(int width, int height, EntityManager entities)
			: base("level")
		{
			Map = new Map<Tile>(width, height);
			Entities = entities;
			entities.ParentObject = this;
		}

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			Entities.Update(gameTime);
			base.Update(gameTime);
		}

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, bool isGridObject = false)
		{
			Entities.Draw(spriteBatch, isGridObject);
			base.Draw(spriteBatch, isGridObject);
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			Entities.HandleInput(inputHelper);
			base.HandleInput(inputHelper);
		}
	}
}
