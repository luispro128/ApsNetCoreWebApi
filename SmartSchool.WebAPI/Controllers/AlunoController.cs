using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper) { 
            _repo = repo;
            _mapper = mapper;
        }

        //api/Aluno
        /// <summary>
        /// Metodo responsável para retornar todos os alunos. Teste
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var result = await _repo.GetAllAlunosAsync(pageParams, true);
            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(result);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPage);
            return Ok(alunosResult);
        }

        /// <summary>
        /// Metodo responsável para retornar todos os alunos. Teste
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetByDisciplinaId(int id)
        {
            var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);
            return Ok(result);
        }

        //api/Aluno/1
        /// <summary>
        /// Metodo reponsavel par retornar um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id); 
            if (aluno == null) return BadRequest("Aluno nao encontrado.");
            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);
            return Ok(alunoDto);
        }

        //api/Aluno
        /// <summary>
        /// /Metodo para inserir um aluno.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado.");
        }

        //api/Aluno
        /// <summary>
        /// Metodo para atualizar um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado.");
            _mapper.Map(model, aluno);
            
             _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não atualizado.");
        }

        //api/Aluno
        /// <summary>
        /// Metodo para atualizar um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoPatchDto model)
        {
             var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado.");
            _mapper.Map(model, aluno);
            
             _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno));
            }
            return BadRequest("Aluno não atualizado.");
        }

        //api/Aluno
        /// <summary>
        /// Metodo para atualizar um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}/trocarestado")]
        public IActionResult TrocarEstado(int id, TrocaEstadoDto trocaEstado)
        {
            var aluno = _repo.GetAlunoById(id);
            aluno.Ativo = trocaEstado.Estado;
            if(aluno == null) return BadRequest("Aluno não encontrado.");
                   
             _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                var status = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {status} com sucesso!" });
            }
            return BadRequest("Aluno não atualizado.");
        }

        //api/Aluno
        /// <summary>
        /// Metodo para excluir um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado.");
           
            _repo.Delete(aluno);
            if(_repo.SaveChanges())
            {
                return Ok("Aluno excluído.");
            }
            return BadRequest("Aluno não excluído.");
        }
    }
}