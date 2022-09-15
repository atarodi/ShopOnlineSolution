using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Web.Services.Contracts;
using System.Text.Json;

namespace ShopOnline.Web.Pages
{
    public partial class Counter
    {
        [Inject]
        IAnalysisService AnalysisRepository { get; set; }
        public TimeSpan TimeSpanOfRandomDataWithHttpClient { get; set; }
        public TimeSpan TimeSpanOfRandomDataWithJsCallHttpCLient { get; set; }
        public string TimeSpanOfRandomDataWithJsCall { get; set; }
        [Inject]
        public IJSRuntime JS { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TimeSpanOfRandomDataWithHttpClient = TimeSpan.MaxValue;
        }


        private int currentcount = 0;

        private void incrementcount()
        {
            currentcount++;
        }

        protected async Task TimeSpanWithHttpClient_Click()
        {
            try
            {
                TimeSpanOfRandomDataWithHttpClient = await AnalysisRepository.GetRandomDataWithHttpClientTimeSpan();
            }
            catch (Exception)
            {

                //Log 
            }
        }
        protected async Task TimeSpanWithJson_Click()
        {
            try
            {
                string name = await JS.InvokeAsync<string>("prompt", "What is your name?");
                await JS.InvokeVoidAsync("alert", $"Hello {name}!");
            }
            catch (Exception)
            {

                //Log 
            }
        }

        //--------------------------------------

        protected static string message { get; set; }

        protected void CallJSMethod()
        {
            //var starttime = DateTime.Now;
            //var a = JS.InvokeAsync<bool>($"api/Analysis/GetRandomData/10000");
            //var Endtime = DateTime.Now;
            //TimeSpanOfRandomDataWithHttpClient = Endtime - starttime;
            JS.InvokeAsync<bool>("JSMethod");
        }

        protected void CallCSMethod()
        {
            JS.InvokeAsync<bool>("CSMethod");
        }

        [JSInvokable]
        public static void CSCallBackMethod()
        {
            message = "C# function called from JavaScript";
        }
        [JSInvokable]
        protected async Task TimeSpanWithHttpClient_Click2()
        {
            try
            {
                TimeSpanOfRandomDataWithJsCallHttpCLient = await AnalysisRepository.GetRandomDataWithHttpClientTimeSpan();
            }
            catch (Exception)
            {

                //Log 
            }
        }
        //----------------------------------------

        private string serializedObject;
        private async Task GetWeatherDataUsingJavaScript()
        {
            var weatherDataObject = await JS.InvokeAsync<Object>("dotNetToJsSamples.getWeatherData");
            serializedObject = JsonSerializer.Serialize(weatherDataObject);
        }
        //----
        private string serializedRandomdata;
        private async Task GetRandomDataUsingJavaScript()
        {
          //  var startTime = DateTime.Now;

            var weatherDataObject = await JS.InvokeAsync<Object>("dotNetToJsSamples3.getRandomData3");
           // serializedObject = JsonSerializer.Serialize(weatherDataObject);
           // var endTime = DateTime.Now;

            TimeSpanOfRandomDataWithJsCall = weatherDataObject.ToString();
        }
    }
}
