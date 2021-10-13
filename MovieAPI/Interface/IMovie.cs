using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Interface
{
    public interface IMovie
    {
        StatsModel[] Get();
    }
}
