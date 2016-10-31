using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Example.ViewModels
{
    public class MovieViewModel
    {
        //public int MovieId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Length { get; set; }
    }
}
