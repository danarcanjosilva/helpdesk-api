using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

var tickets = new List<Ticket>()
{
    new Ticket
    {
        Id = 1,
        Title = "Erro no login",
        Description = "Usuário não consegue acessar",
        Status = "Open"
    }
};

app.MapGet("/api/tickets", () =>
{
    return tickets;
});

app.MapPost("/api/tickets", ([FromBody] Ticket ticket) =>
{
    ticket.Id = tickets.Count + 1;

    tickets.Add(ticket);

    return Results.Ok(ticket);
});

app.Run();

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Open";
}