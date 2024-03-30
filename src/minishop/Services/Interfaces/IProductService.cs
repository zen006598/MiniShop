
using minishop.Commons;
using minishop.Models;
using minishop.Services.DTOs.Info;
using minishop.Services.DTOs.ResultModel;

namespace minishop.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResultModel>> AllAsync();
    Task<IEnumerable<ProductResultModel>> GetProductsByStatusAsync(ProductStatus status);

    Task<ProductResultModel> GetAsync(int id);

    Task<bool> Insert(ProductInfo info, ApplicationUser user);

    bool Update(int id, ProductInfo info);
}
