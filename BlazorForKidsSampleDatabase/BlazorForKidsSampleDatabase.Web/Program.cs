using BlazorForKids;
using BlazorForKids.Designer.Domain.Services;
using BlazorForKids.Designer.Domain.Services.DefaultBkServiceImplementation;

using BlazorForKidsSampleDatabase.Domain.Entities;
using BlazorForKidsSampleDatabase.Web;
using BlazorForKidsSampleDatabase.Web.Source.Main;

using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

BkCommandResult.Defaults.ErrorMessage = "O no! An error occurred during the operation. Please try again.";
BkCommandResult.Defaults.SuccessMessage = "Hooray! The operation was completed successfully.";
BkCommandResult.Defaults.WarningMessage = "Oops! The operation was not successful.";
BkDefaultText.ApplicationName = "BlazorForKidsSampleDatabase";

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.AddBlazorForKids<ApplicationDbContext>(options =>
{
    // Before enabling BkLogger, make sure the connection string under the "BkLoggingDb" section in appsettings is correct.
    // Example:
    /*
      "BkLoggingDb": {
        "ConnectionString": "Server=.;Database=Your_DataBaseName_For_Logs;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;",
        "TableName": "BlazorForKidsSampleDatabase_Logs",
        "MinimumLogLevel": "Warning"
      }
    */
    // If the connection string is incorrect or the database does not exist, enabling this will throw errors at startup.
    options.UseBkLoggerToDatabase = false;



    options.UseDefaultCachePipeline = true;
    options.AuthenticationType = BkAuthenticationType.BasicAuthentication;
    options.IdentityOptions = o =>
    {
        o.SignIn.RequireConfirmedAccount = false;
        o.Password.RequireDigit = true;
        o.Password.RequireLowercase = true;
        o.Password.RequireUppercase = true;
        o.Password.RequiredLength = 8;
        o.Password.RequireNonAlphanumeric = true;
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //uncomment below statement to create database and seed sample data
    await app.SeedSampleData();
}
else
{
    app.UseExceptionHandler(errorApp =>
   {
       errorApp.Run(context =>
       {
           var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
           if (exceptionHandlerPathFeature?.Error is not null)
           {
               var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
               logger.LogError(exceptionHandlerPathFeature.Error, "Unhandled exception occurred.");
           }

           context.Response.StatusCode = 500;
           context.Response.ContentType = "text/html";
           context.Response.Redirect("/Error");
           return Task.CompletedTask;
       });
   });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();
app.UseBlazorForKids();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
