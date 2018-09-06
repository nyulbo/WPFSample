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
using System.Windows.Shapes;
using System.Threading;
using System.Collections.ObjectModel;
using MessageBus;

namespace MessageBusSimulator
{
    /// <summary>
    /// ReceiverWin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReceiverWin : Window
    {
        private bool isStart = false;
        public ObservableCollection<MessageResult> MessageResultCollection { get; } = new ObservableCollection<MessageResult>();
        IBus bus;
        public ReceiverWin()
        {
            InitializeComponent();
        }

        private void CallApi(string apiName, MessageConfiguration messageConfig)
        {
            bus = ConnectionManager.CreateBus();

            if (apiName.Equals("Subscribe"))
            {
                bus.Subscribe((body, prop, info) => Task.Factory.StartNew(() =>
                {
                    listView.Dispatcher.BeginInvoke((Action)(() => {
                        var message = Encoding.UTF8.GetString(body);
                        MessageResultCollection.Add(new MessageResult
                        {
                            Body = message,
                            No = MessageResultCollection.Count.ToString(),
                            ConsumerTag = info.ConsumerTag,
                            DeliveryTag = info.DeliverTag.ToString()
                        });
                        listView.ItemsSource = MessageResultCollection;
                    }));
                }), messageConfig);

            }

        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //isStart = true;
            //new Thread(doPerformanceCounter).Start();
            var messageConfig = new MessageConfiguration
            {
                QueueName = txtQueue.Text
            };

            var messageBusApi = (MessageBusApi)cmbMessageApi.SelectedValue;
            CallApi(messageBusApi.Name, messageConfig);
        }
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            //isStart = false;
            bus.Dispose();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(bus != null)
                bus.Dispose();
            e.Cancel = false;
        }
    }
}
