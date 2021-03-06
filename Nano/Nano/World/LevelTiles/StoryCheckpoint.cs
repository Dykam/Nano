﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.Entities;
using Nano.Interface;

namespace Nano.World.LevelTiles
{
    class StoryCheckpoint : InanimateEntity
    {
        public bool Passed;
        string text;

        public StoryCheckpoint(string text, int id)
            : base()
        {
            this.text = text;
            this.id = id.ToString();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Passed)
            {
				StoryNarrative.Set(State.Interface, text);
                State.Level.Entities.Remove(this);
            }
                
                
        }
    }
}
