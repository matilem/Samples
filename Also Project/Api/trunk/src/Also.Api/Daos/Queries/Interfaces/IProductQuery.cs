using Aafp.Also.Api.Dtos;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IProductQuery
    {
        ProductDto GetProduct(string productCode);
    }
}
