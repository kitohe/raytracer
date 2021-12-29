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

            const float focalLenght = 1f;
            const float viewportHeight = 2f;
            var viewportWidth = viewportHeight * (16f / 9f);
            var origin = Vector3.Zero;

            var downLeftCorner = new Vector3(origin.X - viewportWidth / 2, origin.Y - viewportHeight / 2, -focalLenght);

            for (var i = 0; i < _imageHeight; i++)
            {
                for (var j = 0; j < _imageWidth; j++)
                {
                    var u = (float) i / _imageHeight;
                    var v = (float) j / _imageWidth;

                    var ray = new Ray(Vector3.Zero, new Vector3(downLeftCorner.X + v, downLeftCorner.Y + u, downLeftCorner.Z) - origin);
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

        private int GetImageWidth(float imageHeight, string aspectRatio)
        {
            var aspectRatioSplitted = aspectRatio.Split(":").Select(float.Parse).ToArray();
            var aspectRatioValue = aspectRatioSplitted[0] / aspectRatioSplitted[1];
            var imageWidth = imageHeight * aspectRatioValue;

            return (int)imageWidth;
        }

        private Vector3 RayColor(Ray ray)
        {
            var amount = Math.Abs(Vector3.Normalize(ray.Direction).Y);
            
            return Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0.7f, 1f), amount);
        }
    }
}
