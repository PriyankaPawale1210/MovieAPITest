using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Interface
{
    public interface IMetaData
    {
        MetadataModel[] Get(int movieId);
        bool Post(MetadataModel metadata);
    }
}
