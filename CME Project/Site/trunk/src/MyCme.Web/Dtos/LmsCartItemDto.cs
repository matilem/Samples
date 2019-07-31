using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.MyCme.Web.Dtos
{
    public class LmsCartItemDto
    {
        public string Cart_Item_Id { get; set; }

        public int Cart_Id { get; set; }

        public int Nid { get; set; }

        public int Qty { get; set; }

        public string Changed { get; set; }
    }
}