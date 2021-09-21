using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunoController(SmartContext context) { 
            _context = context;
        }

        //api/Aluno
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        //api/Aluno/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => x.Id == id);
            if (aluno == null) return BadRequest("Aluno nao encontrado.");
            return Ok(aluno);
        }

        //api/Aluno/Luis
        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => 
                x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome)
            );
            if (aluno == null) return BadRequest("Aluno nao encontrado.");
            return Ok(aluno);
        }

        //api/Aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        //api/Aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado.");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        //api/Aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado.");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        //api/Aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado.");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok();
        }
    }
}