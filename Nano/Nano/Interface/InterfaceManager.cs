using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Nano.Interface
{
	class InterfaceManager : GameObjectList
	{
		public InterfaceManager(string id, bool draw = false)
			: base(id, draw)
		{
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			base.Draw(spriteBatch, transform);
			spriteBatch.End();
		}
	}
}
