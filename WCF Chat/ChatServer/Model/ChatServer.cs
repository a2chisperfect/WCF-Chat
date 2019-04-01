using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Server.Contract;

namespace Server.Model
{
    static class ChatServer
    {
        public static ObservableCollection<ChatClient> clients { get; set; }
        static ChatServer()
        {
            clients = new ObservableCollection<ChatClient>();
        }
        public static ObservableCollection<UserInfo> UserJoin(UserInfo user, IClientCallback callback)
        {
            clients.Add(new ChatClient(user, callback));
            foreach (var client in clients)
            {
                if (client.User.Id != user.Id)
                {
                    client.UserJoin(user);
                }
            }
            return new ObservableCollection<UserInfo>(clients.Select(c => c.User).ToList().Where(c => c.Id != user.Id).ToList());
        }

        public static void BroadcastMessage(Message msg)
        {
            foreach (var client in clients)
            {
                if (client.User.Id != msg.Sender.Id)
                {
                    client.ReciveMessage(msg);
                }
            }
        }

        public static void SendPrivateMessage(Message msg)
        {
            var r = clients.FirstOrDefault(c => c.User.Id == msg.Reciver.Id);
            r.RecivePrivateMessage(msg);
        }

        public static void DisconnectUser(UserInfo user)
        {
            clients.Remove(clients.FirstOrDefault(c => c.User.Id == user.Id));
            foreach (var client in clients)
            {
                if (client.User.Id != user.Id)
                {
                    client.UserLeave(user);
                }
            }
        }


    }
}
