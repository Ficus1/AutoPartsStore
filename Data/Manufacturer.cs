// Data/Manufacturer.cs
using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Data
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        
        [Required(ErrorMessage = "Укажите название производителя!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина названия должна быть от 2 до 100 символов")]
        public string? Name { get; set; }
        
        [StringLength(50, ErrorMessage = "Страна не должна превышать 50 символов")]
        public string? Country { get; set; }
        
        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов")]
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public virtual ICollection<Part>? Parts { get; set; }
    }
}