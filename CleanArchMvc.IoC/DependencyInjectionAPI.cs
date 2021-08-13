﻿using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Context;
using CleanArchMvc.Infra.Identity;
using CleanArchMvc.Infra.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchMvc.IoC
{
	public static class DependencyInjectionAPI
	{
		public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
			), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();

			services.AddScoped<IAuthenticate, AuthenticateService>();

			services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

			var myhandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
			services.AddMediatR(myhandlers);

			return services;
		}
	}
}