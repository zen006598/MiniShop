using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minishop.Commons;
using minishop.Data;
using minishop.Models;
using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;
using minishop.Repositories.Interfaces;

namespace minishop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(
        ApplicationDbContext dbContext,
        IMapper mapper,
        ILogger<ProductRepository> logger
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDataModel>> AllAsync()
    {
        try
        {
            var products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDataModel>>(products);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in ProductRepository AllAsync method", ex);
        }
    }

    public async Task<ProductDataModel> GetAsync(int id)
    {
        try
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductDataModel>(product);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in ProductRepository GetAsync method", ex);
        }
    }

    public async Task<IEnumerable<ProductDataModel>> GetProductsByStatusAsync(ProductStatus status)
    {
        try
        {
            var products = await _dbContext.Products
                               .Where(p => p.Status == status)
                               .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDataModel>>(products);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in ProductRepository GetProductsByStatusAsync method", ex);
        }
    }

    public async Task<bool> Insert(ProductCondition condition)
    {
        try
        {
            Product productToInsert = _mapper.Map<Product>(condition);
            await _dbContext.Products.AddAsync(productToInsert);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"{ex.Message}, {ex.InnerException}");
            return false;
        }
    }

    public bool Update(int id, ProductUpdateCondition condition)
    {
        try
        {
            var productToUpdate = _dbContext.Products.Find(id);
            if (productToUpdate is null)
            {
                _logger.LogError($"Product with id: {id}, hasn't been found in the database.");
                return false;
            }
            _mapper.Map(condition, productToUpdate);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"{ex.Message}, {ex.InnerException}");
            return false;
        }
    }
}
