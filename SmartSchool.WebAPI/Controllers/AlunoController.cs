using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
         public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno() { 
                Id = 1,
                Nome = "Luis",
                Sobrenome = "Alves",
                Telefone = "123"
            },
            new Aluno() { 
                Id = 2,
                Nome = "Fabiana",
                Sobrenome = "Avila",
                Telefone = "456"

            }
        };

        public AlunoController() { }

        //api/Aluno
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        //api/Aluno/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(x => x.Id == id);
            if (aluno == null) return BadRequest("Aluno nao encontrado.");
            return Ok(aluno);
        }

        //api/Aluno/Luis
        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(x => 
                x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome)
            );
            if (aluno == null) return BadRequest("Aluno nao encontrado.");
            return Ok(aluno);
        }

        //api/Aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        //api/Aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        //api/Aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        //api/Aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}