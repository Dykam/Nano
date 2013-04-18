using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities;

namespace Nano.World.LevelTiles
{
    class Wall : InanimateEntity
    {
        public Wall()
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/wallTexture");
			Solid = true;
        }
    }
}
