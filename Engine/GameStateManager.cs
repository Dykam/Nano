using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*
 * Usage of GameStateManager:
 * 
 * //create one gamestatemanager (make it public static maybe?)
 * 
 * GameStateManager gsm = new GameStateManager();
 * 
 * //add gamestates to it (gamestate must be inheriting from GameState class)
 * 
 * gsm.AddGameState("mainmenu", new MenuGameState());
 * 
 * //set current gamestate
 * 
 * gsm.SetCurrentState("mainmenu");
 * 
 * //update and draw it
 * 
 * gsm.update(gameTime)
 * gsm.draw(spriteBatch)
 */



namespace Engine
{
    public class GameStateManager
    {
        Dictionary<string, GameState> gameStates;
        GameState currentGameState;

        public GameStateManager()
        {
            gameStates = new Dictionary<string, GameState>();
        }

        public void Update(GameTime gameTime)
        {
            if (currentGameState == null)
                throw new Exception("No current game state set, use GameStateManager.SetCurrentState()");

            currentGameState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentGameState == null)
                throw new Exception("No current game state set, use GameStateManager.SetCurrentState()");

            currentGameState.Draw(spriteBatch);
        }

        public void SetCurrentState(string gameStateName, bool reset = false)
        {
            if (!gameStateName.Contains(gameStateName))
                throw new Exception("Tried to set a non-existant gamestate: " + gameStateName);
            if(currentGameState != null)
                currentGameState.Disable();
            currentGameState = gameStates[gameStateName];
            currentGameState.Enable();

            if (reset)
                currentGameState.Reset();
        }

        public GameState GetGameStateByName(string gameStateName)
        {
            if (!gameStateName.Contains(gameStateName))
                throw new Exception("Tried to get a non-existant gamestate: " + gameStateName);

            return gameStates[gameStateName];
        }
        
        public void AddGameState(string gameStateName, GameState gameState)
        {
            gameState.Disable();
            gameStates.Add(gameStateName, gameState);
        }

        public bool IsCurrentGameStateDefined
        {
            get { return !(currentGameState == null); }
        }
    }
}
