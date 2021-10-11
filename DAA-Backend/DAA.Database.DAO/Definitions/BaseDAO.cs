using DAA.Database.Migrations.Contexts;

namespace DAA.Database.DAO.Definitions
{
    public class BaseDAO
    {
        protected readonly DAADbContext _context;

        public BaseDAO(DAADbContext context)
        {
            this._context = context;
        }
    }
}
