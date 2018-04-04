﻿
using BeerWiki.Interfaces;

namespace BeerWiki.Models
{
    public class Available : IAvailable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}