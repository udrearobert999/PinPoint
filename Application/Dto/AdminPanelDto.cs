using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Dto
{
    public class AdminPanelDto
    {
        public string SelectedUser { get; set; } = null!;
        public string SelectedRole { get; set; } = null!;
        public List<string?> Users { get; set; } = new();
        public List<string?> Roles { get; set; } = new();
    }
}
