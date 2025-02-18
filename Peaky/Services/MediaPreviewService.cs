using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Peaky.Services
{
    public class MediaPreviewService : IFilePreviewService
    {
        private readonly string[] _supportedExtensions = { ".mp4", ".avi", ".mkv", ".mp3", ".wav", ".m4a" };

        public bool CanPreview(string filePath)
        {
            return _supportedExtensions.Contains(Path.GetExtension(filePath).ToLower());
        }

        public async Task<Control> CreatePreviewAsync(string filePath)
        {
            var mediaElement = new MediaElement
            {
                Source = new Uri(filePath),
                LoadedBehavior = MediaState.Manual,
                UnloadedBehavior = MediaState.Stop,
                Stretch = Stretch.Uniform,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };

            var grid = new Grid();
            grid.Children.Add(mediaElement);

            var controlPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                Margin = new System.Windows.Thickness(0, 0, 0, 10)
            };

            var playButton = new Button { Content = "Play" };
            playButton.Click += (s, e) => mediaElement.Play();

            var pauseButton = new Button { Content = "Pause" };
            pauseButton.Click += (s, e) => mediaElement.Pause();

            var stopButton = new Button { Content = "Stop" };
            stopButton.Click += (s, e) => mediaElement.Stop();

            controlPanel.Children.Add(playButton);
            controlPanel.Children.Add(pauseButton);
            controlPanel.Children.Add(stopButton);

            grid.Children.Add(controlPanel);

            return grid;
        }

        public string[] GetSupportedExtensions()
        {
            return _supportedExtensions;
        }
    }
}