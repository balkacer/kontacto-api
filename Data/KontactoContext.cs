using Microsoft.EntityFrameworkCore;

namespace kontacto_api.Data
{
    public class KontactoContext: DbContext
    {
        public KontactoContext()
        {
            
        }

        public KontactoContext(DbContextOptions<KontactoContext> options) : base(options)
        {
            
        }
    }
}