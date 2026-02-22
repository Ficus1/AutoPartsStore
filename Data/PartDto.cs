// Data/ParteDto.cs
using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Data
{
    public class PartDto
    {
        public int PartId { get; set; }

        [Required(ErrorMessage = "Укажите название запчасти!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 200 символов")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Укажите артикул!")]
        [StringLength(50, ErrorMessage = "Артикул не должен превышать 50 символов")]
        public string Article { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Укажите производителя!")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите производителя")]
        public int ManufacturerId { get; set; }
        
        [Required(ErrorMessage = "Укажите категорию!")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите категорию")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Укажите цену!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Укажите количество на складе!")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int StockQuantity { get; set; }
        
        public string? ImagePath { get; set; }
        
        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string? Description { get; set; }
    }
}