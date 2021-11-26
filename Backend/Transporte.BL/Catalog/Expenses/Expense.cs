using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using ExpenseModel = Transporte.Model.Catalog.Expenses.Expense;
using ExpenseGroupedModel = Transporte.Model.Catalog.Expenses.ExpenseGrouped;
using ExpenseDB = Transporte.DAL.Models.Expense;


namespace Transporte.BL.Catalog.Expenses
{
    public class Expense : LogicBase
    {
        private string connectionString { get; }

        public Expense(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Expense(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<ExpenseModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.Expense.Include(x => x.TypeNavigation).ToList().ToModel();
        }

        public List<ExpenseGroupedModel> GetGrouped()
        {
            using Database db = new Database(this.connectionString);

            return db.ExpenseType.Include(x => x.Expense)
                .Where(x => x.Expense.Count > 0)
                .ToList()
                .ToGroupedModel();
        }

        public ExpenseModel Add(ExpenseModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Expense.Any(x=>
                x.Type == model.expenseType.id &&
                x.Name == model.name))
                throw new Exception($"Ya existe un gasto nombrado '{model.name}'");

            ExpenseDB item = model.ToDbModel();
            item.TypeNavigation = null;
            db.Expense.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public ExpenseModel Update(ExpenseModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Expense.Any(x =>
                x.Type == model.expenseType.id &&
                x.Name == model.name &&
                x.Id != model.id))
                throw new Exception($"Ya existe un gasto nombrado '{model.name}'");

            ExpenseDB item = model.ToDbModel();
            item.TypeNavigation = null;
            db.Expense.Update(item);
            db.SaveChanges();

            return model;
        }

        public void Delete(short expenseId)
        {
            if (expenseId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            ExpenseDB item = db.Expense
                .Include(x => x.Transaction)
                .FirstOrDefault(x => x.Id == expenseId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if(item.Transaction.Count > 0)
                throw new Exception("Este gasto cuenta con transacciones");

            db.Expense.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(ExpenseModel model, DatabaseEvent dbEvent)
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

                    if (model.expenseType == null || model.expenseType.id.IsNull())
                        throw new Exception("El tipo es requerido");
                }
            }
        }
        #endregion
    }
}
