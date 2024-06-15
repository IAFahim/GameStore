using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    [Required] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);