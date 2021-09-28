using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();
         Aluno[] GetAllAlunos(bool includeProfessor = false);
         Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
         Aluno[] GetAllAlunosByDisciplinaId(int id, bool includeProfessor = false);
         Aluno GetAlunoById(int id, bool includeProfessor = false);
         Professor[] GetAllProfessores(bool includeAlunos = false);
         Professor[] GetAllProfessoresByDisciplinaId(int id, bool includeAlunos = false);
         Professor GetProfessorById(int id, bool includeAlunos = false);
    }
}