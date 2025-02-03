using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = await _context.Login
            .FirstOrDefaultAsync(u => u.Usuario == request.Usuario && 
                                    u.Password == request.Password);

        if (usuario == null)
        {
            return Unauthorized("Usuario o contraseña incorrectos");
        }

        return Ok(new { mensaje = "Inicio de sesión exitoso" });
    }
} 