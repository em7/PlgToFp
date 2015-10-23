using Microsoft.Win32;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlgToFp.Windows.Infrastructure.Interaction;

namespace PlgToFp.Windows.Module.App.Interaction
{
    /// <summary>
    /// A service providing the user interaction
    /// </summary>
    public class InteractionService : IInteractionService
    {
        private ILoggerFacade _logger;

        public InteractionService(ILoggerFacade logger)
        {
            _logger = logger;
        }

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
