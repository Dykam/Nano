﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Engine.GameObjects
{
    public class GameObjectList : GameObject, IEnumerable<GameObject>
    {
        protected List<GameObject> gameObjects;
		// Whether or not to draw children
		protected bool draw;
		// Lock mutations until it finishes updating
		bool lockMutations;
		List<Action> mutations;
        public GameObjectList(string id, bool draw = false)
            : base(id)
        {
            gameObjects = new List<GameObject>();
			this.draw = draw;
			mutations = new List<Action>();
        }

        public void Add(GameObject obj)
        {
			if (lockMutations) {
				mutations.Add(() => gameObjects.Add(obj));
			} else {
				gameObjects.Add(obj);
			}
			obj.ParentObject = this;
        }
		public void Remove(GameObject obj)
		{
			if (lockMutations) {
				mutations.Add(() => gameObjects.Remove(obj));
			} else {
				gameObjects.Remove(obj);
			}
			obj.ParentObject = null;
		}
		public void Clear()
		{
			if (lockMutations) {
				mutations.Add(gameObjects.Clear);
			} else {
				gameObjects.Clear();
			}
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			if (!draw)
				return;

			foreach (var obj in gameObjects) {
				obj.Draw(spriteBatch, transform);
			}
		}

		public override void Update(GameTime gameTime)
		{
			lockMutations = true;
			foreach (var obj in gameObjects)
				obj.Update(gameTime);
			foreach (var mutation in mutations)
				mutation();
			mutations.Clear();
			lockMutations = false;
		}

		public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
		{
			lockMutations = true;
			foreach (var obj in gameObjects)
				obj.HandleInput(inputHelper, gameTime);
			foreach (var mutation in mutations)
				mutation();
			mutations.Clear();
			lockMutations = false;
		}

		public GameObject Find(string keyword)
		{
			foreach (GameObject obj in gameObjects) {
				var result = obj.Find(keyword);
				if (result != null)
					return result;
			}
			return null;
		}

		public GameObject Find<T>()
		{
			foreach (GameObject obj in gameObjects) {
				var result = obj.Find<T>();
				if (result != null)
					return result;
			}
			return null;
		}


		public IEnumerator<GameObject> GetEnumerator()
		{
			return gameObjects.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return gameObjects.GetEnumerator();
		}
	}
}
