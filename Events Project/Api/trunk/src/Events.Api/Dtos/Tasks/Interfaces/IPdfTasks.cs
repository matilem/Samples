using System.Collections.Generic;
using Aafp.Events.Api.Models.Badges;

namespace Aafp.Events.Api.Tasks.Interfaces
{
    public interface IPdfTasks
    {
        byte[] GetPdf(IEnumerable<BadgeBase> badges);
    }
}
