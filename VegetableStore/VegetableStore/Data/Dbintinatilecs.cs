﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VegetableStore.Models;
using VegetableStore.Models.Enums;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.Data
{
    public class Dbintinatilecs
    {
        private readonly ApplicationDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public Dbintinatilecs(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                var c = await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "Admin@123");
                var user = await _userManager.FindByNameAsync("admin");
                var rs = await _userManager.AddToRoleAsync(user, "Admin");
                await _userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));
                await _userManager.AddClaimAsync(user, new Claim("Email", user.Email));
                if (rs.Succeeded)
                {

                }
            }
            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM", Name = "System",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Role",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = "fa-home"  },
                    new Function() {Id = "FUNCTION", Name = "Function",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/admin/function/index",IconCss = "fa-home"  },
                    new Function() {Id = "USER", Name = "User",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = "fa-home"  },
                    new Function() {Id = "ACTIVITY", Name = "Activity",ParentId = "SYSTEM",SortOrder = 4,Status = Status.Active,URL = "/admin/activity/index",IconCss = "fa-home"  },
                    new Function() {Id = "ERROR", Name = "Error",ParentId = "SYSTEM",SortOrder = 5,Status = Status.Active,URL = "/admin/error/index",IconCss = "fa-home"  },
                    new Function() {Id = "SETTING", Name = "Configuration",ParentId = "SYSTEM",SortOrder = 6,Status = Status.Active,URL = "/admin/setting/index",IconCss = "fa-home"  },

                    new Function() {Id = "PRODUCT",Name = "Product Management",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Category",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/admin/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Product",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/admin/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "BILL",Name = "Bill",ParentId = "PRODUCT",SortOrder = 3,Status = Status.Active,URL = "/admin/bill/index",IconCss = "fa-chevron-down"  },

                    new Function() {Id = "CONTENT",Name = "Content",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "fa-table"  },
                    new Function() {Id = "BLOG",Name = "Blog",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/admin/blog/index",IconCss = "fa-table"  },
                    new Function() {Id = "PAGE",Name = "Page",ParentId = "CONTENT",SortOrder = 2,Status = Status.Active,URL = "/admin/page/index",IconCss = "fa-table"  },

                    new Function() {Id = "UTILITY",Name = "Utilities",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "fa-clone"  },
                    new Function() {Id = "FOOTER",Name = "Footer",ParentId = "UTILITY",SortOrder = 1,Status = Status.Active,URL = "/admin/footer/index",IconCss = "fa-clone"  },
                    new Function() {Id = "FEEDBACK",Name = "Feedback",ParentId = "UTILITY",SortOrder = 2,Status = Status.Active,URL = "/admin/feedback/index",IconCss = "fa-clone"  },
                    new Function() {Id = "ANNOUNCEMENT",Name = "Announcement",ParentId = "UTILITY",SortOrder = 3,Status = Status.Active,URL = "/admin/announcement/index",IconCss = "fa-clone"  },
                    new Function() {Id = "CONTACT",Name = "Contact",ParentId = "UTILITY",SortOrder = 4,Status = Status.Active,URL = "/admin/contact/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY",SortOrder = 5,Status = Status.Active,URL = "/admin/slide/index",IconCss = "fa-clone"  },
                    new Function() {Id = "ADVERTISMENT",Name = "Advertisment",ParentId = "UTILITY",SortOrder = 6,Status = Status.Active,URL = "/admin/advertistment/index",IconCss = "fa-clone"  },

                    new Function() {Id = "REPORT",Name = "Report",ParentId = null,SortOrder = 5,Status = Status.Active,URL = "/",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "REVENUES",Name = "Revenue report",ParentId = "REPORT",SortOrder = 1,Status = Status.Active,URL = "/admin/report/revenues",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "ACCESS",Name = "Visitor Report",ParentId = "REPORT",SortOrder = 2,Status = Status.Active,URL = "/admin/report/visitor",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "READER",Name = "Reader Report",ParentId = "REPORT",SortOrder = 3,Status = Status.Active,URL = "/admin/report/reader",IconCss = "fa-bar-chart-o"  },
                });
            }
            if (_context.Products.Count() == 0)
            {
                //_context.Products.AddRange(new List<Product>()
                //{
                //    new Product(){Name="Cà chua",DateCreated=DateTime.Now,Image="/uploaded/images/6.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Cam",DateCreated=DateTime.Now,Image="/uploaded/images/3.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Nho",DateCreated=DateTime.Now,Image="/uploaded/images/4.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Bơ",DateCreated=DateTime.Now,Image="/uploaded/images/7.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Chanh",DateCreated=DateTime.Now,Image="/uploaded/images/Lemon.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Sầu Riêng",DateCreated=DateTime.Now,Image="/uploaded/images/Durian.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Dưa Hấu",DateCreated=DateTime.Now,Image="/uploaded/images/watermelon.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Dâu",DateCreated=DateTime.Now,Image="/uploaded/images/strawberry.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Việt Quất",DateCreated=DateTime.Now,Image="/uploaded/images/blueberry.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Quýt",DateCreated=DateTime.Now,Image="/uploaded/images/tangerines.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Xoài",DateCreated=DateTime.Now,Image="/uploaded/images/mango.jpg",Price = 1000,Status = Status.Active},
                //    new Product(){Name="Trái Thăng Long",DateCreated=DateTime.Now,Image="/uploaded/images/dragon.jpg",Price = 1000,Status = Status.Active}
                //});
            }
            if (_context.ProductCategories.Count() == 0)
            {


                List<ProductCategory> listProductCategory = new List<ProductCategory>() { };
                //{
                //    new ProductCategory() { Name="Trái cây theo miền",ParentId = null,Status=Status.Active,SortOrder=0 },
                //    new ProductCategory() { Name="Miền Bắc",ParentId = 2,Status=Status.Active,SortOrder=1,
                //        Products = new List<Product>()
                //        {
                //            new Product(){Name = "Bưởi Đoan Hùng",DateCreated=DateTime.Now,Image="/uploaded/images/buoi-doan-hung (1).jpg",Price = 1000,Status = Status.Active,Month=1},
                //            new Product(){Name = "Lê Đông Khê",DateCreated=DateTime.Now,Image="/uploaded/images/image.jpeg",Price = 11000,Status = Status.Active,Month=2},
                //            new Product(){Name = "Na Đồng Bành",DateCreated=DateTime.Now,Image="/uploaded/images/na.jpg",Price = 12000,Status = Status.Active,Month=3},
                //            new Product(){Name = "Vải Thiều",DateCreated=DateTime.Now,Image="/uploaded/images/vai.jpg",Price = 1000,Status = Status.Active,Month=4}
                //        }
                //    },
                //    new ProductCategory() { Name="Miền Tây",ParentId = 2,Status=Status.Active ,SortOrder=2,
                //        Products = new List<Product>()
                //        {
                //            new Product(){Name = "Quả thanh trà",DateCreated=DateTime.Now,Image="/uploaded/images/thanhtra.jpg",Price = 12000,Status = Status.Active,Month=5},
                //            new Product(){Name = "Sầu riêng Ri6",DateCreated=DateTime.Now,Image="/uploaded/images/saurieng.jpg",Price = 1000,Status = Status.Active,Month=6},
                //            new Product(){Name = "Vú sữa Lò Rèn",DateCreated=DateTime.Now,Image="/uploaded/images/vu-sua.jpg",Price = 1000,Status = Status.Active,Month=7},
                //            new Product(){Name = "Nhãn tím Sóc Trăng",DateCreated=DateTime.Now,Image="/uploaded/images/nhan (2).jpg",Price = 1000,Status = Status.Active,Month=8}
                //        }},
                //    new ProductCategory() { Name="Miền Đông Nam Bộ",ParentId = 2,Status=Status.Active ,SortOrder=3,
                //        Products = new List<Product>()
                //        {
                //            new Product(){Name = "Thanh Long Bình Thuận",DateCreated=DateTime.Now,Image="/uploaded/images/dragon.jpg",Price = 1000,Status = Status.Active,Month=9},
                //            new Product(){Name = "Sầu Riêng Khánh Sơn",DateCreated=DateTime.Now,Image="/uploaded/images/Durian.jpg",Price = 1000,Status = Status.Active,Month=10},
                //            new Product(){Name = "Xoài tượng Bình Định",DateCreated=DateTime.Now,Image="/uploaded/images/mango.jpg",Price = 1000,Status = Status.Active,Month=11},
                //            new Product(){Name = "Dưa hấu Bình Sơn",DateCreated=DateTime.Now,Image="/uploaded/images/watermelon.jpg",Price = 1000,Status = Status.Active,Month=12}
                //        }},
                //    new ProductCategory() { Name="Trái cây theo mùa",ParentId = null,Status=Status.Active,SortOrder=0 },
                //    new ProductCategory() { Name="Trái cây xắt sẵn",ParentId = null,Status=Status.Active,SortOrder=0 }
                //    ,
                //    new ProductCategory() { Name="Mua gộp",ParentId = null,Status=Status.Active,SortOrder=0 }
                
                _context.ProductCategories.AddRange(listProductCategory);
            }
            await _context.SaveChangesAsync();
        }
    }
}
