using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using Microsoft.Xna.Framework;
namespace Nano.Entities
{
	class NPCEntity : LivingEntity
	{
		Queue<Int2> path;
		public bool HasPath { get { return path != null; } }
        public NPCEntity()
            : base()
        {
			
        }

        protected void BuildPath(Int2 target)
		{
			if (path == null) {
				var from = new Int2((int)Transform.LocalPosition.X, (int)Transform.LocalPosition.Y);
				path = new Queue<Int2>(State.Level.Solver.Search(from, this, target));
			}
        }

		public override void Update(GameTime gameTime)
		{
			if (Stunned)
				return;
			float distanceLeft = Speed;
			while (distanceLeft > 0) {
				var target = new Vector2(path.Peek().X, path.Peek().Y);
				var movement = target - Transform.LocalPosition;
				var distance = movement.Length();
				distance = Math.Min(distance, distanceLeft);
				Transform.LocalPosition = target;
				distanceLeft -= distance;
				if (distanceLeft >= 0) {
					path.Dequeue();
				}
			}
		}
	}
}
