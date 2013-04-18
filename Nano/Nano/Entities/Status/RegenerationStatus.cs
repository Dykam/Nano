using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities.Status
{
    class RegenerationStatus : EntityStatus
    {
        float amountPerTick;
        int nTicks;
        public RegenerationStatus(LivingEntity entity, float amount, float duration)
            : base(entity)
        {
            nTicks = (int)(duration / 1000);
            amountPerTick = amount / nTicks;
        }

        public override void Activate()
        {
            for (int i = 1; i == nTicks; i++)
                NanoGame.Awaiter.Delay(1000*i).ContinueWith(t => Entity.Heal(amountPerTick));
            Entity.RemoveStatus(this);
        }
    }
}
