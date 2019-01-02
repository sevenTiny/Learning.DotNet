using SevenTiny.Bantina.Spring;
using SevenTiny.Bantina.Spring.DependencyInjection;
using SevenTiny.Bantina.Spring.Middleware;

namespace Test.SevenTiny.Bantina.Spring
{
    public class StartUp : SpringStartUp
    {
        public override void Configure(IApplicationBuilder app)
        {
            app.UseDynamicProxy();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BusinessService>();
            services.AddSingleton<IBusinessService, BusinessService>();
        }

        public override void Start()
        {
            base.Start();
        }
    }
}
