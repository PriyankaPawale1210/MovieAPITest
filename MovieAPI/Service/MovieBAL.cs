using CsvHelper;
using MovieAPI.Interface;
using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Service
{
    public class MovieBAL:IMovie
    {

        private const string MetadataPath = "DataBase/metadata.csv";
        private const string StatsPath = "DataBase/stats.csv";
       
        public StatsModel[] Get()
        {
           
            var csvData = MovieBAL.GetCSVData();
            var movies = csvData.GroupBy(_ => _.MovieId,_ => _)
                .Select(g => g.OrderByDescending(o => o.Id))
                .Select(g => g.FirstOrDefault(f => f.Language == "EN"))
                .ToArray();

            var watchsData = MovieBAL.GetStatsLookup();
            return GetStats(movies, watchsData)
               .OrderByDescending(x => x.Watches)
               .ThenByDescending(x => x.ReleaseYear)
               .ToArray();

        }

        private static MetadataModel[] GetCSVData()
        {
            using (var reader = new StreamReader(MetadataPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    var records = csv.GetRecords<MetadataModel>();
                    return records.ToArray();
                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        private static WatchStatsModel[] GetStatsLookup()
        {
            using (var reader = new StreamReader(StatsPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    var records = csv.GetRecords<WatchStatsModel>();
                    return records.ToArray();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        private static StatsModel[] GetStats(MetadataModel[] movies, WatchStatsModel[] statsLookup)
        {
            List<StatsModel> sm = new List<StatsModel>();
            foreach (var movie in movies)
            {
               
                var (watches, averageDuration) = GetWatchStats(statsLookup, movie.MovieId);
                sm.Add(new StatsModel() {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    AverageWatchDurationS = averageDuration,
                    Watches = watches,
                    ReleaseYear = movie.ReleaseYear,
                });

              
                
                
            }
            return sm.ToArray();
        }

        private static Tuple<int,int> GetWatchStats(WatchStatsModel[] statsLookup, int movieId)
        {
            var avg = (int)statsLookup.Where(_ => _.MovieId == movieId).Select(_ => _.WatchDurationMs).Average() / 1000;
            var watches = statsLookup.Where(_ => _.MovieId == movieId).Count();


            return Tuple.Create(watches, avg);
        }
    }
}
