using System;
using System.Collections.Generic;
using System.Text;
using VegetableStore.Models.ViewModels;
using VegetableStore.Utilities;

namespace VegetableStore.Repositories
{
    public interface IAnnouncementService
    {
        PagedResult<AnnouncementViewModel> GetAllUnReadPaging(Guid userId, int pageIndex, int pageSize);

        bool MarkAsRead(Guid userId, string id);
    }
}
