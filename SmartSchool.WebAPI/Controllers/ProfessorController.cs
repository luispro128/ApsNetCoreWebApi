using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper) { 
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
        }

        //api/Aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor nao encontrado.");
            return Ok(professor);
        }

        //api/Aluno
        [HttpPost]
        public IActionResult Post(Professor model)
        {
            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado.");
        }

        //api/Aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor model)
        {
            var professor = _repo.GetAlunoById(id);
            if(professor == null) return BadRequest("Pofessor não encontrado.");
            _mapper.Map(model, professor);
            
             _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado.");
        }

        //api/Aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor model)
        {
            var professor = _repo.GetAlunoById(id);
            if(professor == null) return BadRequest("Pofessor não encontrado.");
            _mapper.Map(model, professor);
            
             _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado.");
        }

        //api/Aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if(professor == null) return BadRequest("Professor não encontrado.");
           
            _repo.Delete(professor);
            if(_repo.SaveChanges())
            {
                return Ok("Professor excluído.");
            }
            return BadRequest("Professor não excluído.");
        }
    }
}