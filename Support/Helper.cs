using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Support
{

   
    public class Helper
    {
        [Obsolete]
        private readonly IHostingEnvironment Environment;

        [Obsolete]
        public Helper(IHostingEnvironment hostingEnvironment ) {
            Environment = hostingEnvironment;
        }

        [Obsolete]
        public void createDirectory(string folderName, string fileName) {
            string _fileName = Path.GetFileName(fileName);
            string path = Environment.WebRootPath + "/"+folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }

}
