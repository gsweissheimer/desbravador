using System;
using System.Threading.Tasks;
using v1.Models;
using v1.Repositories;

namespace v1.Services
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(Exception ex);
    }

    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLogService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task LogErrorAsync(Exception ex)
        {
            var errorLog = new ErrorLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                LogTime = DateTime.UtcNow
            };

            await _errorLogRepository.AddErrorLogAsync(errorLog);
        }
    }
}
