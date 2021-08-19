using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using CP380_B2_BlockWebAPI.Models;
using CP380_B1_BlockList.Models;



namespace CP380_B2_BlockWebAPI

{
    
    
    public class Startup
   
    
    {
        public Startup(IConfiguration configuration)
       
        
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
       
        
        {
        
            
            services.AddSingleton<BlockList>();
            services.AddSingleton<BlockSummary>();
            services.AddSingleton<PendingPayloads>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
           
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CP380_B2_BlockWebAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
       
        
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CP380_B2_BlockWebAPI v1"));
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            
            
            {
                endpoints.MapControllers();
            });
        
}

}
}