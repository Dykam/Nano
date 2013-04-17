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

		public Transform()
		{
			LocalScale = Vector2.One;
		}

		public Vector2 Position {
			get { return Parent != null ? LocalPosition * Parent.LocalScale + Parent.Position : LocalPosition; }
			set
			{
				if (Parent != null) {
					LocalPosition = (value - Parent.Position) / Parent.LocalScale;
				} else {
					LocalPosition = value;
				}
			}
		}
	}
}
