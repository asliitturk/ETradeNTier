using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Data.Models.ViewModels
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor")]
        Waiting,
        [Display(Name = "Sipariş Hazırlanıyor")]
        Preparing,
        [Display(Name = "Kargoya verildi")]
        Shipped,
        [Display(Name = "Teslim Edildi")]
        Completed,
    }
}
