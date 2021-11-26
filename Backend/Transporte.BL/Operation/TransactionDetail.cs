using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using TransactionDetailModel = Transporte.Model.Operation.TransactionDetail;
using TransactionDetailDB = Transporte.DAL.Models.TransactionDetail;
using TransactionDB = Transporte.DAL.Models.Transaction;
using Microsoft.EntityFrameworkCore.Storage;
using Database = Transporte.DAL.Database;

namespace Transporte.BL.Operation
{
    public class TransactionDetail : LogicBase
    {
        private string connectionString { get; }
        
        public TransactionDetail(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TransactionDetail(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<TransactionDetailModel> Get(long transactionId)
        {
            if (transactionId == 0)
                throw new Exception("Transacción no reconocida");

            using Database db = new Database(this.connectionString);

            return db.TransactionDetail
                .Where(x =>
                    x.Transaction == transactionId)
                .ToList()
                .ToModel();
        }

        public TransactionDetailModel Add(TransactionDetailModel model)
        {
            using Database db = new Database(this.connectionString);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                Validate(model, DatabaseEvent.Insert);

                TransactionDetailDB item = model.ToDbModel();
                db.TransactionDetail.Add(item);
                db.SaveChanges();
                model.id = item.Id;
                UpdateTotal(db, item.Transaction);

                transaction.Commit();
                return model;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public TransactionDetailModel Update(TransactionDetailModel model)
        {
            using Database db = new Database(this.connectionString);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                Validate(model, DatabaseEvent.Update);

                db.TransactionDetail.Update(model.ToDbModel());
                db.SaveChanges();
                UpdateTotal(db, model.transaction);

                transaction.Commit();
                return model;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(long detailId)
        {
            if (detailId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                TransactionDetailDB item = db.TransactionDetail
                    .FirstOrDefault(x => x.Id == detailId);

                if (item == null)
                    throw new Exception("Registro no reconocido");

                db.TransactionDetail.Remove(item);
                db.SaveChanges();
                UpdateTotal(db, item.Transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        #region Privates
        private void Validate(TransactionDetailModel model, DatabaseEvent dbEvent)
        {
            if (dbEvent == DatabaseEvent.Select)
            {
                if (model.id == 0)
                    throw new Exception("La aplicación no registra información con identificador 0");
            }
            else
            {
                if (dbEvent == DatabaseEvent.Update || dbEvent == DatabaseEvent.Delete)
                {
                    if (model.id == 0)
                        throw new Exception("Es necesario el identificador de registro para operar la transacción");
                }

                if (dbEvent != DatabaseEvent.Delete)
                {
                    if (model == null)
                        throw new Exception("No existe información para insertar");

                    if (string.IsNullOrEmpty(model.description) || string.IsNullOrWhiteSpace(model.description))
                        throw new Exception("Debe especificar un detalle para la transacción");

                    if (model.quantity <= 0)
                        throw new Exception("La cantidad no puede ser menor o igual a cero");
                    
                    if (model.unitPrice <= 0)
                        throw new Exception("El precio no puede ser menor o igual a cero");
                }
            }
        }

        public void UpdateTotal(Database db, long headerId)
        {
            TransactionDB item = db.Transaction.Include(x => x.TransactionDetail)
                .FirstOrDefault(x => x.Id == headerId);
            item.Total = item.TransactionDetail.Sum(x => x.TotalPrice);
            db.Transaction.Update(item);
            db.SaveChanges();
        }
        #endregion
    }
}
