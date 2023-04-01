using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saturn.BusinessUser.Dtos
{
    public class UpdateProfessionalPhotoDto
    {
        public IFormFile ProfessionalPhoto { get; set; }
    }
}
