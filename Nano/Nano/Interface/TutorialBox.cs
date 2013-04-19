using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine;
using Nano;
using Microsoft.Xna.Framework.Input;

namespace Nano.Interface
{
    class TutorialBox : GameObject
    {
        public bool Active;
        int activeTutorial;
        Button closeButton;
        SpriteFont font;
        string tutorialMessage;
        Dictionary<int, bool> tutorialTracker;
        Dictionary<int, string> tutorials;
        public TutorialBox()
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/tutorialTexture");
            font = NanoGame.Engine.ResourceManager.GetFont("Fonts/Tutorial");
            InitializeTutorials();
            New(0);
        }

        private void InitializeTutorials()
        {
            tutorials = new Dictionary<int, string>();
            tutorialTracker = new Dictionary<int, bool>();
            tutorials.Add(0, "Use the [W, A, S, D] keys to move around!"); tutorialTracker.Add(0, false);
            tutorials.Add(1, "Use the mouse to aim and press the left mouse button to shoot!"); tutorialTracker.Add(1, false);
            tutorials.Add(2, "Press the spacebar to use Shockwave! This is an area of effect attack that damages all nearby enemies within range."); tutorialTracker.Add(2, false);
        }

        public void New(int id) 
        {
            activeTutorial = id;
            tutorialMessage = tutorials[id];
            Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            CheckForCompletion();

        }

        private void CheckForCompletion()
        {
            for (int i = 0; i < tutorialTracker.Count; i++)
            {
                if (!tutorialTracker[i])
                {
                    activeTutorial = i;
                    i = 999;
                }
            }

            switch (activeTutorial)
            {
                case 0:
                    if (NanoGame.Engine.InputHelper.KeyPressed(Keys.A) || NanoGame.Engine.InputHelper.KeyPressed(Keys.S) || NanoGame.Engine.InputHelper.KeyPressed(Keys.D) || NanoGame.Engine.InputHelper.KeyPressed(Keys.W))
                    {
                        activeTutorial = 1;
                        tutorialTracker[0] = true;
                    }
                    break;
                case 1:
                    if (NanoGame.Engine.InputHelper.MouseLeftButtonPressed())
                    {
                        activeTutorial = 2;
                        tutorialTracker[1] = true;
                    }
                    break;
                case 2:
                    if (NanoGame.Engine.InputHelper.KeyPressed(Keys.Space))
                    {
                        tutorialTracker[2] = true;
                    }
                    break;
            }
            for (int i = 0; i < tutorialTracker.Count; i++)
            {
                if (tutorialTracker[i] && i == tutorialTracker.Count - 1)
                    Active = false;
            }
                
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
            if (Active)
            {
                spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
                spriteBatch.DrawString(font, Engine.HelperMethods.WrapText(font, tutorials[activeTutorial], 230), new Vector2(10,10), Color.Black);
            }
        }
    }
}
