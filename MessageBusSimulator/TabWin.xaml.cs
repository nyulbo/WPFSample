using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBus;

namespace MessageBusSimulator
{
    /// <summary>
    /// TabTestWin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabWin : Window
    {   
        public static ConcurrentDictionary<int, IBus> Buses = new ConcurrentDictionary<int, IBus>();
        public static int TabIdx;

        public TabWin()
        {
            InitializeComponent();
        }
        //private void Publish_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.NavigationService.Navigate(new Uri("/Sender.xaml", UriKind.Relative));
        //}
        //private void Subscribe_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.NavigationService.Navigate(new Uri("/Receiver.xaml", UriKind.Relative));
        //}
        //private void Request_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.NavigationService.Navigate(new Uri("/Sender.xaml", UriKind.Relative));
        //}
        //private void Reply_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.NavigationService.Navigate(new Uri("/Receiver.xaml", UriKind.Relative));
        //}
        private void AddTab_Click(object sender, RoutedEventArgs e)
        {
            TabItem newTabItem = new TabItem
            {
                Header = "Test",
                Name = "Test"
            };
            mainTab.Items.Add(newTabItem);
            newTabItem.Focus();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = false;
            Environment.Exit(0);
            //Application.Current.Shutdown();
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void OnPlusTabClick(object sender, RoutedEventArgs e)
        {
            MenuItem mn = (MenuItem)sender;
            // Create the header
            var header = new TextBlock { Text = mn.Header.ToString() };

            // Create the content
            //var content = new TextBlock
            //{
            //    Text = string.Format("Tab numero {0}-o",
            //        mainTab.Items.Count + 1),
            //};
            
            // Create the tab
            var tab = new CloseableTabItem();
            tab.SetHeader(header);
            Frame tabFrame = new Frame();

            Assembly asm = Assembly.GetExecutingAssembly();
            Type t = asm.GetType(string.Format("MessageBusSimulator.{0}", mn.Header.ToString()));
            tab.Idx = TabIdx++;
            var bus = ConnectionManager.CreateBus();
            TabWin.Buses.TryAdd(tab.Idx, bus);
            Page pg = (Page)Activator.CreateInstance(t, tab.Idx);
            
            tabFrame.Content = pg;
            tab.Content = tabFrame;

            // Add to TabControl
            mainTab.Items.Add(tab);
            tab.Focus();
        }
    }
}
