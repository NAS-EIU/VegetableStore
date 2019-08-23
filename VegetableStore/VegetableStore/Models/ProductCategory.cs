using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Models.Enums;

namespace VegetableStore.Models
{
    [Table("ProductCategories")]
    public class ProductCategory : DomainEntity<int>
    {
  

        public ProductCategory(string name, string description, int? parentId, int? homeOrder, string image, bool? homeFlag, int sortOrder, Status status)
        {
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            Image = image;
            HomeFlag = homeFlag;
            SortOrder = sortOrder;
            Status = status;
        }

        public ProductCategory(string name, string description, int? parentId, int? homeOrder, string image, bool? homeFlag, DateTime dateCreated, DateTime dateModified, int sortOrder, Status status)
        {
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            Image = image;
            HomeFlag = homeFlag;
            DateCreated = dateCreated;
            DateModified = dateModified;
            SortOrder = sortOrder;
            Status = status;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
