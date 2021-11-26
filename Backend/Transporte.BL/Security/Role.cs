using System.Collections.Generic;
using System.Linq;
using Transporte.DAL;
using RoleModel = Transporte.Model.Security.Role;

namespace Transporte.BL.Security
{
    public class Role : LogicBase
    {
        private string connectionString { get; }

        public Role(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Role(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<RoleModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.Role.ToList().ToModel();
        }
    }
}
