using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Client.ViewModel;

namespace Client
{
    static class Navigation
    {
        static LoginViewModel login;
        static ChatViewModel chat;

        static Navigation()
        {
            login = new LoginViewModel(new LogIn(), new Model.ChatClient());
            chat = new ChatViewModel(new ChatView(), login.Model);
        }
        public static void NavigateToLogin()
        {
            Application.Current.MainWindow = login.View as Window;
            login.Show();
        }
        public static void NavigateToMain()
        {
            Application.Current.MainWindow = chat.View as Window;
            login.Close();
            chat.Show();
        }
    }
}
