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
using Nano.World;

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
		Level level;
		TileSheet uisheet;

		public PlayState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			uisheet = new TileSheet(nanoGame.Content.Load<Texture2D>("Interface"), 128);
			entities = new EntityManager("entities", true) {
				new PlayerEntity(nanoGame.Content.Load<Texture2D>("Sprites/playerTexture"))
			};
			root = new GameObjectList("play", true) {
				(level = new Level(100, 100, entities)),
				(@interface = new InterfaceManager("interface", true) {
					new CrossHair(uisheet, 0, 0)
				}),
				// TODO: Add world
			};
			level.Transform.LocalPosition += new Vector2(100, 100);
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
