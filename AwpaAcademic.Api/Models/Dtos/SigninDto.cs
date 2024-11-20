using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Dtos;

public record SigninDto(
    [Required]
    [EmailAddress]
    [StringLength(256)]
    string Email,
    [Required] [StringLength(50)] string Passwd
);
