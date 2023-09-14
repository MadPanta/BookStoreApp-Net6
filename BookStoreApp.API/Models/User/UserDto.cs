using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.User
{
    public class UserDto: LoginUserDto
    {
       
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
