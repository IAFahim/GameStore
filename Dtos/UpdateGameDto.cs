using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);