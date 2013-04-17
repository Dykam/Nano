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
using Engine.GameObjects;

/*
 * GameEngine Usage:
 * 
 * //the game engine is a very strong tool to have all resources and sprites at hand.
 * //it also provides helping classes for the networking part.
 * //best way to use it is to create one public static gameEngine.
 * 
 * 
 * public static GameEngine gameEngine;
 * 
 * gameEngine = new GameEngine(Content, <Method for receiving packets>);
 * 
 * //from now on you can always have all methods and classes available
 * //for example:
 * 
 * MainClassWhereTheEngineIsMade.gameEngine.NetClient.Connect("127.0.0.1", 1337);
 * 
 * //or get a sprite
 * 
 * Texture2D player_blue_texture = MainClassWhereTheEngineIsMade.gameEngine.ResourceManager.GetSprite("blue_player");
 * 
 * //the engine contains:
 * - a networking client
 * - a sprite manager
 * - a gamestate manager
 * - an input helper
 * //for usage of each individual piece see their class files.
 * 
 **/
namespace Engine
{
    public class GameEngine
    {
        Game mainGame;
        NetClient netClient;
        GameStateManager gameStateManager;
        InputHelper inputHelper;
        ResourceManager resourceManager;

        public GameEngine(Game game, SpriteBatch spriteBatch, ContentManager content, Action<Packet> HandlePacketData, Action OnConnectMethod)
        {
            //assign the game to the engine
            this.mainGame = game;
            
            //create the resourcemanager here
            resourceManager = new ResourceManager(spriteBatch, content, this);

            //add gamestates here. Gamestate can be changed anytime with TowerDefence.gameStateManager.SetCurrentState()
            gameStateManager = new GameStateManager();
            
            //create the inputhelper
            inputHelper = new InputHelper();

            //create the netClient and connect it. 
            netClient = new NetClient(HandlePacketData, OnConnectMethod);
        }

        public void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);
            gameStateManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameStateManager.Draw(spriteBatch);
        }

        public NetClient NetClient
        {
            get { return netClient; }
        }

        public GameStateManager GameStateManager
        {
            get { return gameStateManager; }
        }

        public InputHelper InputHelper
        {
            get { return inputHelper; }
        }

        public ResourceManager ResourceManager
        {
            get { return resourceManager; }
        }

        public Game Game
        {
            get { return mainGame; }
        }

        public Vector2 Screen
        {
            get { return new Vector2(mainGame.GraphicsDevice.Viewport.Width, mainGame.GraphicsDevice.Viewport.Height); }
        }

        public Rectangle ScreenRect
        {
            get { return new Rectangle(0, 0, (int)Screen.X, (int)Screen.Y); }
        }

        public bool MuteSound { get; set; }
    }
}
