using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyOnline.Services.Candidate;
using PharmacyOnline.Services.EmailService;
using PharmacyOnline.Services.Product;
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





// Khai báo Interface repository (DI):
builder.Services.AddScoped<ICandidateRepo, CandidateRepoClass>();
builder.Services.AddScoped<IEmailService, EmailRepoClass>();
builder.Services.AddScoped<IProductRepo, ProductRepoClass>();


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
