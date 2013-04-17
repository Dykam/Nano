using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*
 * Usage of GameState:
 * 
 * make class inheriting from it:
 * 
 * class MainMenu : GameState
 * 
 * override update and draw, and you're done
 * 
 */


namespace Engine
{
    public class GameState
    {
        public GameState()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Reset()
        {

        }

        public virtual void Enable()
        {

        }

        public virtual void Disable()
        {

        }
    }
}
