// Data/OrderCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Data
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Укажите ID пользователя!")]
        [StringLength(450, ErrorMessage = "ID пользователя не должен превышать 450 символов")]
        public string UserId { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Укажите дату заказа!")]
        [DataType(DataType.Date, ErrorMessage = "Введите корректную дату")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Укажите статус заказа!")]
        [StringLength(50, ErrorMessage = "Статус не должен превышать 50 символов")]
        public string Status { get; set; } = "В обработке";
        
        [Required(ErrorMessage = "Укажите общую сумму!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0")]
        public decimal TotalAmount { get; set; }
        
        [StringLength(500, ErrorMessage = "Адрес доставки не должен превышать 500 символов")]
        public string? ShippingAddress { get; set; }
        
        [StringLength(20, ErrorMessage = "Номер телефона не должен превышать 20 символов")]
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string? PhoneNumber { get; set; }
    }
}