using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PlgToFp.Windows.Infrastructure;
using PlgToFp.Windows.Infrastructure.Interaction.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlgToFp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        private IEventAggregator _evtAggregator;
        private IList<CustomDialog> _shownDialogs = new List<CustomDialog>(1);
        public Shell(IEventAggregator evtAggregator)
        {
            _evtAggregator = evtAggregator;
            RegisterEvents();
            InitializeComponent();
        }

        private void RegisterEvents()
        {
            _evtAggregator.GetEvent<ShowDialogEvent>().Subscribe(async payload =>
            {
                await ShowDialogAsync(payload);
            });

            _evtAggregator.GetEvent<CloseDialogEvent>().Subscribe(async payload =>
            {
                await CloseDialogAsync(payload);
            });
        }

        private async Task ShowDialogAsync(ShowDialogEventPayload payload)
        {
            if (payload == null 
                || payload.DialogContent == null
                || (! (payload.DialogContent is FrameworkElement)))
                return;

            var cnt = (FrameworkElement)payload.DialogContent;
            var dlg = new CustomDialog();
            dlg.Content = cnt;
            dlg.Title = payload.Header;
            _shownDialogs.Add(dlg);

            await this.ShowMetroDialogAsync(dlg);
        }

        private async Task CloseDialogAsync(CloseDialogEventPayload payload)
        {
            if (payload == null
                || payload.DialogContent == null
                || (!(payload.DialogContent is FrameworkElement)))
                return;

            var cnt = (FrameworkElement)payload.DialogContent;
            var dlg = _shownDialogs.FirstOrDefault(d => d.Content == cnt);

            if (dlg != null)
            {
                await this.HideMetroDialogAsync(dlg);
                _shownDialogs.Remove(dlg);
            }
            
        }
    }
}
