using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogSite.API.Filters;
using BlogSite.API.Middlewares;
using BlogSite.API.Modules;
using BlogSite.Repository;
using BlogSite.Service.Mapping;
using BlogSite.Service.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//FluentValidation - Fiter
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TBlogDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//Filter
builder.Services.AddScoped(typeof(NotFoundFilter<,>));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MasterProfile), typeof(TransactionProfile), typeof(UserBaseProfile));

//SqlConnection
builder.Services.AddDbContext<BlogSiteContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(BlogSiteContext)).GetName().Name);
    });
});

//AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepositoryServiceModule());
});

//CORS
//builder.Services.AddCors(opts =>
//{
//    opts.AddDefaultPolicy(build =>
//    {
//        build.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors();

app.UseBlogSiteException();

app.UseAuthorization();

app.MapControllers();

app.Run();
