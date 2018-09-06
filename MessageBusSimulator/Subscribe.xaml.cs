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
using System.Collections.ObjectModel;
using MessageBus;

namespace MessageBusSimulator
{
    /// <summary>
    /// Receiver.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Subscribe : Page
    {   
        public ObservableCollection<MessageResult> MessageResultCollection { get; } = new ObservableCollection<MessageResult>();
        int idx;
        
        public Subscribe(int idx)
        {
            InitializeComponent();
            this.idx = idx;
  
        }
        private void CallApi(string apiName, MessageConfiguration messageConfig)
        {
            //bus = ConnectionManager.CreateBus();
            IBus bus;
            TabWin.Buses.TryGetValue(idx, out bus);
            if(bus == null)
            {
                bus = ConnectionManager.CreateBus();
                TabWin.Buses.TryAdd(idx, bus);
            }

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
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {   
            var messageConfig = new MessageConfiguration
            {
                QueueName = txtQueue.Text,
                PrefetchCount = ushort.Parse(txtPrefetch.Text)
            };
            var messageBusApi = (MessageBusApi)cmbMessageApi.SelectedValue;
            CallApi(messageBusApi.Name, messageConfig);
        }
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            IBus bus;
            TabWin.Buses.TryRemove(idx, out bus);
            if (bus != null)
                bus.Dispose();
        }
    }
}
