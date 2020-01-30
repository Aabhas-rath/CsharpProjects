using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;

namespace Services.ServiceComponents.ServiceExceptions
{
    public class ImageNotFoundException : FileNotFoundException
    {
        public ImageNotFoundException()
        {
        }

        public ImageNotFoundException(string message) : base(message)
        {
        }

        public ImageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ImageNotFoundException(string message, string fileName) : base(message, fileName)
        {
        }

        public ImageNotFoundException(string message, string fileName, Exception innerException) : base(message, fileName, innerException)
        {
        }

        protected ImageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
