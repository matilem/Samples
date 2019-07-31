using System.Collections.Generic;

namespace Aafp.Cme.Api.Dtos.Lms
{
    public class LmsDtoWrapper<T>
    {
        public string Self { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public List<T> List { get; set; }
    }
}