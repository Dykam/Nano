using System;
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

		public override void HandleInput(InputHelper inputHelper)
		{
			Transform.Position = inputHelper.MousePosition - Vector2.One * sheet.TileSize / 2;
		}

		public override void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
		{
			spriteBatch.Draw(sheet, x, y, Color.Red, Transform);
		}
	}
}
