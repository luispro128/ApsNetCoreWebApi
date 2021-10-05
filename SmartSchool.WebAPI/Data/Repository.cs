using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartSchool.WebAPI.Helpers;

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

        public async Task<PageList<Aluno>> GetAllAlunosAsync(
            PageParams pageParams,
            bool includeProfessor  = false){
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);

            if(!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(x => x.Nome.ToUpper().Contains(pageParams.Nome) ||
                                 x.Sobrenome.ToUpper().Contains(pageParams.Nome));
            if(pageParams.Matricula > 0)
                query = query.Where(x => x.Matricula == pageParams.Matricula);
            if(pageParams.Ativo != null)
                query = query.Where(x => x.Ativo == (pageParams.Ativo == 0 ? false : true));

            //return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
         
        public async Task<Aluno[]> GetAllAlunosByDisciplinaIdAsync(int id, bool includeProfessor = false){
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(a => a.AlunosDisciplinas.Any(d => d.DisciplinaId == id));
            return await query.ToArrayAsync();
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

        public Professor[] GetProfessorByAluno(int id, bool includeAlunos  = false){
            IQueryable<Professor> query = _context.Professores;
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.AlunoId == id)));

            return query.ToArray();
        }
        
    }
}