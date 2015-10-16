using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Infrastructure
{
    /// <summary>
    /// A service providing the user interaction
    /// </summary>
    public class InteractionService : IInteractionService
    {
        public string ShowOpenFileDialog()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
                return dlg.FileName;
            return null;
        }

        public Task<string> ShowOpenFileDialogAsync()
        {
            return Task.Run(() => ShowOpenFileDialog());
        }
    }
}
