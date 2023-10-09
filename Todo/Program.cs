var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddSingleton<Serilog.ILogger>(logger);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(w =>
{
    w.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Todo", Version = "v1" });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<IRequestHandler<CreateTodoItemCommand, int>, CreateTodoItemHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteTodoItemCommand>, DeleteTodoItemHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateCompletedTodoItemCommand>, UpdateTodoItemCompleteHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateTodoItemCommand>, UpdateTodoItemHandler>();

builder.Services.AddTransient<IRequestHandler<GetTodoItemsQuery, List<TodoItemDto>>, GetTodoItemsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetTodoItemByIdQuery, TodoItemDto>, GetTodoItemByIdQueryHandler>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<TodoContext>();
builder.Services.AddMemoryCache();

var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json")
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .Build();

builder.Services.AddDbContext<ITodoContext, TodoContext>(options =>
{
    options.UseSqlServer(conf.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo api");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
    }
    else
    {
        await next();
    }
});
app.Run();