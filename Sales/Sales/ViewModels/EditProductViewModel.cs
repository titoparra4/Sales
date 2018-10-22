using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{
    public class EditProductViewModel
    {
        private Product product;

        public EditProductViewModel(ProductItemViewModel productItemViewModel)
        {
            this.product = productItemViewModel;
        }
    }
}
