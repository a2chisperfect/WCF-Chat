using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Interface;
using System.Windows;
using Client.Model;
using System.Windows.Input;
using System.ServiceModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Client.ChatServiceReference;

namespace Client.ViewModel
{
    class ChatViewModel: ViewModelBase
    {
        private IChatView _view { get; set; }
     
        public IChatView View
        {
            get { return _view; }
            set { _view = value; }
        }
        

        public Model.ChatClient Model { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand OpenTabCommand { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }
        

        public ICommand CloseTabCommand { get { return new ParamCommand(CloseTab); } }
        private void CloseTab(object tab)
        {
            var close = Tabs.FirstOrDefault(t=>(t.Tag as UserInfo).Id == (tab as UserInfo).Id );
            Tabs.Remove(close);
        }

        public ObservableCollection<ClosableTab> Tabs { get; set; }
        
        private ClosableTab _selectedTab;

        public ClosableTab SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                NotifyPropertyChanged();
            }
        }
        public User SelectedUser { get; set; } 
        
        public ChatViewModel(IChatView view, Model.ChatClient model)
        {
            Model = model;
            _view = view;
            _view.DataContext = this;
            SelectedUser = null;
            Tabs = new ObservableCollection<ClosableTab>();
            SendCommand = new RelayCommand(SendCommand_Execute, Send_CanExecute);
            OpenTabCommand = new RelayCommand(OpenTabCommand_Execute);
            _view.CloseHandler += _view_Closing;
            Model.PrivateMessage += PrivateMessageHandler; 
            ResourceDictionary myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source = new Uri("Styles.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
            AddLobbyTab();
        }
        public bool Send_CanExecute()
        {
            if (SelectedTab.Title == "Lobby")
                return true;

            var user = Model.users.FirstOrDefault(u => u.user.Id == (SelectedTab.Tag as UserInfo).Id);
            return user == null ? false : true;
        }

        void _view_Closing()
        {
            Model.Leave();
        }

        private void PrivateMessageHandler(object sender, PrivateMessageEventArgs e)
        {
            foreach (var item in Tabs)
            {
                if (item.Tag == null)
                    continue;
                if ((item.Tag as UserInfo).Id == e.user.Id)
                   return;
            }
            AddNewTab(e.user,false);
        }
        private async void SendCommand_Execute()
        {
            try
            {
                if(SelectedTab.Title == "Lobby")
                {
                    await Model.SendMessageAsync(Message);
                }
                else
                {
                    await Model.SendPrivateMessageAsync(Message, SelectedTab.Tag as UserInfo);
                }
                Message = String.Empty;
                
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
        private void OpenTabCommand_Execute()
        {
            foreach (var item in Tabs)
            {
                if (item.Tag == null)
                    continue;
                if ((item.Tag as UserInfo).Id == SelectedUser.user.Id)
                {
                    SelectedTab = item; return;
                }
            }
            AddNewTab(SelectedUser.user,true);
        }

        private void AddLobbyTab()
        {
            ClosableTab t = new ClosableTab();
            t.Title = "Lobby";
            t.Tag = new UserInfo() { Name = "Lobby", Id = Guid.NewGuid()};
            t.CloseIsEnabled = false;
            ListBox tmp = new ListBox();
            tmp.ItemTemplate = (DataTemplate)Application.Current.FindResource("ListBoxItemTemplate");
            tmp.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            t.Content = tmp;
            Tabs.Add(t);
            SelectedTab = t;
            tmp.ItemsSource = Model.history;
        }

        private void AddNewTab(UserInfo user,bool isSelected)
        {
            ClosableTab t = new ClosableTab();
            t.Title = user.Name;
            t.Tag = user;
            ListBox tmp = new ListBox();
            tmp.ItemTemplate = (DataTemplate)Application.Current.FindResource("ListBoxItemTemplate");
            tmp.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            t.Content = tmp;
            Tabs.Add(t);
            tmp.ItemsSource = Model.users.FirstOrDefault(u=>u.user.Id == user.Id).history;
            if (isSelected)
            {
                SelectedTab = t;
            }
        }

        public void Show()
        {
            Application.Current.MainWindow = _view as Window;
            _view.Show();
        }
        public void Close()
        {
            Model.Leave();
            Application.Current.MainWindow.Close();
        }
    }
}
