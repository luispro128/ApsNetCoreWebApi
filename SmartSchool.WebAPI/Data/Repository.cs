using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor  = false){
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }
         
        public Aluno[] GetAllAlunosByDisciplinaId(int id, bool includeProfessor = false){
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(a => a.AlunosDisciplinas.Any(d => d.DisciplinaId == id));
            return query.ToArray();
        }
         
        public Aluno GetAlunoById(int id, bool includeProfessor = false){
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(a => a.Id == id);
            return query.FirstOrDefault();
        }
        
        public Professor[] GetAllProfessores(bool includeAlunos  = false){
            IQueryable<Professor> query = _context.Professores;
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }
        
        public Professor[] GetAllProfessoresByDisciplinaId(int id, bool includeAlunos  = false){
            IQueryable<Professor> query = _context.Professores;
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == id)));
            return query.ToArray();
        }
        
        public Professor GetProfessorById(int id, bool includeAlunos  = false){
            IQueryable<Professor> query = _context.Professores;
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Id == id);
            return query.FirstOrDefault();
        }
        
    }
}