using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Interface
{
	class InterfaceManager : GameObjectList
	{
		public InterfaceManager(string id, bool draw = false)
			: base(id, draw)
		{
		}

		public override void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
		{
			spriteBatch.Begin();
			base.Draw(spriteBatch, isGridObject);
			spriteBatch.End();
		}
	}
}
