using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("user_cpf")]
        public string CPF { get; set; }

        [Column("user_tipo")]
        public TipoUsuario? Tipo { get; set; }
    }
}
