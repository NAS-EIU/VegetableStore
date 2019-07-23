using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Models;

namespace VegetableStore.Interfaces
{
    interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
