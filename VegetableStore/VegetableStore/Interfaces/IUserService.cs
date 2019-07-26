using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegetableStore.Models.ViewModels;
using VegetableStore.Utilities;

namespace VegetableStore.Interfaces
{
    public class IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        Task<List<AppUserViewModel>> GetAllAsync();

        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(string id);


        Task UpdateAsync(AppUserViewModel userVm);
    }
}
