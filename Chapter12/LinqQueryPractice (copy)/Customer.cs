using System;
using System.ComponentModel.DataAnnotations;

namespace Pembroke.Shared
{
    public class Customer
    {
        [Required]
        [StringLength(5)]
        string CustomerId { get; set; }

        [Required]
        [StringLength(40)]
        string CompanyName { get; set; }

        [StringLength(30)]
        string ContactName { get; set; }

        [StringLength(30)]
        string ContactTitle { get; set; }

        [StringLength(60)]
        string Address { get; set; }

        [StringLength(15)]
        string City { get; set; }

        [StringLength(15)]
        string Region { get; set; }

        [StringLength(10)]
        string PostalCode { get; set; }

        [StringLength(15)]
        string Country { get; set; }

        [StringLength(24)]
        string Phone { get; set; }

        [StringLength(24)]
        string Fax { get; set; }
    }
}