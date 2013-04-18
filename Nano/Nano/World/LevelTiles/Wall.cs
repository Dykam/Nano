using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities;

namespace Nano.World.LevelTiles
{
    class Wall : InanimateEntity
    {
        Tile tile;

        public Wall()
            : base()
        {
            tile = new Tile();
            tile.IsWall = true;
            Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/wallTexture");
        }
    }
}
