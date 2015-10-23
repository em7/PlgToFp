using System.Threading.Tasks;

namespace PlgToFp.Windows.Infrastructure.Interaction
{
    /// <summary>
    /// A service providing the user interaction
    /// </summary>
    public interface IInteractionService
    {
        /// <summary>
        /// Synchronously shows OpenFileDialog
        /// </summary>
        /// <returns></returns>
        string ShowOpenFileDialog();
        /// <summary>
        /// Asynchronous version for ShowOpenFileDialog
        /// </summary>
        /// <returns></returns>
        Task<string> ShowOpenFileDialogAsync();
    }
}