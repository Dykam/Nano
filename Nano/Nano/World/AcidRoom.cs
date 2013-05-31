using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Nano.Entities;
using Nano.Entities.Status;
using Nano.Interface;

namespace Nano.World
{
    class AcidRoom : Room
    {
        Cooldown AcidCooldown;

        public AcidRoom(RectangleF location)
            : base(location)
        {
            AcidCooldown = new Cooldown(.25);
        }

        protected override void OnPlayerEnter(PlayerEntity player)
        {
            StoryNarrative.Set(NanoGame.PlayState.Interface, "Watch out! The stomach acid will kill you if you stay too long.");
        }

		protected override void OnPlayerLeave(PlayerEntity player)
        {
            
        }
        protected override void OnUpdate(PlayerEntity player)
        {
            if (AcidCooldown.TryTick())
                player.Damage(.50f);
        }
    }
}
