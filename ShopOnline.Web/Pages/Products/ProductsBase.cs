using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages.Products
{
    public class ProductsBase : ComponentBase
    {
        // productservice injected in runtime
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
        //blazor lifecycle
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();

            }
            catch (Exception ex)
            {

            }

        }

    }
}
