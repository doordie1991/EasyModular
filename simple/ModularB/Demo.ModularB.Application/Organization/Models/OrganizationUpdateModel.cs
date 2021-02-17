using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.ModularB.Application.Organization.Models
{
    public class OrganizationUpdateModel : OrganizationAddModel
    {
        [Required(ErrorMessage = "请选择账户")]
        public Guid Id { get; set; }
    }
}
