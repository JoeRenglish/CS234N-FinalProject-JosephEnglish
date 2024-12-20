﻿using System;
using System.Collections.Generic;

namespace BreweryTrackingSystem.models
{
    public partial class AddressType
    {
        public AddressType()
        {
            SupplierAddresses = new HashSet<SupplierAddress>();
        }

        public int AddressTypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<SupplierAddress> SupplierAddresses { get; set; }
    }
}
