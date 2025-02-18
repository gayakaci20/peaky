using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Peaky.Services
{
    public class PreviewWindowService
    {
        private readonly IEnumerable<IFilePreviewService> _previewServices;
        private Window? _previewWindow;

        public PreviewWindowService(IEnumerable<IFilePreviewService> previewServices)
        {
            _previewServices = previewServices;
        }

        public async void ShowPreview(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            // Find a preview service that can handle this file type
            foreach (var service in _previewServices)
            {
                if (service.CanPreview(filePath))
                {
                    // Create or reuse preview window
                    if (_previewWindow == null)
                    {
                        _previewWindow = new Window
                        {
                            Title = "File Preview",
                            Width = 800,
                            Height = 600,
                            WindowStyle = WindowStyle.None,
                            AllowsTransparency = true,
                            Background = System.Windows.Media.Brushes.Transparent,
                            Topmost = true
                        };

                        _previewWindow.Closed += (s, e) => _previewWindow = null;
                    }

                    try
                    {
                        // Create preview control
                        var previewControl = await service.CreatePreviewAsync(filePath);
                        _previewWindow.Content = previewControl;

                        // Show the window if it's not already visible
                        if (!_previewWindow.IsVisible)
                        {
                            _previewWindow.Show();
                        }

                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating preview: {ex.Message}", "Preview Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            MessageBox.Show("No preview available for this file type.", "Preview Unavailable",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ClosePreview()
        {
            if (_previewWindow != null && _previewWindow.IsVisible)
            {
                _previewWindow.Close();
                _previewWindow = null;
            }
        }
    }
}