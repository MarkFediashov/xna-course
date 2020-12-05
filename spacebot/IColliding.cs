using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Game1
{
    public interface IColliding
    {
        bool ShouldDisposeOnCollideWithBullet();
        bool ShouldDisposeBullet();
        Rectangle GetBounds();
        void OnHitNotify();
    }
}
