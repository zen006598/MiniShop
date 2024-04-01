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
        #region Product Mapping
        CreateMap<Product, ProductDataModel>();
        CreateMap<ProductCondition, Product>();
        CreateMap<ProductUpdateCondition, Product>();
        CreateMap<ProductParameter, ProductInfo>();
        CreateMap<ProductInfo, ProductUpdateCondition>();
        CreateMap<ProductDataModel, ProductResultModel>();
        CreateMap<ProductResultModel, ProductViewModel>();
        CreateMap<ProductInfo, ProductCondition>();
        CreateMap<ProductParameter, ProductInfo>();
        #endregion Product Mapping

        #region Order Mapping
        CreateMap<Order, OrderDataModel>();
        CreateMap<OrderCondition, Order>();
        // CreateMap<OrderUpdateCondition, Order>();
        CreateMap<OrderParameter, OrderInfo>();
        CreateMap<OrderParameter, OrderSearchInfo>();
        CreateMap<OrderSearchInfo, OrderSearchCondition>();
        // CreateMap<OrderInfo, OrderUpdateCondition>();
        CreateMap<OrderDataModel, OrderResultModel>();
        CreateMap<OrderResultModel, OrderViewModel>();
        CreateMap<OrderInfo, OrderCondition>();
        CreateMap<OrderCreateParameter, OrderInfo>();

        CreateMap<ShoppingCartItem, OrderItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.ToString()))
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());
        CreateMap<OrderCreateParameter, OrderCreateViewModel>();

        CreateMap<OrderParameter, OrderSearchingViewModel>()
            .ForMember(dest => dest.Orders, opt => opt.Ignore());
        #endregion Order Mapping

        #region Order Item Mapping
        CreateMap<OrderItem, OrderItemDataModel>();
        #endregion Order Item Mapping
    }
}
