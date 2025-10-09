using ComandasApi.DTOs;
using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario {
                Id = 1, Nome= "Admin", Senha = "admin123", Email = "a@a.com"
            },
            new Usuario
            {
                Id = 2,Nome="jose",Senha = "123", Email = "jose@gmail.com"
            }
        };
        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] UsuarioCreatRequest usuariopost)
        {
            if(usuariopost.Senha.Length < 6)
            {
                return Results.BadRequest("A senha deve ter pelo menos 6 caracteres.");
            }
            if(usuariopost.Nome.Length < 3)
            {
                return Results.BadRequest("O nome deve ter pelo menos 3 caracteres.");
            }
            if(usuariopost.Email.Length < 6 || !usuariopost.Email.Contains("@"))
            {
                return Results.BadRequest("O email deve ter pelo menos 6 caracteres e conter '@'.");
            }

            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nome = usuariopost.Nome,
                Email = usuariopost.Email,
                Senha = usuariopost.Senha
            };
            usuarios.Add(usuario);
            
            return  Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UsuarioUpdateRequest usuarioput)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
