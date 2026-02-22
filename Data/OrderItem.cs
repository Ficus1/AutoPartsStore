// Data/OrderItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsStore.Data
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        
        [Required(ErrorMessage = "Укажите ID заказа!")]
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "Укажите ID запчасти!")]
        public int PartId { get; set; }
        
        [Required(ErrorMessage = "Укажите количество!")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть не менее 1")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Укажите цену!")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        public virtual Order? Order { get; set; }
        public virtual Part? Part { get; set; }
    }
}