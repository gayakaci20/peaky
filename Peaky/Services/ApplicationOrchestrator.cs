using System;
using System.Threading.Tasks;

namespace Peaky.Services
{
    public class ApplicationOrchestrator : IDisposable
    {
        private readonly KeyboardHookService _keyboardHookService;
        private readonly FileSelectionService _fileSelectionService;
        private readonly PreviewWindowService _previewWindowService;
        private readonly ILoggingService _loggingService;

        public ApplicationOrchestrator(
            KeyboardHookService keyboardHookService,
            FileSelectionService fileSelectionService,
            PreviewWindowService previewWindowService,
            ILoggingService loggingService)
        {
            _keyboardHookService = keyboardHookService;
            _fileSelectionService = fileSelectionService;
            _previewWindowService = previewWindowService;
            _loggingService = loggingService;

            _keyboardHookService.SpacebarPressed += OnSpacebarPressed;
        }

        private async void OnSpacebarPressed(object? sender, EventArgs e)
        {
            try
            {
                var selectedFile = await _fileSelectionService.GetSelectedFileAsync();
                if (selectedFile != null)
                {
                    await _previewWindowService.ShowPreviewAsync(selectedFile);
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error occurred while showing preview", ex);
            }
        }

        public void Dispose()
        {
            _keyboardHookService.SpacebarPressed -= OnSpacebarPressed;
        }
    }
}