﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.Entities.Enemies
{
    class White : NPCEntity
    {
        public White(int strength)
			: base(20 * strength / 10f, 4, strength)
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("sprites/whiteTexture");
			Transform.LocalScale *= strength / 10f;
			Transform.LocalPosition += (Vector2.One - Vector2.One * Transform.LocalScale) / 2;
        }

		public override void Update(GameTime gameTime)
		{
			if (gameTime.TotalGameTime - LastSearch > TimeSpan.FromSeconds(1)) {
				var target = State.Player.Transform.LocalPosition + Vector2.One * .5f;
				BuildPath(new Int2((int)target.X, (int)target.Y), gameTime);
			}
			base.Update(gameTime);
			Vector2 aim = State.Player.Transform.LocalPosition - Transform.LocalPosition;
			aim.Normalize();
			foreach (var skill in DNA.SkillCooling.Where(kvp => kvp.Value <= TimeSpan.Zero).ToArray()) {
				if (skill.Key.HasTargets(this, aim)) {
					DNA.ActivateSkill(skill.Key, this, aim);
				}
			}
		}
	}
}
