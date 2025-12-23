var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// --- CONFIGURACIÓN DE SWAGGER ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CONFIGURACIÓN DE CORS ---
builder.Services.AddCors(options => {
    options.AddPolicy("AllowBlazor", policy => 
        policy.WithOrigins("http://localhost:5120", "https://localhost:5120") // Tu puerto de Blazor
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Habilitar Swagger siempre o solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryHub API v1");
        // Esto hace que Swagger sea la página de inicio (opcional)
        options.RoutePrefix = string.Empty; 
    });
}

app.UseCors("AllowBlazor");
app.MapControllers();
app.Run();