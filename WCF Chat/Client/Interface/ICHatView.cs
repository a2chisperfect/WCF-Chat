using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interface
{
    interface IChatView:IErrorShow
    {
        object DataContext { get; set; }
        void Show();
        void Close();

        event Action CloseHandler;
    }
}
