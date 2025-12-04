using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComandasApi;
using ComandasApi.Models;
using ComandasApi.DTOs;

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        public ComandaDBContext _context { get;set; }

        public FuncionarioController(ComandaDBContext context)
        {
            _context = context;
        }

        // GET: api/Funcionario
        [HttpGet]
        public IResult Get()
        {
            var funcionario = _context.Funcionario.ToList();
            return Results.Ok(funcionario);
        }

        // GET: api/Funcionario/5
        [HttpGet("{id}")]
        public IResult Get(int id) {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Id == id);
            if (funcionario == null) { 
            return Results.NotFound();
            }
            return Results.Ok(funcionario);
        }

        // PUT: api/Funcionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] FuncionarioUpdateRequest funcput)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return Results.NotFound();
            }
            funcionario.Nome = funcput.Nome;
            funcionario.Cargo = funcput.Cargo;
            funcionario.Salario = funcput.Salario;
            _context.SaveChanges();
            return Results.Ok(funcionario);
                
        }


        [HttpPost]
        public IResult Post([FromBody] FuncionarioCreateRequest func) {
            var funcionario = new Funcionario
            {
                Nome = func.Nome,
                Cargo = func.Cargo,
                Salario = func.Salario,
            };
            _context.Funcionario.Add(funcionario);
            _context.SaveChanges();
            return Results.Created($"/api/funcionario/{funcionario.Id}", funcionario);
        }

        // DELETE: api/Funcionario/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Id == id);
            if (funcionario is null)
            {
                return Results.NotFound($"funcionario {id} nao encontrado");
            }
            _context.Funcionario.Remove(funcionario);
            var remove = _context.SaveChanges();
            if (remove > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }

        
    }
}
