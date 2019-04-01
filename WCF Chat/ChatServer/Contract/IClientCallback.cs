using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Server.Model;

namespace Server.Contract
{
    interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void UserJoin(UserInfo user);
        [OperationContract(IsOneWay = true)]
        void ReciveMessage(Message msg);
        [OperationContract(IsOneWay = true)]
        void RecivePrivateMessage(Message msg);
        [OperationContract(IsOneWay = true)]
        void UserLeave(UserInfo user);
    }
}
