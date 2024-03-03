using Backend.Application.Enums;
using Backend.Domain.Common;

namespace Backend.Application.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<int> Count { get; }

        public Task<TEntity> Create(TEntity entity);

        public Task Update(Guid id, TEntity incoming);

        public Task<TEntity[]> Read(
            int? limit,
            int? offset,
            string[]? sortCriterias,
            SortOrder? order,
            string[]? fields,
            string filters
        );

        public Task<TEntity> Read(Guid id, bool withRelatedEntities = false);

        public Task<TEntity> Delete(Guid id);
    }
}
