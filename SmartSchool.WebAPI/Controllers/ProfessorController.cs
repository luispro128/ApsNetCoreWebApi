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
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repo, IMapper mapper) { 
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo para obter todos os professores.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
        }

        /// <summary>
        /// Metodo para obter um professor.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //api/Aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, true);
            if (professor == null) return BadRequest("Professor nao encontrado.");
            return Ok(professor);
        }

        /// <summary>
        /// Metodo para obter um professor.
        /// </summary>
        /// <param name="idAluno"></param>
        /// <returns></returns>
        //api/Aluno/1
        [HttpGet("byaluno/{alunoId}")]
        public IActionResult GetByAluno(int alunoId)
        {
            var professor = _repo.GetProfessorByAluno(alunoId, true);
            if (professor == null) return BadRequest("Professor nao encontrado.");
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        //api/Aluno
        /// <summary>
        /// Metodo para inserir um professor.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Metodo para atualizar um professor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Metodo para atualizar um professor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Metodo para excluir um professor.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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