using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Sevices.Dtos.UserDto
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\\d\\W])|(?=.*\\W)(?=.*\\d))|(?=.*\\W)(?=.*[A-Z])(?=.*\\d)).{8,}$",
            ErrorMessage = "Passowrd Must Have 1 Upper,1 Lower , At least one Numbers , Disallow the consecutive digits like 1234, 4567, etc , Disallow the consecutive alphabets like abcd, ijkl, etc")]
        public string Password { get; set; }
    }
}
