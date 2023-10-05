namespace Silos.Users.API.Controllers.Requests;

public record class UpdateUserRequest
{
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
    public string Name { get; init; }    
}