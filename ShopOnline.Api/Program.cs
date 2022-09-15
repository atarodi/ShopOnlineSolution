using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopOnline.Api.Data;
using ShopOnline.Api.Repositories;
using ShopOnline.Api.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopOnlineDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShopOnlineConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IAnalysisRepository, AnalysisRepository>();

//builder.Services.AddMediatR(typeof(ForecastQueryHandler).GetTypeInfo().Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
            policy.WithOrigins("https://localhost:7206", "http://localhost:7206")
            .AllowAnyMethod()
            .WithHeaders(HeaderNames.ContentType)

    );
//app.MapPost("/rpc", async context =>
//{
//    StreamReader reader = new StreamReader(context.Request.Body);
//    string requestJson = await reader.ReadToEndAsync();

//    JsonSerializerSettings settings = new JsonSerializerSettings()
//    {
//        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
//        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
//        NullValueHandling = NullValueHandling.Include,
//        TypeNameHandling = TypeNameHandling.All
//    };

//    object? requestObject = JsonConvert.DeserializeObject(requestJson, settings);

//    IMediator? mediator = context.RequestServices.GetService<IMediator>();

//    Debug.Assert(mediator != null, nameof(mediator) + " != null");
//    object? commandQueryResponse = await mediator.Send(requestObject);

//    string responseJson = JsonConvert.SerializeObject(commandQueryResponse, settings);

//    await context.Response.WriteAsync(responseJson);
//});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
