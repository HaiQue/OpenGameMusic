using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameMusic.Models
{
    public class FileDetails
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public class FileViewModel
    {
        public List<FileDetails> Files { get; set; }
            = new List<FileDetails>();
    }
}
