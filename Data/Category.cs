// Data/Category.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsStore.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Укажите название категории!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 100 символов")]
        public string? Name { get; set; }
        
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }
        
        public virtual ICollection<Part>? Parts { get; set; }
    }
}