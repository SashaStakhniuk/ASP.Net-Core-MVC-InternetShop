using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Base64File
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] Photo_Base64 { get; set; }
    }
}
