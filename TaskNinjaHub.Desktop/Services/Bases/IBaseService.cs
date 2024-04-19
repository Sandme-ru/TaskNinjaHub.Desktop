using System.Net.Http;
using TaskNinjaHub.Desktop.Utils.OperationResults;

namespace TaskNinjaHub.Desktop.Services.Bases;

public interface IBaseService<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllByFilterAsync(TEntity filterModel);

    Task<TEntity> GetIdAsync(int id);

    Task<HttpResponseMessage> DeleteAsync(int id);

    Task<OperationResult<TEntity>> CreateAsync(TEntity entity);

    Task<OperationResult<TEntity>> UpdateAsync(TEntity entity);
}