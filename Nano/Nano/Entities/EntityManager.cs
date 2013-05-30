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
		protected List<GameObject> entities, entitiesInDistance;
		// Whether or not to draw children
		protected bool draw;
		List<Action> mutations;
		float drawDistance, updateDistance;

		public IEnumerable<GameObject> EntitiesInDistance { get { return entitiesInDistance; } }

		public EntityManager(float drawDistance, float updateDistance, string id = null, bool draw = false)
			: base(id)
		{
			entities = new List<GameObject>();
			entitiesInDistance = new List<GameObject>();
			this.draw = draw;
			mutations = new List<Action>();
			this.drawDistance = drawDistance;
			this.updateDistance = updateDistance;
		}

		public void Add(Entity entity)
		{
			entities.Add(entity);
			entity.Manager = this;
			entity.ParentObject = this;
		}
		public void Remove(Entity entity)
		{
			entities.Remove(entity);
			if (entity.ParentObject == this) {
				entity.ParentObject = null;
			}
		}
		public void Clear()
		{
			entities.Clear();
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			if (!draw)
				return;
			setEntitiesInDistance(drawDistance);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			foreach (var entity in entitiesInDistance) {
				entity.Draw(spriteBatch, transform);
			}
			spriteBatch.End();
		}

		private void setEntitiesInDistance(float distance)
		{
			distance *= distance;
			entitiesInDistance.Clear();
			var pos = NanoGame.PlayState.Player.Transform.LocalPosition;
			entitiesInDistance.AddRange(this.Where(t => Vector2.DistanceSquared(t.Transform.Position, pos) < distance));
		}

		public override void Update(GameTime gameTime)
		{
			setEntitiesInDistance(updateDistance);
			foreach (var entity in entitiesInDistance)
				entity.Update(gameTime);
		}

		public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
		{
			setEntitiesInDistance(updateDistance);
			foreach (var entity in entitiesInDistance)
				entity.HandleInput(inputHelper, gameTime);
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
