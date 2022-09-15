using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages.Products
{
    public class ProductsByCategoryBase : ProductsBase
    {
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsWithCategory()
        {
            return from product in Products
                   group product by product.CategoryId into ProdByCatGroup
                   orderby ProdByCatGroup.Key
                   select ProdByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, ProductDto> GroupedProductDtos)
        {
            return GroupedProductDtos.FirstOrDefault(pg => pg.CategoryId == GroupedProductDtos.Key).CategoryName;
        }

    }
}
