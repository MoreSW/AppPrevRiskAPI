using appPrevencionRiesgos.Data.Repository;
using appPrevencionRiesgos;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Services;
using appPrevencionRiesgos.Services.Security;


namespace appPrevencionRiesgos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("https://0.0.0.0:3001"); ;
                });
    }
}
/*
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();




builder.Services.AddControllers();



//builder.Services.AddTransient<IMongoDBServices, MongoDBService>();
builder.Services.AddSingleton<IMongoDBServices, MongoDBService>();
builder.Services.AddScoped<IMongoDBServices, MongoDBService>();
builder.Services.AddControllers();
builder.Services.AddTransient<IBasicInformationService, BasicInformationService>();
builder.Services.AddTransient<IBasicInformationRepository, BasicInformationRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//Map model and entitys (equality)
builder.Services.AddAutoMapper(typeof(Program));

var aux = config.GetRequiredSection("MongoDB");
MongoDBSettings dbConf = new MongoDBSettings() ;
dbConf.ConnectionURI = aux["ConnectionURI"];
dbConf.DatabaseName = aux["DatabaseName"];
dbConf.CollectionName = aux["CollectionName"];
//Console.WriteLine(dbConf);

//builder.Services.Configure<MongoDBService>(dbConf);
//builder.Services.AddDefaultIdentity<IdentityUser>()
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

//Console.WriteLine();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });

app.UseAuthorization();

app.MapControllers();

app.Run();
*/
