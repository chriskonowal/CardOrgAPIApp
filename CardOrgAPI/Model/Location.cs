﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Model
{
    public partial class Location
    {
        public Location()
        {
            Cards = new HashSet<Card>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}