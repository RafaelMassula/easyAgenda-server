using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgendaService;
using EasyAgendaService.Exceptions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using EasyAgenda.ExtensionMethods;

namespace EasyAgenda.Data.DAL
{
    public class PeopleDAL : IPeopleDAL
    {
        private readonly EasyAgendaContext _context;
        private readonly string _connectionString;
        public PeopleDAL(EasyAgendaContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public async Task<People> Get(int id)
        {
            var people = await _context.People.FindAsync(id);
            if (people == null)
            {
                throw new KeyNotFoundException("Not found");
            }
            return people;
        }

        public async Task<IEnumerable<People>> GetAll()
        {
            try
            {
                var people = await _context.People
                    .OrderBy(people => people.Name)
                    .ToListAsync();
                return people;
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<int> InsertReturn(PeopleDTO people)
            {
            string query = DmlService<PeopleDTO>
                           .GetQueryInsertReturn(people, people.TableName());
            try
            {
                using var connection = new DbSession(_connectionString).Connection;
                return (int)await connection
                                 .ExecuteScalarAsync(sql: query, param: people);
            }
            catch (CpfException error)
            {
                throw new CpfException(error.Message);
            }
            catch (BirthDateException error)
            {
                throw new BirthDateException(error.Message);
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task Insert(PeopleDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Update(PeopleDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
