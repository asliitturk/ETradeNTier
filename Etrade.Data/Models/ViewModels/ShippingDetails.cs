using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Data.Models.ViewModels
{
    public class ShippingDetails
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen adres tanımı giriniz.")]
        public string AddressTitle { get; set; }
        [Required(ErrorMessage = "Lütfen bir adres giriniz.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Lütfen bir sehir giriniz.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Lütfen ilçe giriniz.")]
        public string District { get; set; }
        [Required(ErrorMessage = "Lütfen mahalle giriniz.")]
        public string Neighbourhood { get; set; }
        public string PostalCode { get; set; }
    }
}
