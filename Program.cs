
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ������� ����������� middleware, ������� �������� � ��������� ������� � ���������� ����� ��������� ������� �������
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    // 1. �������� "��" ������ ��������� ������
    Console.WriteLine("> received request");
    Stopwatch sw = Stopwatch.StartNew();
    // 2. ������� ��������� ����������
    await next(context);
    // 3. �������� "�����" ������ ��������� ������
    Console.WriteLine($"> send response, request proccess time: {sw.ElapsedMilliseconds} ms.");
});

// �������: �������� ���������� middlewaare, ������� ��������:
// - ����� ����������� �������
// - ����� �������
// - �������� end-point-�������
// - ���� ����
// ��� � ����: <METHOD> http://<host>:<port>, <request time>
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    string method = context.Request.Method;
    HostString host = context.Request.Host;
    DateTime reqestTime = DateTime.Now;
    await next(context);
    Console.WriteLine(method + " " + host + " " + " " + reqestTime);
});

// �����������
app.MapGet("", () => new { Message = "server is running" });
app.MapGet("ping", () => new { Message = "pong" });


app.Run();

app.MapGet("/", () => "Hello World!");

app.Run();
