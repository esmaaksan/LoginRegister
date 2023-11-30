using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Layer.Entities
{
    public class User:IdentityUser
    {
        public int ConfirmationCode { get; set; }
        [JsonIgnore]
        public ICollection<Adresses> adresses { get; set; }
    }
}
