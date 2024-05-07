using WashingtonStoreWebApi.Configurations;
using WashingtonStoreWebApi.Infrastructure.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<StoreDbSettings>(builder.Configuration.GetSection(nameof(StoreDbSettings)));
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

//will rnot emove the suffix "Async" applied to controller action names ( to resolve the problem of redirection using CreatedAtAction )
builder.Services.AddControllers(options=>options.SuppressAsyncSuffixInActionNames = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
