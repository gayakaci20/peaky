using System.Threading.Tasks;
using System.Windows.Controls;

namespace Peaky.Services
{
    public interface IFilePreviewService
    {
        /// <summary>
        /// Checks if this preview service can handle the specified file type
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>True if this service can handle the file type</returns>
        bool CanPreview(string filePath);

        /// <summary>
        /// Creates a preview control for the specified file
        /// </summary>
        /// <param name="filePath">Path to the file to preview</param>
        /// <returns>A control containing the preview</returns>
        Task<Control> CreatePreviewAsync(string filePath);

        /// <summary>
        /// Gets the supported file extensions for this preview service
        /// </summary>
        /// <returns>Array of supported file extensions (e.g., ".txt", ".jpg")</returns>
        string[] GetSupportedExtensions();
    }
}