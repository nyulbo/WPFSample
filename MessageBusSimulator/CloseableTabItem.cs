using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MessageBus;
using System.Collections.Concurrent;

namespace MessageBusSimulator
{
    class CloseableTabItem : TabItem
    {   
        public int Idx { get; set; }

        public void SetHeader(UIElement header)
        {
            // Container for header controls
            var dockPanel = new DockPanel();
            dockPanel.Children.Add(header);

            // Close button to remove the tab
            var closeButton = new TabCloseButton();
            closeButton.Click +=
                (sender, e) =>
                {
                    var tabControl = Parent as ItemsControl;
                    tabControl.Items.Remove(this);

                    IBus bus;
                    TabWin.Buses.TryRemove(Idx, out bus);
                    if (bus != null)
                        bus.Dispose();
                };
            dockPanel.Children.Add(closeButton);

            // Set the header
            Header = dockPanel;
        }
    }
}
