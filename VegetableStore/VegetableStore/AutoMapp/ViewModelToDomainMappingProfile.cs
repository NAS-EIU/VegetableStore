using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Models;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.AutoMapp

{
    public class ViewModelToDomainMappingProfile :Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
           .ConstructUsing(c => new Product(c.Name, c.Image, c.Price
           , c.Description, c.Content, c.Tags, c.Status));
            CreateMap<BillViewModel, Bill>()
              .ConstructUsing(c => new Bill(c.Id, c.CustomerName, c.CustomerAddress,
              c.CustomerMobile, c.CustomerMessage, c.BillStatus,
              c.PaymentMethod, c.Status, c.CustomerId));

            CreateMap<BillDetailViewModel, BillDetail>()
              .ConstructUsing(c => new BillDetail(c.Id, c.BillId, c.ProductId,
              c.Price));
        }
        
    }
}
