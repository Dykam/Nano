using System;
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
        }
		public void Remove(GameObject obj)
		{
			if (lockMutations) {
				mutations.Add(() => gameObjects.Remove(obj));
			} else {
				gameObjects.Remove(obj);
			}
		}
		public void Clear()
		{
			if (lockMutations) {
				mutations.Add(() => gameObjects.Clear());
			} else {
				gameObjects.Clear();
			}
		}

		public override void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
		{
			if (!draw)
				return;

			foreach (var obj in gameObjects) {
				obj.Draw(spriteBatch, isGridObject);
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

		public override void HandleInput(InputHelper inputHelper)
		{
			lockMutations = true;
			foreach (var obj in gameObjects)
				obj.HandleInput(inputHelper);
			foreach (var mutation in mutations)
				mutation();
			mutations.Clear();
			lockMutations = false;
		}

        public GameObject Find(string keyword)
        {
            foreach (GameObject obj in gameObjects)
            {
                if (obj.ID == keyword)
                {
                    return obj;
                }
                if (obj is GameObjectList)
                {
                    GameObjectList objectList = obj as GameObjectList;
                    GameObject subObject = objectList.Find(keyword);
                    if (subObject != null)
                    {
                        return subObject;
                    }
                }
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
