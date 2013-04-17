using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
	public class Transform
	{
		public Vector2 LocalPosition { get; set; }
		public Vector2 LocalScale { get; set; }
		//public float LocalRotation { get; set; }
		public Transform Parent { get; set; }

		public Matrix GetMatrix()
		{
			var matrix = Parent != null ? Parent.GetMatrix() : Matrix.Identity;
			matrix *= Matrix.CreateScale(LocalScale.X, LocalScale.Y, 1);
			//matrix *= Matrix.CreateRotationZ(LocalRotation);
			matrix *= Matrix.CreateTranslation(LocalPosition.X, LocalPosition.Y, 0);
			return matrix;
		}
		public Vector2 Position {
			get { return Parent != null ? LocalPosition + Parent.Position : LocalPosition; }
			set
			{
				if (Parent != null) {
					LocalPosition = value - Parent.Position;
				} else {
					LocalPosition = value;
				}
			}
		}
	}
}
