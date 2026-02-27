using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoPartsStore.Components;
using AutoPartsStore.Components.Account;
using AutoPartsStore.Data;
using Microsoft.AspNetCore.Http.Features;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AutoPartsContext>(opt => 
    opt.UseSqlite($"Data Source=autoparts.db")
       .UseLazyLoadingProxies());

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 52428800; // 50MB для загрузки файлов
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800; // 50MB для загрузки файлов
});

builder.Services.AddMudServices();

//builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AutoPartsContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AutoPartsContext>();
    dbContext.Database.Migrate(); // Применит все ожидающие миграции
    await SeedData.Initialize(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
