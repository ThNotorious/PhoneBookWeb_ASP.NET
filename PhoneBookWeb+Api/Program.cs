using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneBookWeb_Api.Authorize;
using PhoneBookWeb_Api.Data;
using PhoneBookWeb_Api.Interfaces;
using PhoneBookWeb_Api.Interfaces.Password;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IJWT, JWT>();
builder.Services.AddScoped<IDataContact, DataContact>();
builder.Services.AddScoped<IAccountData, AccountData>();

builder.Services.AddAuthentication(options => //��������� �������������� � DI
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => //��������� ��������� �������������� � ������� JWT Bearer token
{
    options.RequireHttpsMetadata = false; //������ HTTPS-���������� ��� �������� ������, ���� true
    options.SaveToken = true; //��������� ����� � ���������, ��������� ������������ � ����������
    options.TokenValidationParameters = new TokenValidationParameters //������� ��������� ������
    {
        ValidateIssuerSigningKey = true, //�������� ������� ������ ��� ��������������
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SecretKey"))),//��������� ���� ��� �������
        ValidateIssuer = false, //�������� �������� ������
        ValidateAudience = false //�������� ��������� ������
    };
});


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

