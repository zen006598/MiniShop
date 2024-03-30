
using AutoMapper;
using minishop.Commons;
using minishop.Models;
using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;
using minishop.Repositories.Interfaces;
using minishop.Services.DTOs.Info;
using minishop.Services.DTOs.ResultModel;
using minishop.Services.Interfaces;

namespace minishop.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;
    private readonly IProductRepository _product;
    public ProductService(
        IMapper mapper,
        ILogger<ProductService> logger,
        IProductRepository productRepository
    )
    {
        _mapper = mapper;
        _logger = logger;
        _product = productRepository;
    }

    public async Task<IEnumerable<ProductResultModel>> AllAsync()
    {
        var products = await _product.AllAsync();
        var productResultModel = _mapper.Map<IEnumerable<ProductDataModel>, IEnumerable<ProductResultModel>>(products);
        return productResultModel;
    }

    public async Task<ProductResultModel> GetAsync(int id)
    {
        var product = await _product.GetAsync(id);
        return _mapper.Map<ProductResultModel>(product);
    }

    public async Task<IEnumerable<ProductResultModel>> GetProductsByStatusAsync(ProductStatus status)
    {
        var products = await _product.GetProductsByStatusAsync(status);
        var productResultModel = _mapper.Map<IEnumerable<ProductDataModel>, IEnumerable<ProductResultModel>>(products);
        return productResultModel;
    }

    public async Task<bool> Insert(ProductInfo info, ApplicationUser user)
    {
        var product = _mapper.Map<ProductInfo, ProductCondition>(info);
        product.CreateAt = DateTime.UtcNow;
        product.CreateBy = user.UserName;
        product.UserId = user.Id;
        return await _product.Insert(product);
    }

    public bool Update(int id, ProductInfo info)
    {
        var product = _mapper.Map<ProductInfo, ProductUpdateCondition>(info);
        product.UpdateAt = DateTime.UtcNow;
        return _product.Update(id, product);
    }
}
