using System.Numerics;

namespace Raytracer
{
    public class Ray
    {
        public Vector3 Origin { get; private set; }

        public Vector3 Direction { get; private set; }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        /// <summary>
        /// Get point that is located on a path o this Ray, at the point t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3 At(float t)
        {
            return Origin + Direction * t;
        }
    }
}
