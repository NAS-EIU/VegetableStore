using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VegetableStore.Models.ViewModels
{
    public class SizeViewModel
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Name
        {
            get; set;
        }
    }
}
