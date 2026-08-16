using System.ComponentModel.DataAnnotations;

namespace IdentityService.Pages.Account.Register;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    public string Username { get; set; }
    public string FullName { get; set; }
    public string ReturnUrl { get; set; }
    public string Button { get; set; }
}
