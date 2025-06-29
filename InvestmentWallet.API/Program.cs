using InvestmentWallet.Application.Handlers;
using InvestmentWallet.Domain.Interfaces;
using InvestmentWallet.Infrastructure.Data;
using InvestmentWallet.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InvestmentWalletDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateInvestorHandler).Assembly));

builder.Services.AddScoped<IInvestorRepository, InvestorRepository>();
builder.Services.AddScoped<IInvestmentOpportunityRepository, InvestmentOpportunityRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InvestmentWalletDbContext>();
    context.Database.EnsureCreated();
}

app.Run();