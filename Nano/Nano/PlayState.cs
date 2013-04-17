using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Nano.Interface;
using Microsoft.Xna.Framework;
using Nano.Entities;

namespace Nano
{
	class PlayState : GameState
	{
		NanoGame nanoGame;
		GameObjectList root;
		/// <summary>
		/// Contains all GUI gameobjects
		/// </summary>
		InterfaceManager @interface;
		/// <summary>
		/// Contains all interactive gameobjects
		/// </summary>
		EntityManager entities;
		TileSheet uisheet;

		public PlayState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			uisheet = new TileSheet(nanoGame.Content.Load<Texture2D>("Interface"), 128);
			root = new GameObjectList("play", true) {
				(@interface = new InterfaceManager("interface", true) {
					new CrossHair(uisheet, 0, 0)
				}),
				(entities = new EntityManager("entities", true) {
					new PlayerEntity(nanoGame.Content.Load<Texture2D>("Sprites/playerTexture"))
				}),
				// TODO: Add world
			};
			root.Transform.LocalScale = Vector2.One * 2;
		}

		public override void Enable()
		{
			nanoGame.IsMouseVisible = false;
		}

		public override void Reset()
		{
			root.Clear();
		}
		public override void Update(GameTime gameTime)
		{
			root.Update(gameTime);
			root.HandleInput(nanoGame.Engine.InputHelper);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			root.Draw(spriteBatch, false);
		}
	}
}
