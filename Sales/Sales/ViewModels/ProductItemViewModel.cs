﻿using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Sales.Services;
using Xamarin.Forms;
using Sales.Helpers;
using System.Linq;
using Sales.Views;

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

        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        private async void EditProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditProductPage());
        }

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

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.ProductId, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var productsViewModel = ProductsViewModel.GetInstance();
            var deleteProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.ProductId).FirstOrDefault();
            if(deleteProduct != null)
            {
                productsViewModel.MyProducts.Remove(deleteProduct);
            }

            productsViewModel.RefreshList();

        }
        #endregion
    }
}
