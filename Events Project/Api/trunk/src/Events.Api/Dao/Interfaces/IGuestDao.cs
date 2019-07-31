using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IGuestDao
    {
        void Store(Guest guest);

        void Delete(Guest guest);
    }
}
