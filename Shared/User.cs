using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Mysql_blazor2.Shared
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Mínimo de 6 caracteres")]
        public string Password { get; set; }

    }
}