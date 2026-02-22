// Data/Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsStore.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "Укажите ID пользователя!")]
        public string? UserId { get; set; }
        
        [Required(ErrorMessage = "Укажите дату заказа!")]
        [DataType(DataType.Date, ErrorMessage = "Введите корректную дату")]
        public DateTime OrderDate { get; set; }
        
        [Required(ErrorMessage = "Укажите статус заказа!")]
        [StringLength(50, ErrorMessage = "Статус не должен превышать 50 символов")]
        public string? Status { get; set; } // "В обработке", "Оплачен", "Доставлен", "Отменен"
        
        [Required(ErrorMessage = "Укажите общую сумму!")]
        [Range(0, double.MaxValue, ErrorMessage = "Сумма не может быть отрицательной")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [StringLength(500, ErrorMessage = "Адрес доставки не должен превышать 500 символов")]
        public string? ShippingAddress { get; set; }
        
        [StringLength(20, ErrorMessage = "Номер телефона не должен превышать 20 символов")]
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string? PhoneNumber { get; set; }
        
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}