using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using InventoryApp.Models;

namespace InventoryApp.DBContext
{
    public class Context : DbContext
    {

        public DbSet<Item> Items { get; set; }
    }
}