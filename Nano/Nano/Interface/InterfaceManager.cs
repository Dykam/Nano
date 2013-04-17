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

		public override void Draw(SpriteBatch spriteBatch, Vector2 viewingOffset)
		{
			spriteBatch.Begin();
			base.Draw(spriteBatch, viewingOffset);
			spriteBatch.End();
		}
	}
}
