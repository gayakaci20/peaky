using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Peaky.Services
{
    public class TextFilePreviewService : IFilePreviewService
    {
        private readonly string[] _supportedExtensions = { ".txt", ".log", ".ini", ".csv", ".xml", ".json" };

        public bool CanPreview(string filePath)
        {
            return _supportedExtensions.Contains(Path.GetExtension(filePath).ToLower());
        }

        public async Task<Control> CreatePreviewAsync(string filePath)
        {
            var textBox = new TextBox
            {
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                Background = System.Windows.Media.Brushes.Transparent,
                Foreground = System.Windows.Media.Brushes.White
            };

            try
            {
                string content = await File.ReadAllTextAsync(filePath);
                textBox.Text = content;
            }
            catch (Exception ex)
            {
                textBox.Text = $"Error loading file: {ex.Message}";
            }

            return textBox;
        }

        public string[] GetSupportedExtensions()
        {
            return _supportedExtensions;
        }
    }
}