using System.Reflection;
using BookingApi.Data.Data;
using BookingApi.Data.Helpers;
using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Repositories;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.User;
using BookingApi.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReservationDatabase")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials();
        });
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
builder.Services.AddScoped<IPerformerService, PerformerService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IPerformerRepository, PerformerRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();