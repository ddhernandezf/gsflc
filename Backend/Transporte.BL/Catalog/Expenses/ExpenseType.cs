using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using ExpenseTypeModel = Transporte.Model.Catalog.Expenses.ExpenseType;
using ExpenseTypeDB = Transporte.DAL.Models.ExpenseType;

namespace Transporte.BL.Catalog.Expenses
{
    public class ExpenseType : LogicBase
    {
        private string connectionString { get; }

        public ExpenseType(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ExpenseType(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<ExpenseTypeModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.ExpenseType.ToList().ToModel();
        }

        public ExpenseTypeModel Add(ExpenseTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.ExpenseType.Any(x=>x.Name == model.name))
                throw new Exception($"Ya existe un gasto nombrado '{model.name}'");

            ExpenseTypeDB item = model.ToDbModel();
            db.ExpenseType.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public ExpenseTypeModel Update(ExpenseTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.ExpenseType.Any(x => x.Name == model.name && x.Id != model.id))
                throw new Exception($"Ya existe un gasto nombrado '{model.name}'");

            db.ExpenseType.Update(model.ToDbModel());
            db.SaveChanges();

            return model;
        }

        public void Delete(byte expenseId)
        {
            if (expenseId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            ExpenseTypeDB item = db.ExpenseType
                .Include(x => x.Expense)
                .FirstOrDefault(x => x.Id == expenseId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Expense.Count > 0)
                throw new Exception("Este tipo cuenta con gastos relacionados");

            db.ExpenseType.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(ExpenseTypeModel model, DatabaseEvent dbEvent)
        {
            if (dbEvent != DatabaseEvent.Select)
            {
                if (dbEvent == DatabaseEvent.Delete || dbEvent == DatabaseEvent.Update)
                {
                    if (model.id.IsNull()) 
                        throw new Exception("El identificador es requerido");
                }

                if (dbEvent != DatabaseEvent.Delete)
                {
                    if (model.name.IsNull())
                        throw new Exception("El nombre es requerido");
                }
            }
        }
        #endregion
    }
}
