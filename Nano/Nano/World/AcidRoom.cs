using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Nano.World
{
    class AcidRoom : Room
    {
        public AcidRoom(Rectangle location)
            :base(location)
        {
            
        }
        public override void OnPlayerEnter()
        {
            throw new NotImplementedException();
        }

        public override void OnPlayerLeave()
        {
            throw new NotImplementedException();
        }
    }
}
