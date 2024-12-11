﻿using System;
using System.Collections.Generic;

namespace BreweryTrackingSystem.models
{
    public partial class InventoryTransactionType
    {
        public InventoryTransactionType()
        {
            InventoryTransactions = new HashSet<InventoryTransaction>();
        }

        public int InventoryTransactionTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
