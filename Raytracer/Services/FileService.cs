namespace Raytracer.Services
{
    public interface IFileService
    {
        Task WriteFile(byte[] data, string path, int imageWidth, int imageHeight);
    }

    public interface IP3FileService : IFileService
    {
    }

    public class P3FileService : IP3FileService
    {
        public async Task WriteFile(byte[] data, string fileName, int imageWidth, int imageHeight)
        {
            var stringImage = System.Text.Encoding.UTF8.GetString(data);
            var header = GetP3Header(imageWidth, imageHeight);
            var image = header + stringImage;

            await File.WriteAllTextAsync(fileName, image);
        }

        private static string GetP3Header(int width, int height)
        {
            return $"P3\n{width} {height}\n255\n";
        }
    }
}
