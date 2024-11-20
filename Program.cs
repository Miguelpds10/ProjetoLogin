using ProjetoLogin.Libraries.Login;
using ProjetoLogin.Libraries.Middleware;
using ProjetoLogin.Repositorio;
using ProjetoLogin.Repositorio.Contract;

namespace ProjetoLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Adicionar a Interface como um serviço
            builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            builder.Services.AddScoped<IColaboradorRepositorio, ColaboradorRepositorio>();


            builder.Services.AddScoped<ProjetoLogin.Libraries.Sessao.Sessao>();
            builder.Services.AddScoped<LoginCliente>();
            builder.Services.AddScoped<LoginColaborador>();

            //Corrigir problema com TEMPDATA

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {


                //Definir um tempo para duração

                options.IdleTimeout = TimeSpan.FromSeconds(50);
                options.Cookie.HttpOnly = true;
                //Mostrar para o navegador que o cookie e essencial
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddMvc().AddSessionStateTempDataProvider();

            //Adicionado para manipular a Sessão
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            //AREA
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


            app.UseCookiePolicy();
            app.UseSession();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
