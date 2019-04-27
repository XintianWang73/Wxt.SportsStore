using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wxt.SportsStore.Domain.Entities
{
    public class LoginUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please input pssword")]
        public string Password { get; set; }
    }
}
