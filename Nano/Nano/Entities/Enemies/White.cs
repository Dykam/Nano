using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities.Enemies
{
    class White : NPCEntity
    {
        public White()
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("sprites/whiteTexture");
        }

		public int Strength { get; set; }
	}
}
