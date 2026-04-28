using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Dtos.Blob
{
    public class BlobObject
    {
        public Stream? Content  { get; set; }
        public string? ContentType { get; set; } = null;
    }
}
