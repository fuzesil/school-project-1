using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QKNWZ1_HFT_2021221.Logic;
using QKNWZ1_HFT_2021221.Repository;

namespace QKNWZ1_HFT_2021221.Endpoint
{
	/// <summary>
	/// The start-up type to be used by the web host.
	/// </summary>
	public class Startup
	{
		public Startup(IConfiguration configuration) => this.Configuration = configuration;
		public IConfiguration Configuration { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">The contract for a collection of service descriptors.</param>
		/// <remarks>
		/// For more information on how to configure your application, visit <a href="https://go.microsoft.com/fwlink/?LinkID=398940">this website</a>.
		/// </remarks>.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(); // enable controllers

			// IoC
			services.AddScoped<Microsoft.EntityFrameworkCore.DbContext, Data.EisaDbContext>();

			services.AddTransient<IInternalLogic, InternalAuditLogic>();
			services.AddTransient<IExternalLogic, ExternalAuditLogic>();
			services.AddTransient<IAdministrator, AdminLogic>();

			services.AddTransient<IBrandRepository, BrandRepository>();
			services.AddTransient<ICountryRepository, CountryRepository>();
			services.AddTransient<IExpertGroupRepository, ExpertGroupRepository>();
			services.AddTransient<IMemberRepository, MemberRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();

			services.AddSignalR();
			services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = nameof(Endpoint), Version = "v1" }));
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">Provides the mechanisms to configure an application's request pipeline.</param>
		/// <param name="env">Provides information about the web hosting environment an application is running in.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{nameof(Endpoint)} v1"));
			}

			app.UseExceptionHandler(appBuilder => appBuilder.Run(async context =>
			{
				var exception = context.Features
					.Get<IExceptionHandlerPathFeature>()
					.Error;
				var response = new { Msg = exception.Message };
				await context.Response.WriteAsJsonAsync(response);
			}));

			app.UseCors(corsPolicyBuilder => corsPolicyBuilder
				.AllowCredentials()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.WithOrigins("http://localhost:49945"));

			//app.UseDeveloperExceptionPage();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<SignalRHub>("/hub");
			});
			/*
			app.UseRouting();
			//app.UseEndpoints(endpoints =>
			//{
			//    endpoints.MapGet("/", async context =>
			//    {
			//        await context.Response.WriteAsync("Hello World!");
			//    });
			//});
			app.UseEndpoints(endpoints => endpoints.MapControllers());
			*/
		}
	}
}
