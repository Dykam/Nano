using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine;
using Nano;

namespace Nano.Interface
{
    class TutorialBox : GameObject
    {
        public bool Active;
        Button closeButton;
        public TutorialBox()
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/tutorialTexture");
            Active = true;
            TileSheet tileSheet = new TileSheet(NanoGame.Engine.ResourceManager.GetSprite("MenuSheet"), 128);
            closeButton = new Button(tileSheet, 1, 1, () => Console.WriteLine("click"))
            {
                Border = Color.Black,
                Inner = Color.White
            };
            closeButton.Transform.LocalScale = new Vector2(0.1f,0.1f);
            closeButton.Transform.Position = new Vector2(200, 375);
        }


        public override void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
            if (Active)
            {
                spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
                spriteBatch.End();
                closeButton.Draw(spriteBatch, transform);
                
            }
        }
    }
}
