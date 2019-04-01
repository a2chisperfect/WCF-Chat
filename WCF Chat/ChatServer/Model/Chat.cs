using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Contract;
using System.ServiceModel;
using System.Collections.ObjectModel;

namespace Server.Model
{
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    class Chat:IChat
    {
        public ObservableCollection<UserInfo> Join(UserInfo user)
        {
            return ChatServer.UserJoin(user, OperationContext.Current.GetCallbackChannel<IClientCallback>());

        }

        public void SendMessage(Message msg)
        {
         
            ChatServer.BroadcastMessage(msg);
        }

        public void SendPrivateMessage(Message msg)
        {
            ChatServer.SendPrivateMessage(msg);
         
        }

        public void Leave(UserInfo user)
        {
           
            ChatServer.DisconnectUser(user);
        }
    }
}
