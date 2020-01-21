using AutoMapper;
using RacingDAL.Interfaces;
using RacingDTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Services
{
    public abstract class GenericService<DTOModel, DALModel> : IGenericRacingService<DTOModel>, IDisposable
        where DTOModel : class
        where DALModel : class, IEntity
    {
        protected IMapper _mapper;
        private readonly IGeneralDBRepository<DALModel> _repository;
        public GenericService(IGeneralDBRepository<DALModel> rep)
        {
            _repository = rep;
        }
        public async Task CreateAsync(DTOModel item)
        {
            var dalModelToCreate = Map(item);
            await _repository.CreateAsync(dalModelToCreate);
        }

        public async Task<DTOModel> FindByIdAsync(int? id)
        {
            var entity = await _repository.FindByIdAsync(id);
            return Map(entity);
        }

        public async Task<IEnumerable<DTOModel>> GetAllAsync()
        {
            var listOfDal = await _repository.GetAllAsync();
            return Map(listOfDal);
        }

        public IEnumerable<DTOModel> GetAll(Func<DTOModel, bool> predicate)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateAsync(DTOModel item)
        {
            var dalModelToUpdate = Map(item);
            await _repository.UpdateAsync(dalModelToUpdate);
        }
        public abstract DTOModel Map(DALModel entity);
        public abstract DALModel Map(DTOModel blmodel);

        public abstract IEnumerable<DTOModel> Map(IEnumerable<DALModel> entity);
        public abstract IEnumerable<DALModel> Map(IEnumerable<DTOModel> entity);

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<bool> RemoveAsync(DTOModel item)
        {
            var dalModelToDelete = Map(item);
            var deleteResult = await _repository.RemoveAsync(dalModelToDelete);
            return deleteResult;
        }
    }
}
