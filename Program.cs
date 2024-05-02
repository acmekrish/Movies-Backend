public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp", policy =>
        {
            policy.WithOrigins("*") 
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
        
        builder.Services.AddControllers();

        var app = builder.Build();

       
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors("AllowReactApp");
        app.MapControllers();

        await app.RunAsync();
    }
}
