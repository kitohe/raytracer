namespace Raytracer
{
    public record Image
    {
        public byte[] Data { get; set; }

        public int Width { get; init; }

        public int Height { get; init; }

        public ImageFormat Format { get; init; }

        public string FileName { get; init; }

        public string FullFileName => $"{FileName}.{Format}";
    }
}
