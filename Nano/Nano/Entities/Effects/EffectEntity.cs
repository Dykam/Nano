using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.Entities.Effects
{
	abstract class EffectEntity : Entity
	{
		public new EffectManager Manager { get; internal set; }
	}
}
