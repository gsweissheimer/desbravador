using System.Threading.Tasks;
using v1.Data;
using v1.Models;

namespace v1.Repositories
{
    public interface IErrorLogRepository
    {
        Task AddErrorLogAsync(ErrorLog errorLog);
    }

    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly ApplicationDbContext _context;

        public ErrorLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddErrorLogAsync(ErrorLog errorLog)
        {
            _context.ErrorLogs.Add(errorLog);
            await _context.SaveChangesAsync();
        }
    }
}
