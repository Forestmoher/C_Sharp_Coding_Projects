using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter item name.")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Item entered must be in stock")]
        [Display(Name = "In Stock")]
        public bool IsInStock { get; set; }

        [Required(ErrorMessage = "Please enter amount in stock.")]
        [Display(Name = "Stocked Amount")]
        public int Amount { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
    }
}