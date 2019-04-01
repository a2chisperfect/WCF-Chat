using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ServiceModel;
using Client.Interface;
using Client.Model;

namespace Client.ViewModel
{
    class LoginViewModel
    {
        public string UserName { get; set; }
        public ICommand Login { get; set; }
        public ChatClient Model { get; set; }


        private ILoginView _view { get; set; }

        public ILoginView View
        {
            get { return _view; }
            set { _view = value; }
        }

        public LoginViewModel(ILoginView view, ChatClient model)
        {
            Login = new RelayCommand(LoginCommand_Execute, Login_CanExecute);
            _view = view;
            Model = model;
            _view.DataContext = this;
           

        }
        private async void LoginCommand_Execute()
        {
            try
            {
                await Model.Login(UserName);
                Navigation.NavigateToMain();
            }
            catch (CommunicationException)
            {
                _view.ShowError("Ooops no connection to server... Try again later.");
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        public bool Login_CanExecute()
        {
            if (String.IsNullOrEmpty(UserName))
                return false;
            else
                return true;
        }
        public void Show()
        {
            _view.Show();
        }
        public void Close()
        {
            _view.Close();
        }
    }
}
