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
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Win32;

using MessageBus;

namespace MessageBusSimulator
{
    /// <summary>
    /// Simulator.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Request : Page
    {
        private bool isStart = false;
        private string result = string.Empty;
        public ObservableCollection<MessageResult> MessageResultCollection { get; } = new ObservableCollection<MessageResult>();

        public Request()
        {
            InitializeComponent();
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var messageConfig = new MessageConfiguration
            {
                ExchangeName = txtExchange.Text,
                RoutingKey = txtRouter.Text,
            };
            var messageType = (MessageType)cmdType.SelectedValue;
            CallApi(messageType.Type, messageConfig);
        }

        private void CallApi(string type, MessageConfiguration messageConfig)
        {
            using (var bus = ConnectionManager.CreateBus())
            {
                try
                {
                    var message = GetMessage(type);
                    bus.Publish(messageConfig, message);
                    MessageResultCollection.Add(new MessageResult { Text = "Sucess", No = MessageResultCollection.Count.ToString() });
                }
                catch
                {
                    MessageResultCollection.Add(new MessageResult { Text = "Error", No = MessageResultCollection.Count.ToString() });
                }
                listView.ItemsSource = MessageResultCollection;
            }
        }
        private byte[] GetMessage(string type)
        {
            byte[] ret = null;
            if (type.Equals("String"))
            {
                ret = Encoding.UTF8.GetBytes(txtMessage.Text);
            }
            else
            {
                ret = File.ReadAllBytes(txtBlockFileName.Text);
            }

            return ret;
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = "Document"; // Default file name
            //dlg.DefaultExt = ".txt"; // Default file extension
            //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                //string filename = dlg.FileName;
                txtBlockFileName.Text = dlg.FileName;
            }
        }
    }
}
