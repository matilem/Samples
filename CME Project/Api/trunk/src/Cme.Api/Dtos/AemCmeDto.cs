using System.Collections.Generic;

namespace Aafp.Cme.Api.Dtos
{
    public class AemCmeDto
    {
        public bool HideFeatured { get; set; }

        public bool HideEventDetails { get; set; }

        public bool HideCme { get; set; }

        public bool HideDisplayTags { get; set; }

        public List<AemCmeItemDto> Results { get; set; }
    }
}