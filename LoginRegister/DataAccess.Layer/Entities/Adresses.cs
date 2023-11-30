using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Entities
{
    public class Adresses
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserAddress { get; set; } 
        public string City { get; set; }
        public string Town { get; set; }
        public string Neighbourhood { get; set; }
        public string Street { get; set; }
        public int No { get; set; }
        public bool IsActivite { get; set; }
       // public User user { get; set; }

        
    }
}
