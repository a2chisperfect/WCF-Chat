using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ChatServiceReference;
using System.Collections.ObjectModel;
using System.ServiceModel;


namespace Client.Model
{
    class ChatClient : IChatCallback,IDisposable
    {
        public UserInfo user { get; set; }
        public ObservableCollection<User> users { get; set; }
        public ObservableCollection<Message> history { get; set; }

        public EventHandler<PrivateMessageEventArgs> PrivateMessage;
        private ChatServiceReference.ChatClient client { get; set; }
        public ChatClient()
        {

            history = new ObservableCollection<Message>();
            users = new ObservableCollection<User>();
            InstanceContext s = new InstanceContext(this);
            client = new ChatServiceReference.ChatClient(s, "tcpChatService");

        }
        public async Task<bool> Login(string name)
        {
            user = new UserInfo() { Id = Guid.NewGuid(), Name = name };
            var tmp = await client.JoinAsync(user);
            foreach (var u in tmp)
            {
                users.Add(new User(u));
            }
            return true;
        }

        public async Task SendMessageAsync(string msg)
        {
            var m = new Message() { message = msg, Sender = user, Time = DateTime.Now };
            await client.SendMessageAsync(m);
            history.Add(m);
        }
        public async Task SendPrivateMessageAsync(string msg, UserInfo reciver)
        {
            var m = new Message() { message = msg, Sender = user, Reciver = reciver, Time = DateTime.Now };
            await client.SendPrivateMessageAsync(m);
            users.FirstOrDefault(u => u.user.Id == reciver.Id).history.Add(m);
        }

        public void RecivePrivateMessage(Message msg)
        {
            var t = users.FirstOrDefault(u => u.user.Id == msg.Sender.Id);
            t.history.Add(msg);
            if(PrivateMessage !=null)
            {
                PrivateMessage(this,new PrivateMessageEventArgs(msg.Sender));
            }
           
        }

        public async void Leave()
        {
            await client.LeaveAsync(user);
        }

        public void UserJoin(UserInfo user)
        {
            users.Add(new User(user));
        }

        public void ReciveMessage(Message msg)
        {
            history.Add(msg);
        }

        public void UserLeave(UserInfo user)
        {
            users.Remove(users.FirstOrDefault(u => u.user.Id == user.Id));
        }

        public void Dispose()
        {
            client.Close();
        }
    }

    class PrivateMessageEventArgs : EventArgs
    {
        public UserInfo user { get; set; }
        public PrivateMessageEventArgs(UserInfo user)
        {
            this.user = user;
        }
    }
}
