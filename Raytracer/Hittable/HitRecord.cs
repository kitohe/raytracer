using System.Numerics;

namespace Raytracer.Hittable
{
    public class HitRecord
    {
        public Vector3 HitPoint { get; }

        public Vector3 HitNormal { get; }

        public float HitDistance { get; }

        public HitRecord(Vector3 hitPoint, Vector3 hitNormal, float hitDistance)
        {
            HitPoint = hitPoint;
            HitNormal = hitNormal;
            HitDistance = hitDistance;
        }
    }
}
