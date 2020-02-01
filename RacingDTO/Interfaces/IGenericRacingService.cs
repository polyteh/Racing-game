using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Interfaces
{
    public interface IGenericRacingService<DTOModel>
        where DTOModel:class
    {
        Task CreateAsync(DTOModel item);
        Task<DTOModel> FindByIdAsync(int? id);
        Task<IEnumerable<DTOModel>> GetAllAsync();
        IEnumerable<DTOModel> GetAll(Func<DTOModel, bool> predicate);
        Task<bool> RemoveAsync(int? id);
        Task UpdateAsync(DTOModel item);
        Task<DTOModel> FindByModelAsync(string model);
        DTOModel FindByModel(string model);
    }
}
