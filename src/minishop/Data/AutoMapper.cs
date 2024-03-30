using AutoMapper;

using minishop.Controllers.DTOs.Parameters;
using minishop.Controllers.DTOs.ViewModels;
using minishop.Models;
using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;
using minishop.Services.DTOs.Info;
using minishop.Services.DTOs.ResultModel;

namespace minishop.Data;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Product, ProductDataModel>();
        CreateMap<ProductCondition, Product>();
        CreateMap<ProductUpdateCondition, Product>();
        CreateMap<ProductParameter, ProductInfo>();
        CreateMap<ProductInfo, ProductCondition>();
        CreateMap<ProductInfo, ProductUpdateCondition>();
        CreateMap<ProductDataModel, ProductResultModel>();
        CreateMap<ProductResultModel, ProductViewModel>();
        CreateMap<ProductInfo, ProductCondition>();
        CreateMap<ProductParameter, ProductInfo>();
    }
}
