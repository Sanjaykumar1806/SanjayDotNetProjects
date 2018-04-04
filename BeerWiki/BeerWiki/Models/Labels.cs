using System;
using BeerWiki.Interfaces;

namespace BeerWiki.Models
{
    public class Labels : ILabels
    {
        public string Icon { get; set; }

        public string Medium { get; set; }

        public string Large { get; set; }
    }
}