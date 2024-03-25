
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// добавим собственный middleware, который сообщает о получении запроса и возвращает время обработки данного запроса
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    // 1. действия "ДО" вызова следующей стадии
    Console.WriteLine("> received request");
    Stopwatch sw = Stopwatch.StartNew();
    // 2. вызвать следующий обработчик
    await next(context);
    // 3. действия "ПОСЛЕ" вызова следующей стадии
    Console.WriteLine($"> send response, request proccess time: {sw.ElapsedMilliseconds} ms.");
});

// задание: написать обработчик middlewaare, который логирует:
// - время поступления запроса
// - метод запроса
// - конечный end-point-запроса
// - хост порт
// Все в виде: <METHOD> http://<host>:<port>, <request time>
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    string method = context.Request.Method;
    HostString host = context.Request.Host;
    DateTime reqestTime = DateTime.Now;
    await next(context);
    Console.WriteLine(method + " " + host + " " + " " + reqestTime);
});

// обработчики
app.MapGet("", () => new { Message = "server is running" });
app.MapGet("ping", () => new { Message = "pong" });


app.Run();

app.MapGet("/", () => "Hello World!");

app.Run();
