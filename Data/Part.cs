// Data/Part.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsStore.Data
{
    public class Part
    {
        public int PartId { get; set; }
        
        [Required(ErrorMessage = "Укажите название запчасти!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 200 символов")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Укажите артикул!")]
        [StringLength(50, ErrorMessage = "Артикул не должен превышать 50 символов")]
        public string? Article { get; set; }
        
        [Required(ErrorMessage = "Укажите производителя!")]
        public int ManufacturerId { get; set; }
        
        [Required(ErrorMessage = "Укажите категорию!")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Укажите цену!")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Укажите количество на складе!")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int StockQuantity { get; set; }
        public string? ImagePath { get; set; }
        
        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string? Description { get; set; }
        
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
    }
}