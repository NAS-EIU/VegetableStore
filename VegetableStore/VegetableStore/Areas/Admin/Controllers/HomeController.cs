﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VegetableStore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        

        public IActionResult Index()
        {
            

            return View();
        }

      
    }
}