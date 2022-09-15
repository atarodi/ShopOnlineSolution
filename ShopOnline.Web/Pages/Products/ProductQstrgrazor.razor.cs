using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages.Products
{
    public partial class ProductQstrgrazor
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }


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
        protected override async Task OnInitializedAsync()
        {
            // navigationManager.LocationChanged += NavigationManager_LocationChanged;
            searchWithQueryString();

        }

        //private void NavigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        //{
        //    searchWithQueryString();
        //}

        private async void searchWithQueryString()
        {
            var url = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            QueryHelpers.ParseQuery(url.Query).TryGetValue("NameFilter", out var _nameFilter);
            Products = await ProductService.GetItems();
            if (!string.IsNullOrEmpty(_nameFilter))
            {
                Products = Products.Where(x => x.Name.Contains(_nameFilter)).ToList();
            }

        }

        //public void Dispose()
        //{
        //    navigationManager.LocationChanged -= NavigationManager_LocationChanged;

        //}

        //public ValueTask DisposeAsync()
        //{
        //}
    }
}
