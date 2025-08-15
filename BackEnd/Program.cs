var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string AllowOrigins = "TrustedOrigins";
string allowedHosts = "*";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowOrigins,
    builder =>
    {
        builder.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(AllowOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
