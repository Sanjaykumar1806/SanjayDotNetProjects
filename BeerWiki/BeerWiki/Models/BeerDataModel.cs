using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using BeerWiki.Interfaces;
using BeerWiki.Helpers;

namespace BeerWiki.Models
{
    public class BeerDataModel : IBeer
    {
        public BeerDataModel(IGlass glass, ISrm srm, IStyle style, ILabels labels)
        {
            Glass = glass;
            Srm = srm;
            Style = style;
            Labels = labels;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NameDisplay { get; set; }
        public string Description { get; set; }
        public double Abv { get; set; }
        public double IBU { get; set; }
        public int GlasswareId { get; set; }
        public int SrmId { get; set; }
        public int AvailableId { get; set; }
        public int StyleId { get; set; }
        public string IsOrganic { get; set; }
        public string Status { get; set; }
        public string StatusDisplay { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Glass>))]
        public IGlass Glass { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Srm>))]
        public ISrm Srm { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Style>))]
        public IStyle Style { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Labels>))]
        public ILabels Labels { get; set; }
        public string ServingTemperature { get; set; }
    }
}