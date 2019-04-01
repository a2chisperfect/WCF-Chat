using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Contract;
using System.ServiceModel;

namespace Server.Model
{
    class ChatClient
    {
        public UserInfo User { get; set; }
        public IClientCallback Callback { get; set; } 
        public ChatClient(UserInfo user, IClientCallback callback)
        {
            User = user;
            Callback = callback;
        }
        public void UserJoin(UserInfo user)
        {
            try
            {
                Callback.UserJoin(user);
            }
            catch (CommunicationException)
            {
                ChatServer.DisconnectUser(User);
            }
        }
        public void ReciveMessage(Message msg)
        {
            try
            {
                Callback.ReciveMessage(msg);
            }
            catch(CommunicationException)
            {
                ChatServer.DisconnectUser(User);
            }
        }
        public void RecivePrivateMessage(Message msg)
        {
            try
            {
                Callback.RecivePrivateMessage(msg);
            }
            catch (CommunicationException)
            {
                ChatServer.DisconnectUser(User);
            }
        }
        public void UserLeave(UserInfo user)
        {
            try
            {
                Callback.UserLeave(user);
            }
            catch (CommunicationException)
            {
                ChatServer.DisconnectUser(User);
            }
        }

    }

}
