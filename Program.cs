using PublixVaultProxy;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var vaultUrl = builder.Configuration["VaultConfig:Url"];
var client = new HttpClient();
builder.Services.AddSingleton<IVaultProxyService>(sp => new VaultProxyService(vaultUrl, client));
builder.Services.AddHostedService<VaultStartUpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
