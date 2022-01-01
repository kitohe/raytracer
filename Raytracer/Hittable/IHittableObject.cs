namespace Raytracer.Hittable
{
    public interface IHittableObject
    {
        (bool hit, HitRecord hitRecord) Hit(Ray ray, float tMin, float tMax);
    }
}
