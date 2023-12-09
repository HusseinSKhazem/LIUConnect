using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class ProfilePictureVM
    {
        public IFormFile ProfilePicture { get; set; }
    }
}
