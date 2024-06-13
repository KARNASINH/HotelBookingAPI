using HotelBookingAPI.Connection;
using HotelBookingAPI.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add JsonOptions to remove the camel case property naming policy.
//This is done to keep the property names in the response as it is defined in the model classes.
builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null
    );

//Initialize Serilog from appsettings.json
builder.Host.UseSerilog((context, services, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.ReadFrom.Services(services));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding SqlConnectionFactory service into DI.
builder.Services.AddTransient<SqlConnectionFactory>();

//Add services to the container.
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<AmenityRepository>();
builder.Services.AddScoped<RoomAmenityRepository>();
builder.Services.AddScoped<HotelSearchRepository>();
builder.Services.AddScoped<ReservationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
