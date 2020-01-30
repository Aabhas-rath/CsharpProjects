using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace Services.ServiceComponents.ServiceExceptions
{
    public class AlbumNotFoundException : DirectoryNotFoundException
    {
        public AlbumNotFoundException()
        {
        }

        public AlbumNotFoundException(string message) : base(message)
        {
        }

        public AlbumNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlbumNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
