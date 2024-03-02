using bookingcare.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Cấu hình services inject các dependcies
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//automapper quét các assembly,namespace chứa program
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking care API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        //yêu cầu loại bảo mật ,cách xác thực api sẽ được thực hiện khi truy cập tài nguyên là brearer token
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            //danh sách phạm vi yêu cầu bảo mật VD:new string[] { "read", "write" }
            new string[]{}
        }
    });
});
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
//inject identity context
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<BookingCareContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<BookingCareContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingCareDBString"));
});
//my service
builder.Services.AddScoped<ISpectialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IClinicsRepository,ClinicsRepository>();
//Cấu hình endpoint
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
