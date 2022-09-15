using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Threading;

namespace ShopOnline.Web.Pages.ShoppingCart
{
    public class ShoppingCartBase : ComponentBase
    {

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public IJSRuntime Js { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummaryTotals();

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }

        }
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            await RemoveCartItem(id);

            CalculateCartSummaryTotals();
            //  CartChanged();
        }
        private async Task RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);

            //await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

        }
        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };

                    var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);


                    await UpdateItemTotalPrice(returnedUpdateItemDto);
                    CalculateCartSummaryTotals();

                    //CartChanged();

                    await MakeUpdateQtyButtonVisible(id, false);


                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);

                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }

            //await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

        }
        protected async Task UpdateQty_Input(int id)
        {
            await MakeUpdateQtyButtonVisible(id, true);
        }

        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            //UpdateQtyClass = "btn btn-info btn-sm update_Qty_InVisible";

            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);


            //try
            //{
            //    var lst = new List<int>();
            //    var threadOne = new Thread(new ThreadStart(() => lst.Add(1)));
            //    var threadTwo = new Thread(new ThreadStart(() => lst.Add(2)));
            //    var threadThree = new Thread(new ThreadStart(() => lst.Add(3)));
            //    var threadFour = new Thread(new ThreadStart(() => lst.Add(4)));

            //    threadOne.Start();
            //    threadTwo.Start();
            //    threadThree.Start();
            //    threadFour.Start();

            //    UpdateQtyClass += String.Join("_", lst);
            //}
            //catch (Exception ex)
            //{
            //    var ss = ex;
            //}


            //threadOne.Join();
            //threadTwo.Join();
            //threadThree.Join();
            //threadFour.Join();



        }

        public string UpdateQtyClass = "btn btn-info btn-sm update_Qty_Visible";

    }
}
