using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using Server.Model;
using System.Collections.ObjectModel;

namespace Server.Contract
{
    [ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
    interface IChat
    {
        [OperationContract(IsInitiating = true)]
        ObservableCollection<UserInfo> Join(UserInfo user);
        [OperationContract]
        void SendMessage(Message msg);
        [OperationContract]
        void SendPrivateMessage(Message msg);
        [OperationContract(IsTerminating = true)]
        void Leave(UserInfo user);
    }

   



   
}
