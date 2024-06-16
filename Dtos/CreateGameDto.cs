using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required] int GenreId,
    [Range(0,100)] decimal Price,
    DateOnly ReleaseDate
);