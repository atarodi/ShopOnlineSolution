using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ShopOnline.Web.Pages.Products
{
    public partial class SearchBox
    {

        //[Inject]
        //public NavigationManager navigationManager { get; set; }

        // [Parameter]
        public string NameFilter { get; set; }
        public void DoSearch(KeyboardEventArgs keyboardEventArgs)
        {
            if (keyboardEventArgs.Key != "Enter") return;
            var queryCollection = System.Web.HttpUtility.ParseQueryString("");
            if (string.IsNullOrEmpty(NameFilter)) return;

            queryCollection.Add("Name", NameFilter);
            // navigationManager.NavigateTo($"/ProductsWithQueryString?{queryCollection.ToString()}");

        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

    }
}
