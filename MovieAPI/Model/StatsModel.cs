using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Model
{
    public class StatsModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }

    public class WatchStatsModel
    {
        [Name("movieId")]
        public int MovieId { get; set; }
        [Name("watchDurationMs")]
        public int WatchDurationMs { get; set; }
    }
}
