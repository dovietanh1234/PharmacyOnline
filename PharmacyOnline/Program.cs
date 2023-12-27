using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyOnline.Services.Candidate;
using PharmacyOnline.Services.EmailService;
using PharmacyOnline.Services.ManageCandidates;
using PharmacyOnline.Services.Product;
using PharmacyOnline.Services.ProfileService;
using PharmacyOnline.Services.Statistics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// singleton connect DB:
string connectionString = builder.Configuration.GetConnectionString("API");
builder.Services.AddDbContext<PharmacyOnline.Entities.PharmacyOnlineContext>(options => options.UseSqlServer(connectionString));


// add cors:
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


// Add services to the container.
//tuần tự hóa và giải tuần tự hóa dữ liệu JSON
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

// implement Authentication Jwt Bearer:



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]) )
        };
    
    });



// tích hợp google sheet:
string clientSecret = builder.Configuration["GGSHEET:GGSHEETClientSecretPath"];
string clientSecretPathOnAzure = Path.Combine(Directory.GetCurrentDirectory(), "eloquent-figure-409302-ae6d2cc39f9e.json");
builder.Services.AddSingleton(s =>
{
    var credential = GoogleCredential.FromStream(new FileStream(clientSecretPathOnAzure, FileMode.Open, FileAccess.Read)).CreateScoped(SheetsService.Scope.Spreadsheets);
    return new SheetsService(new BaseClientService.Initializer
    {
        HttpClientInitializer = credential,
        ApplicationName = builder.Configuration["GGSHEET:GGSHEETNameApplication"]
    });
});


// Khai báo Interface repository (DI):
builder.Services.AddScoped<ICandidateRepo, CandidateRepoClass>();
builder.Services.AddScoped<IEmailService, EmailRepoClass>();
builder.Services.AddScoped<IProductRepo, ProductRepoClass>();
builder.Services.AddScoped<IManageCanRepo, ManageCanRepoClass>();
builder.Services.AddScoped<IProfileRepo, ProfileRepoClass>();
builder.Services.AddScoped<IStatistics, StatisticsRepoClass>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
