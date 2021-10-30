using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HDnet
{
    public class Users
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public string Id{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PW")]
        public string password { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class baseuserdata : List<Users>
    {
        public baseuserdata()
        {
            Add(new Users() {
                Id = "admin",
                password = "1234"
            });
        }
    }
}