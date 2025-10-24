using ComandasApi.DTOs;
using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        //variave que representa o banco de dados 
        public ComandaDBContext _context { get; set; }
        // construtor
        public UsuarioController(ComandaDBContext context)
        {
            _context = context;
        } 
        
        
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            //conectar no banco de dados e trazer a consulta select * from usuarios
            var usuarios = _context.Usuarios.ToList();
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
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
             
                Nome = usuariopost.Nome,
                Email = usuariopost.Email,
                Senha = usuariopost.Senha
            };
            // adiciona o usuario no contexto do db
            _context.Usuarios.Add(usuario);
            // executa o insert no banco de dados
            _context.SaveChanges();

            return  Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioput)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
               return  Results.NotFound($"Usario do id {id} nao encontrado.");
            }
            usuario.Nome = usuarioput.Nome;
            usuario.Email = usuarioput.Email;
            usuario.Senha = usuarioput.Senha;
            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
                return Results.NotFound($"usuario {id} nao encontrado");
            }
             _context.Usuarios.Remove(usuario);
            
            var removido = _context.SaveChanges();
            if(removido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
