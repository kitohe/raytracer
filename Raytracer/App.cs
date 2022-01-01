using System.Numerics;
using System.Text;
using Microsoft.Extensions.Options;
using Raytracer.Services;

namespace Raytracer
{
    public class App
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IImageService _imageService;
        private readonly int _imageHeight;
        private readonly int _imageWidth;

        public App(IOptions<AppSettings> appSettings, IImageService imageService)
        {
            _appSettings = appSettings;
            _imageService = imageService;
            _imageHeight = _appSettings.Value.ImageHeight;
            _imageWidth = GetImageWidth(_imageHeight, appSettings.Value.AspectRatio);
        }

        public async Task Run()
        {
            var sb = new StringBuilder();

            const float focalLength = 1f;
            const float viewportHeight = 2f;
            const float viewportWidth = viewportHeight * (16f / 9f);

            var origin = Vector3.Zero;
            var horizontal = new Vector3(viewportWidth, 0, 0);
            var vertical = new Vector3(0, viewportHeight, 0);
            var downLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focalLength);

            for (var i = 0; i < _imageHeight; i++)
            {
                for (var j = 0; j < _imageWidth; j++)
                {
                    var v = (float) i / (_imageHeight - 1);
                    var u = (float) j / (_imageWidth - 1);

                    var ray = new Ray(origin, downLeftCorner + horizontal * u + vertical * v - origin);
                    var color = RayColor(ray);

                    sb.AppendLine($"{color.X * 255.99f} {color.Y * 255.99f} {color.Z * 255.99f}");
                }
            }

            var image = new Image
            {
                Data = Encoding.UTF8.GetBytes(sb.ToString()),
                Format = _appSettings.Value.OutputFormat,
                Width = _imageWidth,
                Height = _imageHeight,
                FileName = _appSettings.Value.OutputFilename
            };

            await _imageService.WriteImage(image);
        }

        private static int GetImageWidth(float imageHeight, string aspectRatio)
        {
            var aspectRatioSplitted = aspectRatio.Split(":").Select(float.Parse).ToArray();
            var aspectRatioValue = aspectRatioSplitted[0] / aspectRatioSplitted[1];
            var imageWidth = imageHeight * aspectRatioValue;

            return (int)imageWidth;
        }

        private static Vector3 RayColor(Ray ray)
        {
            if (SphereHit(ray, new Vector3(0.0f, 0.0f, -1f), 0.5f))
            {
                return new Vector3(1f, 0f, 0f);
            }

            var amount = Math.Abs(Vector3.Normalize(ray.Direction).Y);
            
            return Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0.7f, 1f), amount);
        }

        private static bool SphereHit(Ray ray, Vector3 sphereCenter, float radius)
        {
            var oc = ray.Direction - sphereCenter;
            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = 2f * Vector3.Dot(oc, ray.Direction);
            var c = Vector3.Dot(oc, oc) - radius * radius;
            var delta = b * b - 4f * a * c;
            if (delta > 0) Console.WriteLine(delta);
            return delta > 0;
        }
    }
}
