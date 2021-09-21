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
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context) { 
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        //api/Aluno/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(x => x.Id == id);
            if (professor == null) return BadRequest("Professor nao encontrado.");
            return Ok(professor);
        }

        //api/Aluno/Luis
        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(x => 
                x.Nome.Contains(nome) 
            );
            if (professor == null) return BadRequest("Professor nao encontrado.");
            return Ok(professor);
        }

        //api/Aluno
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        //api/Aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado.");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        //api/Aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado.");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        //api/Aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado.");
            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}