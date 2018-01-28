using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;


namespace WebApplication43.Models
{
    public class KitapPage
    {
        public string KitapAdi { get; set; }
        public int Kitapid { get; set; }
        public string YazarAdi { get; set; }
        public int? page { get; set; }
        public IPagedList<KitapPage> kit { get; set; }
        public string KitapUrl { get; set; }
    }
}