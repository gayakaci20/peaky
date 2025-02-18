using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Peaky
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        public App()
        {
            Services = ConfigureServices();
        }

        public static IServiceProvider Services { get; private set; } = null!;

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register services
            services.AddSingleton<KeyboardHookService>();
            services.AddSingleton<FileSelectionService>();
            services.AddSingleton<PreviewWindowService>();
            services.AddSingleton<IFilePreviewService, TextFilePreviewService>();
            services.AddSingleton<IFilePreviewService, ImagePreviewService>();
            services.AddSingleton<IFilePreviewService, MediaPreviewService>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddSingleton<ApplicationOrchestrator>();

            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _serviceProvider = Services as ServiceProvider;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _serviceProvider?.Dispose();
        }
    }
}