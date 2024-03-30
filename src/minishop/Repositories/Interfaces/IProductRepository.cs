using minishop.Commons;
using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;

namespace minishop.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDataModel>> AllAsync();
    Task<IEnumerable<ProductDataModel>> GetProductsByStatusAsync(ProductStatus status);
    Task<ProductDataModel> GetAsync(int id);
    Task<bool> Insert(ProductCondition condition);
    bool Update(int id, ProductUpdateCondition condition);
}
