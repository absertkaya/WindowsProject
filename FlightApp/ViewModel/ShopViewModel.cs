using FlightApp.Data;
using FlightApp.Model;
using FlightApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class ShopViewModel : ViewModelBase
    {
        #region Properties
        private Passenger _passenger;
        private IList<Product> _foodProducts;
        private IList<Product> _drinkProducts;
        private IList<Product> _snackProducts;
        private Order _shoppingCart;
        private Boolean _hasOrders = false;
        public IList<Product> FoodProducts
        {
            get { return _foodProducts; }
            set { _foodProducts = value; RaisePropertyChanged(); }
        }
        public IList<Product> DrinkProducts
        {
            get { return _drinkProducts; }
            set { _drinkProducts = value; RaisePropertyChanged(); }
        }
        public IList<Product> SnackProducts
        {
            get { return _snackProducts; }
            set { _snackProducts = value; RaisePropertyChanged(); }
        }
        public Order ShoppingCart
        {
            get { return _shoppingCart; }
            set { _shoppingCart = value; RaisePropertyChanged(); }
        }
        public Boolean HasOrders {
            get { return _hasOrders; }
            set { _hasOrders = value; RaisePropertyChanged(); }
        }
        #endregion

        public ShopViewModel()
        {
            LoadProducts();

            _passenger = UserService.GetInstance().User as Passenger;
            if (_passenger.ShoppingCart is null)
                _passenger.ShoppingCart = new Order(_passenger);

            ShoppingCart = _passenger.ShoppingCart;

            if (_passenger.Orders != null)
                HasOrders = _passenger.Orders.Count != 0;
        }

        private async void LoadProducts()
        {
            try
            {
                IList<Product> products = await ShopRepository.GetAllProductsAsync();
                FoodProducts = products.Where(p => p.Type.Equals(ProductType.FOOD)).ToList();
                DrinkProducts = products.Where(p => p.Type.Equals(ProductType.DRINKS)).ToList();
                SnackProducts = products.Where(p => p.Type.Equals(ProductType.SNACKS)).ToList();
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Try again", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }

        public void AddToCart(Product product)
        {
            ShoppingCart.AddProduct(product);
            RefreshCart();
        }

        public void RemoveFromCart(Product product)
        {
            ShoppingCart.RemoveProduct(product);
            RefreshCart();
        }

        public void DecrementFromCart(Product product)
        {
            ShoppingCart.DecrementProduct(product);
            RefreshCart();
        }

        public async void PlaceOrder()
        {
            try
            {
                Order placedOrder = await ShopRepository.PostOrder(ShoppingCart);
                if (placedOrder is null) throw new Exception("Order can't be empty");

                ShoppingCart = new Order(_passenger);
                _passenger.Orders.Add(placedOrder);

                _hasOrders = true;
                MessageDialog messageDialog = new MessageDialog($"Thank you for your order!\nYour order is being processed by our staff members");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.CancelCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Error trying to place order. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.CancelCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            switch (command.Label)
            {
                case "Try again":
                    LoadProducts();
                    break;
            }
        }

        private void RefreshCart()
        {
            ShoppingCart = null;
            ShoppingCart = (UserService.GetInstance().User as Passenger).ShoppingCart;
        }
    }
}
