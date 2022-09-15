using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages.ProductDetails
{
    public partial  class ProductDetail//: ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Parameter]
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetItem(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    
        // for AddToCart Action :
        [Inject]
        public NavigationManager NavigationManager { get; set; }



        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);

                //if (cartItemDto != null)
                //{
                //    ShoppingCartItems.Add(cartItemDto);
                //    await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
                //}

                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {

                //Log 
            }
        }


    


    }
}
