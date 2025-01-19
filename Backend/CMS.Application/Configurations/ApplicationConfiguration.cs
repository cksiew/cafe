using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Configurations
{
    public class ApplicationConfiguration(IConfiguration configuration, IHostEnvironment hostEnvironment)
    {    
        public string HttpHost => configuration["Domain:HttpHost"]??"http://localhost:5001";

        public string HttpsHost => configuration["Domain:HttpsHost"] ?? "https://localhost:6001";

        public string Database => configuration.GetConnectionString("Database") ?? "";

        public string UploadedDirectory => configuration["StaticFiles:UploadedDirectory"] ?? "UploadedFiles";

        public string UploadedImageDirectory => Path.Combine(UploadedDirectory, "images");

        public string FileProviderRequestPath => "files";

        public string UploadedImageHostPath =>  $"{(hostEnvironment.IsDevelopment()?HttpHost:HttpsHost)}/files/images";

          
        
    }
}
