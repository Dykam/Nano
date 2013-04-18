using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine;

namespace Nano.Entities
{
	class EntityManager : GameObject, IEnumerable<GameObject>
	{
		protected List<GameObject> entities;
		// Whether or not to draw children
		protected bool draw;
		// Lock mutations until it finishes updating
		bool lockMutations;
		List<Action> mutations;
		public EntityManager(string id, bool draw = false)
			: base(id)
		{
			entities = new List<GameObject>();
			this.draw = draw;
			mutations = new List<Action>();
		}

		public void Add(Entity entity)
		{
			if (lockMutations) {
				mutations.Add(() => {
					entities.Add(entity);
					entity.Manager = this;
				});
			} else {
				entities.Add(entity);
				entity.Manager = this;
			}
			entity.ParentObject = this;
		}
		public void Remove(Entity entity)
		{
			if (lockMutations) {
				mutations.Add(() => entities.Remove(entity));
			} else {
				entities.Remove(entity);
			}
			if (entity.ParentObject == this) {
				entity.ParentObject = null;
			}
		}
		public void Clear()
		{
			if (lockMutations) {
				mutations.Add(() => entities.Clear());
			} else {
				entities.Clear();
			}
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			if (!draw)
				return;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			foreach (var entity in entities) {
				entity.Draw(spriteBatch, transform);
			}
            spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
			lockMutations = true;
			foreach (var entity in entities)
				entity.Update(gameTime);
			foreach (var mutation in mutations)
				mutation();
			mutations.Clear();
			lockMutations = false;
		}

		public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
		{
			lockMutations = true;
			foreach (var entity in entities)
				entity.HandleInput(inputHelper, gameTime);
			foreach (var mutation in mutations)
				mutation();
			mutations.Clear();
			lockMutations = false;
		}

		public override GameObject Find(string keyword)
		{
			foreach (Entity entity in entities) {
				var result = entity.Find(keyword);
				if (result != null)
					return result;
			}
			return null;
		}
		public override GameObject Find<T>()
		{
			foreach (Entity entity in entities) {
				var result = entity.Find<T>();
				if (result != null)
					return result;
			}
			return null;
		}


		public IEnumerator<GameObject> GetEnumerator()
		{
			return entities.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return entities.GetEnumerator();
		}
	}
}
