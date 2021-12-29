namespace Raytracer.Services
{
    public interface IImageService
    {
        Task WriteImage(Image image);
    }

    public class P3ImageService: IImageService
    {
        private readonly IFileService _fileService;

        public P3ImageService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task WriteImage(Image image)
        {
            await _fileService.WriteFile(
                data: image.Data,
                path: image.FullFileName,
                imageWidth: image.Width,
                imageHeight: image.Height);
        }
    }
}
