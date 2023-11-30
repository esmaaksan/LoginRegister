using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.DTOS
{
    public class ResetPasswordDTO
    {
        public int ConfirmationCode { get; set; }
        public string NewPassword { get; set; }
        

    }
}
