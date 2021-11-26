using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using Transporte.DAL.Models;
using Transporte.Model.Security;
using UserModel = Transporte.Model.Security.User;
using UserDB = Transporte.DAL.Models.User;
using ActionDB = Transporte.DAL.Models.Action;
using ChangePasswordModel = Transporte.Model.Security.ChangePassword;

namespace Transporte.BL.Security
{
    public class User : LogicBase
    {
        private string connectionString { get; }

        public User(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public UserInfo Authenticate(Login login)
        {
            using Database db = new Database(this.connectionString);
            UserDB user = db.User.Include(x => x.RoleUser)
                .FirstOrDefault(x =>
                x.Email == login.user &&
                x.Password == login.password &&
                x.Active == true);

            if (user == null)
                throw new Exception("Credenciales no reconocidas.");

            if (user.RoleUser.Count == 0)
                throw new Exception("Su usuario no posee un rol asignado, comuníquese con el administrador del sistema.");

            user.RoleUser = db.RoleUser.Include(x => x.RoleNavigation)
                .Where(x => x.User == user.Id).ToList();

            if (!db.RoleAction.Any(x => x.Role == user.RoleUser.FirstOrDefault().Role))
                throw new Exception("El rol asignado a su usuario no cuenta con acciones asignadas. Comuníquese con el administrador del sistema.");

            return user.ToModel();
        }

        public byte GetUserId(string userName)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(userName))
                throw new Exception("Es requerido el nombre de usuario");

            using Database db = new Database(this.connectionString);
            UserDB user = db.User.Include(x => x.RoleUser).FirstOrDefault(x => x.Email == userName);

            if (user == null)
                throw new Exception("Usuario no reconocido.");

            return user.Id;
        }

        public bool ValidateKey(Guid key)
        {
            using Database db = new Database(this.connectionString);

            return db.AppKey.Any(x => x.Key == key);
        }

        public List<UserAction> GetActions(string userName)
        {
            using Database db = new Database(this.connectionString);
            byte? roleId = db.User
                .Include(x => x.RoleUser)
                .FirstOrDefault(x => x.Email == userName)
                ?.RoleUser.FirstOrDefault()?.Role;

            if (roleId.IsNull())
                throw new Exception("Role inválido.");

            List<UserAction> result = GetRecursiveActions(db, null, null, roleId);
            result.ForEach(x => { x.items = x.items.Where(y => y.items.Count > 0 || y.url != null).ToList(); });

            return result.Where(x=>x.items.Count > 0).ToList();
        }

        public void ChangePassword(ChangePasswordModel model)
        {
            using Database db = new Database(this.connectionString);

            UserDB user = db.User.First(x => x.Email == model.email && x.ResetPassword == true && x.Active == true);

            if (user == null)
                throw new Exception("Usuario no reconocido");

            if (model.password.IsNull())
                throw new Exception("El password es requerido");

            if (model.confirm.IsNull())
                throw new Exception("La confirmación es requerido");

            if (model.password != model.confirm)
                throw new Exception("Contraseñas deben ser iguales");

            user.Password = model.password;
            user.ResetPassword = false;
            db.User.Update(user);
            db.SaveChanges();
        }

        public List<UserModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.User
                .Include(x => x.RoleUser)
                .ThenInclude(x => x.RoleNavigation)
                .ToList()
                .ToListModel();
        }

        public UserModel Add(UserModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            UserDB item = model.ToDbModel();
            db.User.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            db.RoleUser.RemoveRange(db.RoleUser.Where(x => x.User == model.id));
            db.RoleUser.Add(new RoleUser() { User = model.id ?? 0, Role = model.role.id ?? 0 });
            db.SaveChanges();

            return model;
        }

        public UserModel Update(UserModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            UserDB item = model.ToDbModel();
            db.User.Update(item);
            db.SaveChanges();
            model.id = item.Id;

            db.RoleUser.RemoveRange(db.RoleUser.Where(x => x.User == model.id));
            db.RoleUser.Add(new RoleUser() { User = model.id ?? 0, Role = model.role.id ?? 0 });
            db.SaveChanges();

            return model;
        }

        public void Delete(byte userId)
        {
            if (userId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            UserDB item = db.User
                .FirstOrDefault(x => x.Id == userId);

            if (item == null)
                throw new Exception("Registro no reconocido");


            db.RoleUser.RemoveRange(db.RoleUser.Where(x => x.User == userId));
            db.User.Remove(item);
            db.SaveChanges();
        }

        #region Private
        private IQueryable<ActionDB> GetRecursiveQuery(Database db, byte? parentId)
        {
            return db.Action.Where(x => x.Parent == parentId);
        }
        private List<UserAction> GetRecursiveActions(Database db, List<ActionDB> dbActions, List<RoleAction> assigned,
            byte? roleId)
        {
            List<UserAction> result = new List<UserAction>();

            if (dbActions == null)
                dbActions = GetRecursiveQuery(db, null).ToList();

            if (assigned == null)
                assigned = db.RoleAction.Where(x => x.Role == roleId).ToList();

            if (dbActions.Count == dbActions.Where(x => !x.IsGroup).ToList().Count)
            {
                List<byte> assignedActions = assigned.Select(x => x.Action).ToList();
                dbActions = dbActions.Where(x => assignedActions.Contains(x.Id)).ToList();
            }

            dbActions.ForEach(x =>
            {
                result.Add(new UserAction()
                {
                    id = x.Id,
                    name = x.Name,
                    icon = x.Icon,
                    text = x.Text,
                    url = x.Url,
                    order = x.Order,
                    items = GetRecursiveActions(db, GetRecursiveQuery(db, x.Id).ToList(), assigned, roleId)
                });
            });

            return result.OrderBy(x => x.order).ToList();
        }
        private void Validate(UserModel model, DatabaseEvent dbEvent)
        {
            if (dbEvent != DatabaseEvent.Select)
            {
                if (dbEvent == DatabaseEvent.Update || dbEvent == DatabaseEvent.Delete)
                {
                    if (model.id.IsNull())
                        throw new Exception("El identificador es requerido");
                }

                if (dbEvent != DatabaseEvent.Delete)
                {
                    if (model.name.IsNull())
                        throw new Exception("El nombre es requerido");

                    if (model.password.IsNull())
                        throw new Exception("La contraseña es requerido");

                    if (model.role == null || model.role.id.IsNull())
                        throw new Exception("El role es requerido");
                }
            }
        }
        #endregion
    }
}
