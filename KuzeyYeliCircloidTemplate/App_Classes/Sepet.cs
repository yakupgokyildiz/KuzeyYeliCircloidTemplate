using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuzeyYeliCircloidTemplate.App_Classes
{
    using Models;
    public class Sepet
    {

        private  List<Urunler> urunler = new List<Urunler>();

        public List<Urunler> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }

    }
}