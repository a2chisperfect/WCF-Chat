using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ChatServiceReference;
using System.Collections.ObjectModel;

namespace Client.Model
{
    class User
    {
        public UserInfo user { get; set; }
        public ObservableCollection<Message> history { get; set; }
        public User(UserInfo user)
        {
            this.user = user;
            history = new ObservableCollection<Message>();
        }
    }
}
