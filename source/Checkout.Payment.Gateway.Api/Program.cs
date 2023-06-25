using Checkout.Payment.Gateway.Api;
using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.Repositories;
using Checkout.Payment.Gateway.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPaymentMapper, PaymentMapper>();
builder.Services.AddSingleton<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IPaymentResponseBuilder, PaymentResponseBuilder>();

builder.Services.AddSingleton<IPaymentRepository, InMemoryDataStore>();
builder.Services.AddSingleton<IAcquiringBank, FakeAcquiringBank>();



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
