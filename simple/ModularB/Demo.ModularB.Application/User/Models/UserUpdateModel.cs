using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.ModularB.Application.User.Models
{
    public class UserUpdateModel:UserAddModel
    {
        [Required(ErrorMessage = "请选择账户")]
        public Guid Id { get; set; }
    }
}
