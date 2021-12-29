namespace Raytracer
{
    public class AppSettings
    {
        public int ImageHeight { get; init; }

        public int ImageWidth { get; init; }

        public ImageFormat OutputFormat { get; init; }

        public string OutputFilename { get; init; }

        public string AspectRatio { get; init; }

        public float ViewportHeight { get; init; }

        public float FocalLength { get; init; }
    }
}
