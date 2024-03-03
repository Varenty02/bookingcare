using bookingcare.Repositories;
using bookingcare.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Cấu hình services inject các dependcies
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//automapper quét các assembly,namespace chứa program
builder.Services.AddAutoMapper(typeof(Program));
var configuration = builder.Configuration;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
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

builder.Services.AddDbContext<BookingCareContext>(options => {
    
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingCareDBString"));
},ServiceLifetime.Scoped);
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<BookingCareContext>().AddDefaultTokenProviders();
//my service
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUrlHelper>(x => 
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsetting);
builder.Services.AddSingleton<IEmailSender,SendMailService>();
builder.Services.AddScoped<ISpectialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IClinicsRepository,ClinicsRepository>();
builder.Services.AddScoped<IMetaDataRepository, MetaDataRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//Cấu hình endpoint
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
