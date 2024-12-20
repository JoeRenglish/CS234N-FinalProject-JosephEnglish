﻿using System;
using System.Collections.Generic;

namespace BreweryTrackingSystem.models
{
    public partial class FermentableType
    {
        public FermentableType()
        {
            Fermentables = new HashSet<Fermentable>();
        }

        public int FermentableTypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Fermentable> Fermentables { get; set; }
    }
}
