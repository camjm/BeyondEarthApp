﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models.Precis
{
    public class UnitPrecis
    {
        public long UnitId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public short Strength { get; set; }

        public string TechnologyName { get; set; }

        #region Links

        private List<Link> _links;

        [Editable(false)]
        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #endregion
    }
}
