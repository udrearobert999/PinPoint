using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class PinDto
    {
        public Guid Id { get; set; }
        [Required] public string Title { get; set; } = null!;

        [Required] public IFormFile PictureUpload { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;
    }
}   