using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication;
using StoreAPI_client_grpc_;
using StoreAPI_client_grpc_.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//const string schema = "BasicAuthentication"; 
//builder.Services.AddAuthentication(schema)
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(schema, opt =>
//    {

//    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

app.MapGrpcService<gRPC_OrdreService>().RequireCors("All");

app.UseAuthorization();

app.MapControllers();

app.Run();
