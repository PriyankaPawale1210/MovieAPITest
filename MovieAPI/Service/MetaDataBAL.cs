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
    public class MetaDataBAL : IMetaData
    {
        private const string MetadataPath = "DataBase/metadata.csv";
        private const string StatsPath = "DataBase/stats.csv";

        public MetadataModel[] Get(int movieId)
        {
            var csvData = MetaDataBAL.GetCSVData();
            var movies = csvData.Where(m => m.MovieId == movieId)
                .GroupBy(m => m.Language, m => m)
                .Select(g => g.OrderByDescending(m => m.Id).FirstOrDefault())
                .OrderBy(m => m.Language)
                .ToArray();
                

            if (!movies.Any()) throw new Exception("No movies found by that Id");
            return movies;
        }

        public bool Post(MetadataModel metadata)
        {
            return MetaDataBAL.Insert(metadata);
        }

        public static MetadataModel[] GetCSVData()
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

        public static bool Insert(MetadataModel metadata)
        {
            List<MetadataModel> metadataModels = new List<MetadataModel>();
            metadataModels.Add(metadata);
            return true;
        }
    }
}
