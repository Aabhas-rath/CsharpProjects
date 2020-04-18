using Logics.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Logics
{
    public interface IExtractor
    {
        Dictionary<string, List<string>> Extract();
    }
}