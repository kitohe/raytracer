using System.Numerics;

namespace Raytracer.Hittable.Objects
{
    public class Sphere : IHittableObject
    {
        public Vector3 Center { get; private set; }

        public float Radius { get; private set; }

        public Sphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public (bool hit, HitRecord hitRecord) Hit(Ray ray, float tMin, float tMax)
        {
            var oc = ray.Origin - Center;
            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = 2f * Vector3.Dot(oc, ray.Direction);
            var c = Vector3.Dot(oc, oc) - Radius * Radius;

            var delta = b * b - 4f * a * c;

            if (delta < 0f)
            {
                return (false, null);
            }

            var root = (-b - MathF.Sqrt(delta)) / (2 * a);

            // If first solution is not in acceptable range, calculate next one
            if (tMin > root || root > tMax)
            {
                root = (-b + MathF.Sqrt(delta)) / (2 * a);

                // If second solution is also out of range, treat this as if we didn't hit
                if (tMin > root || root > tMax)
                {
                    return (false, null);
                }
            }

            // Get point at which intersection between Ray and Sphere was made
            var hitPoint = ray.At(root);

            // Calculate a unit vector from the Center of the Sphere, to the hit point.
            // This is normal (vector) to that point (hit point)
            var hitNormal = Vector3.Normalize(hitPoint - Center);

            var hitRecord = new HitRecord(
                hitPoint: hitPoint,
                hitNormal: hitNormal,
                hitDistance: root);

            return (true, hitRecord);
        }
    }
}
