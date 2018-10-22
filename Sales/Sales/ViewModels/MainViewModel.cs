using GalaSoft.MvvmLight.Command;
using Sales.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class MainViewModel
    {

        #region Properties
        public EditProductViewModel EditProduct { get; set; }
        public ProductsViewModel Products { get; set; }

        public AddProductViewModel AddProduct { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }
        #endregion

        #region Commands
        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }
        }

        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        } 
        #endregion
    }
}
