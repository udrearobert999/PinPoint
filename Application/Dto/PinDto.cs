using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class PinDto
    {
        [Required] public string Title { get; set; } = null!;

        [Required] public byte[] Picture { get; set; } = null!;
    }
}   