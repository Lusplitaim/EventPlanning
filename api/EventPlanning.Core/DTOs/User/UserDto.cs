using EventPlanning.Core.DTOs.Role;

namespace EventPlanning.Core.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; } = [];
    }
}
