using System.Collections.Generic;

namespace VegetableStore.Models.ViewModels
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}