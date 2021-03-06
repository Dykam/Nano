﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;

namespace Nano.Interface
{
	class CrossHair : GameObject
	{
		TileSheet sheet;
		int x, y;
		public CrossHair(TileSheet sheet, int x, int y)
			: base()
		{
			this.sheet = sheet;
			this.x = x;
			this.y = y;
		}

		public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
		{
			Transform.LocalPosition = inputHelper.MousePosition;
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			spriteBatch.Draw(sheet, x, y, Color.Red, Transform, Matrix.CreateTranslation(sheet.TileSize / -4f, sheet.TileSize / -4f, 0));
		}
	}
}
