﻿using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Context;
using CleanArchMvc.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.IoC
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services,
			  IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
			), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}