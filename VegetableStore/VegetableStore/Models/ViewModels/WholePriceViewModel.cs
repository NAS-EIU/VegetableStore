using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegetableStore.Models.ViewModels
{
    public class WholePriceViewModel
    {
        public int ProductId { get; set; }

        public int FromQuantity { get; set; }

        public int ToQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
