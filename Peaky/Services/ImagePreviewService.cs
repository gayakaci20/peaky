using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Peaky.Services
{
    public class ImagePreviewService : IFilePreviewService
    {
        private readonly string[] _supportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        public bool CanPreview(string filePath)
        {
            return _supportedExtensions.Contains(Path.GetExtension(filePath).ToLower());
        }

        public async Task<Control> CreatePreviewAsync(string filePath)
        {
            var image = new Image
            {
                Stretch = System.Windows.Media.Stretch.Uniform,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                image.Source = bitmap;
            }
            catch (Exception ex)
            {
                var textBlock = new TextBlock
                {
                    Text = $"Error loading image: {ex.Message}",
                    Foreground = System.Windows.Media.Brushes.Red,
                    TextWrapping = TextWrapping.Wrap
                };
                return textBlock;
            }

            return image;
        }

        public string[] GetSupportedExtensions()
        {
            return _supportedExtensions;
        }
    }
}