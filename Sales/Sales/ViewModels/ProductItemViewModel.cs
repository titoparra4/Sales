using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Sales.Services;
using Xamarin.Forms;
using Sales.Helpers;

namespace Sales.ViewModels
{
    public class ProductItemViewModel : Product
    {
        #region Attributes

        private ApiService apiService;

        #endregion

        #region Constructors

        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }

        #endregion

        #region Commands
        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Confirm, 
                Languages.DeleteConfirmation, 
                Languages.Yes, 
                Languages.No);

            if (!answer)
            {
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


        }
        #endregion
    }
}
