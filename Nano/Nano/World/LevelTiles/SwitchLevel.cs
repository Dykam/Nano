using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities;
using Nano.Interface;
using Microsoft.Xna.Framework;

namespace Nano.World.LevelTiles
{
    class SwitchLevel : InanimateEntity
    {
		public string Level { get; private set; }

		public SwitchLevel(string level, int id)
            : base()
        {
			Level = level;
            this.id = id.ToString();
        }

        public override void Update(GameTime gameTime)
        {
                
        }
    }
}
