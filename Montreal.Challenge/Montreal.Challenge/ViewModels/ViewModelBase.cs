using Acr.UserDialogs;
using Montreal.Challenge.Ioc;
using Montreal.Challenge.Shared.NativeInterfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Montreal.Challenge.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        /// <summary>
        /// Verify if has internet connection
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            var connected = Injector.Resolver<IConnectivity>().IsConnected();

            if(!connected)
            {
                //show message
                UserDialogs.Instance.Alert("Ocorreu um erro de conexão com Internet ! Tente novamente mais tarde !", "Internet");
            }

            return connected;
        }
    }
}
