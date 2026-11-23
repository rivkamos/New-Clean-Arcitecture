using Clean.Core.Repositories;
using Clean.Core.Services;
using Clean.Data.Repositories;
using Clean.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =========================
// חיבור ה-interfaces למימושים
// =========================
// כאן, ורק כאן, נקבע מי מממש מה. זו הנקודה שבה הארכיטקטורה "מתחברת".
// שימי לב ש-Singleton ולא Scoped: הרשימה יושבת בזיכרון, ואם תיווצר
// מחדש בכל בקשה - כל משתמש שנוסף ייעלם מיד.
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
