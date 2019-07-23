using System.ComponentModel.DataAnnotations.Schema;

namespace VegetableStore.Models
{
    public class BillDetail
    {
        public int BillId { set; get; }

        public int ProductId { set; get; }
   
        public decimal Price { set; get; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        
    }
}
}