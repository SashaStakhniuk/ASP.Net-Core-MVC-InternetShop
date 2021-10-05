using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Base64FilesViewModel
    {
        public string Name { get; set; }
        public IFormFile Photo_Base64 { get; set; }
        public List<Base64File> PhotosInBytes { get; set; }

    }
}
