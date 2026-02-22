using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "Укажите фамилию!")]
    [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "Укажите имя!")]
    [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
    public string? FirstName { get; set; }
    
    [StringLength(50, ErrorMessage = "Отчество не должно превышать 50 символов")]
    public string? MiddleName { get; set; }
    
    [Required(ErrorMessage = "Укажите адрес доставки!")]
    [StringLength(500, ErrorMessage = "Адрес не должен превышать 500 символов")]
    public string? Address { get; set; }
    
    [Display(Name = "Дата регистрации")]
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}

