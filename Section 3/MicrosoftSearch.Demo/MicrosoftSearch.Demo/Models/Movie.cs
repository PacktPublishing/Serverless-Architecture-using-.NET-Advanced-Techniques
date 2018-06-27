using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace MicrosoftSearch.Demo.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class Movie
    {
        [Key] 
        public string MovieId { get; set; }

        [IsSortable, IsFilterable, IsSearchable]
        public string Name { get; set; }

        [IsSearchable, IsFilterable, IsSortable]
        public string Description { get; set; }

        [IsSortable, IsFilterable] 
        public int? Year { get; set; }
    }
}