using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TipperERP.Application.Action;
using TipperERP.Application.Attachment;
using TipperERP.Application.AuditLog;
using TipperERP.Application.Auth;
using TipperERP.Application.Customer;
using TipperERP.Application.DocumentType;
using TipperERP.Application.Driver;
using TipperERP.Application.DriverDocument;
using TipperERP.Application.Dumping;
using TipperERP.Application.ExpenseType;
using TipperERP.Application.Financier;
using TipperERP.Application.IncomeType;
using TipperERP.Application.InsuranceCompany;
using TipperERP.Application.LoanEmi;
using TipperERP.Application.MaterialType;
using TipperERP.Application.PaymentMode;
using TipperERP.Application.Route;
using TipperERP.Application.Site;
using TipperERP.Application.Tax;
using TipperERP.Application.Tipper;
using TipperERP.Application.TipperDocument;
using TipperERP.Application.TripRates;
using TipperERP.Application.Users;
using TipperERP.Infrastructure.Data;
using TipperERP.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// DbContext
builder.Services.AddDbContext<TipperErpDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IDumpingService, DumpingService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IMaterialTypeService, MaterialTypeService>();
builder.Services.AddScoped<ITripRateService, TripRateService>();
builder.Services.AddScoped<ITipperService, TipperService>();
builder.Services.AddScoped<IFinancierService, FinancierService>();
builder.Services.AddScoped<ITipperDocumentService, TipperDocumentService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IDriverDocumentService, DriverDocumentService>();
builder.Services.AddScoped<IInsuranceCompanyService, InsuranceCompanyService>();
builder.Services.AddScoped<ILoanEmiService, LoanEmiService>();
builder.Services.AddScoped<IPaymentModeService, PaymentModeService>();
builder.Services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
builder.Services.AddScoped<IIncomeTypeService, IncomeTypeService>();
builder.Services.AddScoped<ITaxService, TaxService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddSingleton<FileStorageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TipperERP API", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token only",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

// JWT Auth
var key = Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new Exception("Jwt:Key not configured"));

builder.Services
    .AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = !string.IsNullOrEmpty(config["Jwt:Issuer"]),
            ValidIssuer = config["Jwt:Issuer"],
            ValidateAudience = !string.IsNullOrEmpty(config["Jwt:Audience"]),
            ValidAudience = config["Jwt:Audience"],
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");
app.MapControllers();

app.Run();
