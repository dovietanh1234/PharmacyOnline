using Microsoft.EntityFrameworkCore;
using PharmacyOnline.Services.Candidate;
using PharmacyOnline.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);


// singleton connect DB:
string connectionString = builder.Configuration.GetConnectionString("API");
builder.Services.AddDbContext<PharmacyOnline.Entities.PharmacyOnlineContext>(options => options.UseSqlServer(connectionString));


// add cors:
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod();

    });
});


// Add services to the container.
//tuần tự hóa và giải tuần tự hóa dữ liệu JSON
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

// Khai báo Interface repository:
builder.Services.AddScoped<ICandidateRepo, CandidateRepoClass>();
builder.Services.AddScoped<IEmailService, EmailRepoClass>();


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

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
