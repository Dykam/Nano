using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities;
using Microsoft.Xna.Framework;

namespace Nano.World.LevelTiles
{
    class Wall : InanimateEntity
    {
        public Wall()
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/wallTexture");
			Solid = true;
			Transform.LocalScale = new Vector2(MathHelper.Lerp(1.05f, 0.97f, (float)NanoGame.Random.NextDouble()), MathHelper.Lerp(1.05f, 0.97f, (float)NanoGame.Random.NextDouble()));
			Transform.LocalPosition += -(Vector2.One - Transform.LocalScale) / 2;
        }
    }
}
