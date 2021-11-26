using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Transporte.BL.Enum;
using Transporte.Model.Operation;
using Database = Transporte.DAL.Database;
using TransactionDB = Transporte.DAL.Models.Transaction;
using TransactionModel = Transporte.Model.Operation.Transaction;

namespace Transporte.BL.Operation
{
    public class Transaction : LogicBase
    {
        private string connectionString { get; }
        private General general { get; }

        public Transaction(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Transaction(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
            this.general = new General();
        }

        public List<TransactionGrid> Get(int year, int month, byte vehicleId)
        {
            if(year.ToString().Length != 4)
                throw new Exception("Formato de año incorrecto");

            if (month.ToString().Length < 1 && month.ToString().Length > 2)
                throw new Exception("Formato de mes incorrecto");

            using Database db = new Database(this.connectionString);

            List<TransactionType> tranTypes = general.GetTransactionType();

            return db.Transaction
                .Include(x => x.VehicleNavigation)
                .ThenInclude(x => x.TypeNavigation)
                .Include(x => x.VehicleNavigation)
                .ThenInclude(x => x.BrandNavigation)
                .Include(x => x.VehicleNavigation)
                .ThenInclude(x => x.ModelNavigation)
                .Include(x => x.VehicleNavigation)
                .ThenInclude(x => x.RegistrationTypeNavigation)
                .Include(x => x.VehicleNavigation)
                .Include(x => x.ExpenseNavigation)
                .ThenInclude(x => x.TypeNavigation)
                .Include(x => x.ServiceNavigation)
                .ThenInclude(x => x.TypeNavigation)
                .Include(x => x.UserNavigation)
                .Where(x=>
                    x.TransactionDate.Year == year &&
                    x.TransactionDate.Month == month &&
                    x.Vehicle == vehicleId)
                .ToList()
                .ToModel()
                .Select(x => new TransactionGrid()
                {
                    id = x.id ?? 0,
                    type = tranTypes.FirstOrDefault(y=>y.id == (x.service == null ? 2 : 1)),
                    movement = x.service == null ? x.expense.ToModel() : x.service.ToModel(),
                    registerDate = x.registerDate,
                    transactionDate = x.transactionDate,
                    total = x.total,
                    vehicle = x.vehicle,
                    userId = x.userId,
                    userName = x.userName
                })
                .ToList();
        }

        public TransactionModel Add(TransactionModel model)
        {
            using Database db = new Database(this.connectionString);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                Validate(model, DatabaseEvent.Insert);

                model.total = 0;
                TransactionDB item = model.ToDbModel();
                db.Transaction.Add(item);
                db.SaveChanges();
                model.id = item.Id;

                transaction.Commit();
                return model;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public TransactionModel Update(TransactionModel model)
        {
            using Database db = new Database(this.connectionString);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                Validate(model, DatabaseEvent.Update);

                TransactionDB item = model.ToDbModel();
                item.Total = db.TransactionDetail
                    .Where(x => x.Transaction == item.Id)
                    .Sum(x => x.TotalPrice);

                db.Transaction.Update(item);
                db.SaveChanges();

                transaction.Commit();
                return model;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(long transactionId)
        {
            if (transactionId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                TransactionDB item = db.Transaction.Include(x => x.TransactionDetail)
                    .FirstOrDefault(x => x.Id == transactionId);
                
                if (item == null)
                    throw new Exception("Registro no reconocido");

                db.TransactionDetail.RemoveRange(item.TransactionDetail);
                db.Transaction.Remove(item);
                db.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool VerifyDetail(long transactionId)
        {
            if (transactionId == 0)
                throw new Exception("No se reconoce este registro");

            using Database db = new Database(this.connectionString);

            TransactionDB item = db.Transaction
                .Include(x => x.TransactionDetail)
                .FirstOrDefault(x=>x.Id == transactionId);

            if (item == null)
                return false;

            return item.TransactionDetail.Count > 0;
        }

        #region Privates
        private void Validate(TransactionModel model, DatabaseEvent dbEvent)
        {
            if (dbEvent == DatabaseEvent.Select)
            {
                if (model.id == 0 || model.id.IsNull())
                    throw new Exception("La aplicación no registra información con identificador 0");
            }
            else
            {
                if (dbEvent == DatabaseEvent.Update || dbEvent == DatabaseEvent.Delete)
                {
                    if (model.id == 0 || model.id.IsNull())
                        throw new Exception("Es necesario el identificador de registro para operar la transacción");
                }

                if (dbEvent != DatabaseEvent.Delete)
                {
                    if (model == null)
                        throw new Exception("No existe información para insertar");

                    if (model.vehicle == null)
                        throw new Exception("Es necesario seleccionar un vehículo para operar la transacción");

                    if (model.service == null && model.expense == null)
                        throw new Exception("No se especificó un tipo de movimiento para la transacción");

                    using Database db = new Database(this.connectionString);
                    if (dbEvent != DatabaseEvent.Insert)
                    {
                        if (!db.Transaction.Any(x => x.Id == model.id))
                            throw new Exception("Registro no reconocido");
                    }

                    short? service = model.service?.id;
                    short? expense = model.expense?.id;
                    byte? vehicle = model.vehicle?.id;
                    if (dbEvent == DatabaseEvent.Insert)
                    {
                        if (db.Transaction.Any(x=>
                            x.Service == service &&
                            x.Expense == expense &&
                            x.Vehicle == vehicle &&
                            x.TransactionDate == model.transactionDate))
                            throw new Exception("Ya existe un registro con estos datos");
                    }

                    if (dbEvent == DatabaseEvent.Update)
                    {
                        if (db.Transaction.Any(x =>
                            x.Service == service &&
                            x.Expense == expense &&
                            x.Vehicle == vehicle &&
                            x.TransactionDate == model.transactionDate &&
                            x.Id != model.id))
                            throw new Exception("Ya existe un registro con estos datos");
                    }
                }
            }
        }
        #endregion
    }
}
