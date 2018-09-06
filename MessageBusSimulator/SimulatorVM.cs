using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using MessageBus;

namespace MessageBusSimulator
{   
    public class MessageBusApi
    {
        public MessageBusApi(string name, ApiType type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; set; }
        public ApiType Type { get; set; }
    }
    public enum ApiType
    {
        Publish,
        Subscribe,
        Request,
        Reply,
        Queue
    }
    public class MessageType
    {
        public MessageType(string type)
        {
            Type = type;
        }
        public string Type { get; set; }
    }
    public class MessageResult
    {   
        public string No { get; set; }
        public string Text { get; set; }
        public string Body { get; set; }
        public string ConsumerTag { get; set; }
        public string DeliveryTag { get; set; }
    }
   
    class SimulatorVM : Notifier
    {
        private IEnumerable<MessageBusApi> messageBusApies;

        public IEnumerable<MessageBusApi> MessageBusApies
        {
            get { return messageBusApies; }
            set
            {
                messageBusApies = value;
                OnPropertyChanged("MessageBusApies");
            }
        }
        private IEnumerable<MessageType> messageTypes;

        public IEnumerable<MessageType> MessageTypes
        {
            get { return messageTypes; }
            set
            {
                messageTypes = value;
                OnPropertyChanged("MessageTypes");
            }
        }
        public SimulatorVM()
        {   
            messageBusApies = new MessageBusApi[] {
                new MessageBusApi("Publish", ApiType.Publish),
                new MessageBusApi("Subscribe", ApiType.Subscribe),
                new MessageBusApi("Request", ApiType.Request),
                new MessageBusApi("Reply", ApiType.Reply)
            };
            messageTypes = new MessageType[] {
                new MessageType("String"),
                new MessageType("Binary")
            };
        }
        public IEnumerable<MessageBusApi> PublishMessageBusApies
        {
            get
            {
                return messageBusApies.Where(api => api.Type.Equals(ApiType.Publish));
            }
        }
        public IEnumerable<MessageBusApi> SubscribeMessageBusApies
        {
            get
            {
                return messageBusApies.Where(api => api.Type.Equals(ApiType.Subscribe));
            }
        }
        public IEnumerable<MessageBusApi> RequestMessageBusApies
        {
            get
            {
                return messageBusApies.Where(api => api.Type.Equals(ApiType.Request));
            }
        }
        public IEnumerable<MessageBusApi> ReplyMessageBusApies
        {
            get
            {
                return messageBusApies.Where(api => api.Type.Equals(ApiType.Reply));
            }
        }
        private MessageBusApi selectedMessageBusApi;
        public MessageBusApi SelectedMessageBusApi
        {
            get { return selectedMessageBusApi; }
            set
            {
                selectedMessageBusApi = value;
                OnPropertyChanged("SelectedMessageBusApi");
                OnSelectedMessageBusApiChanged();
            }
        }
        private void OnSelectedMessageBusApiChanged()
        {

        }
        private MessageBusApi selectedMessageType;
        public MessageBusApi SelectedMessageType
        {
            get { return selectedMessageType; }
            set
            {
                selectedMessageType = value;
                OnPropertyChanged("SelectedMessageType");
                OnSelectedMessageTypeChanged();
            }
        }
        private void OnSelectedMessageTypeChanged()
        {

        }
    }
}
