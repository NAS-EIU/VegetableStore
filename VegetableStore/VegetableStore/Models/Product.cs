using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Models.Enums;

namespace VegetableStore.Models
{
    [Table("Products")]
    public class Product:DomainEntity<int>
    {
        public Product(int id ,string name,string image,decimal price,string description,string content,string tags,Status status)
        {
            Status = status;
            Id = id;
            Name = name;
            Image = image;
            Price = price;
            Description = description;
            Content = content;
            Tags = tags;
            ProductTags = new List<ProductTag>();
        }
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [DefaultValue(0)]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string Content { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        public virtual ICollection<ProductTag> ProductTags { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
    }


}
