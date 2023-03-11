using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal class Transform
    {
        internal Vector2 position;
        internal Vector2 scale;

        internal float Left => position.X;
        internal float Right => position.X + scale.X;
        internal float Top => position.Y;
        internal float Bottom => position.Y + scale.Y;
        internal Rectangle RectangleScaledForGraphics(Graphics graphics) => new Rectangle(
            (int)(position.X),// * GameSession.ResolutionFactor),
            (int)(position.Y),// * GameSession.ResolutionFactor),
            (int)(scale.X * GameSession.SpacialScalar * 2f),
            (int)(scale.Y * GameSession.SpacialScalar * 2f)
            );

        internal bool IntersectsWith(Transform t)
        {
            if(Right < t.Left || Left > t.Right)
                return false;
            if (Bottom < t.Top || Top > t.Bottom)
                return false;
            return true;
        }
        static internal Transform GetTransformFromRectangle(Rectangle rect)
        {
            Transform transform = new Transform();
            transform.position.X = rect.X;
            transform.position.Y = rect.Y;
            transform.scale.X = rect.Width;
            transform.scale.Y = rect.Height;

            return transform;
        }
    }
}
