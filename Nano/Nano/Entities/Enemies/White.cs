using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.Entities.Enemies
{
    class White : NPCEntity
    {
		public int Strength { get; set; }
		
        public White()
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("sprites/whiteTexture");
        }

		public override void Update(GameTime gameTime)
		{
			if (!HasPath) {
				BuildPath(new Int2((int)State.Player.Transform.LocalPosition.X, (int)State.Player.Transform.LocalPosition.Y));
			}
			base.Update(gameTime);
		}
	}
}
