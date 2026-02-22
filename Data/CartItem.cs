// Data/CartItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsStore.Data
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        
        [Required(ErrorMessage = "Укажите ID пользователя!")]
        public string UserId { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Укажите ID запчасти!")]
        public int PartId { get; set; }
        
        [Required(ErrorMessage = "Укажите количество!")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть не менее 1")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Укажите цену!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public DateTime AddedDate { get; set; } = DateTime.Now;
        
        // Навигационные свойства
        public virtual ApplicationUser? User { get; set; }
        public virtual Part? Part { get; set; }
    }
}