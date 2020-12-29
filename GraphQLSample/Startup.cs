using GraphiQl;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Server;
using GraphQL.Types;
using GraphQLSample.Controllers;
using GraphQLSample.Models;
using GraphQLSample.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace GraphQLSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(Mvcoptions =>
            { Mvcoptions.EnableEndpointRouting = false; }
            ).SetCompatibilityVersion(CompatibilityVersion.Latest).AddNewtonsoftJson();

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter> ();
            services.AddScoped<AuthorService>();
            services.AddScoped<AuthorRepository>();
            services.AddScoped<AuthorQuery>();
            services.AddScoped<AuthorType>();
            services.AddScoped<BlogPostType>();
            services.AddScoped<ISchema, GraphQLDemoSchema>();
            services.AddScoped<AuthorMutations>();
            services.AddScoped<CreateAuthorInputType>();
            services.AddScoped<CreateAuthorInput>();
            services.AddControllersWithViews();
            /*services.AddGraphQL((options, provider) =>
             {
                 //options.EnableMetrics = Environment.IsDevelopment();
                 var logger = provider.GetRequiredService<ILogger<Startup>>();
                 options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
             }).AddSystemTextJson().AddDataLoader().AddWebSockets().AddGraphTypes(typeof(GraphQLDemoSchema));*/

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
            })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
            .AddSystemTextJson();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseWebSockets();

            app.UseGraphiQl("/graphiql", "/graphql");
            /*app.UseGraphQL<ISchema>("/graphql");
            app.UseGraphQLWebSockets<GraphQLDemoSchema>("/graphql");

            app.UseGraphQL<ISchema>();*/

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground();

            app.UseMvc();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });*/
        }
    }
}
