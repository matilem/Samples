using System;

namespace Aafp.Also.Api.Dtos
{
    public class ProductDto
    {
        public Guid ProductKey { get; set; }

        public string ProductName { get; set; }

        public Guid ProductTypeKey { get; set; }

        public Guid ProductCompanyKey { get; set; }
    }
}