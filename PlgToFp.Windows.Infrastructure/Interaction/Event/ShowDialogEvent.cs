using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Infrastructure.Interaction.Event
{
    /// <summary>
    /// Event that some view explicitely wants to show dialog
    /// </summary>
    public class ShowDialogEvent : PubSubEvent<ShowDialogEventPayload>
    {
    }
}
