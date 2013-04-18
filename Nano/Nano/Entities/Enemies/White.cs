using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.Entities.Enemies
{
    class White : NPCEntity
    {
        public White(int strength)
            : base(20, 4, strength)
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("sprites/whiteTexture");
        }

		public override void Update(GameTime gameTime)
		{
			if (gameTime.TotalGameTime - LastSearch > TimeSpan.FromSeconds(1) && Vector2.DistanceSquared(State.Player.Transform.LocalPosition, this.Transform.LocalPosition) < 10 * 10) {
				BuildPath(new Int2((int)State.Player.Transform.LocalPosition.X, (int)State.Player.Transform.LocalPosition.Y), gameTime);
			}
			base.Update(gameTime);
			foreach (var skill in DNA.SkillCooling.Where(kvp => kvp.Value <= TimeSpan.Zero).ToArray()) {
				if (skill.Key.HasTargets(this, this.Transform.LocalPosition)) {
					DNA.ActivateSkill(skill.Key, this, this.Transform.LocalPosition);
				}
			}
		}
	}
}
