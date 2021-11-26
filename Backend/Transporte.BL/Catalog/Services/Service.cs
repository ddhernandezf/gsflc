using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using ServiceModel = Transporte.Model.Catalog.Services.Service;
using ServiceGroupedModel = Transporte.Model.Catalog.Services.ServiceGrouped;
using ServiceDB = Transporte.DAL.Models.Service;

namespace Transporte.BL.Catalog.Services
{
    public class Service : LogicBase
    {
        private string connectionString { get; }

        public Service(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Service(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<ServiceModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.Service.Include(x=>x.TypeNavigation).ToList().ToModel();
        }

        public List<ServiceGroupedModel> GetGrouped()
        {
            using Database db = new Database(this.connectionString);

            return db.ServiceType.Include(x => x.Service)
                .Where(x=>x.Service.Count > 0)
                .ToList()
                .ToGroupedModel();
        }

        public ServiceModel Add(ServiceModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Service.Any(x =>
                x.Type == model.serviceType.id &&
                x.Name == model.name))
                throw new Exception($"Ya existe un servicio nombrado '{model.name}'");

            ServiceDB item = model.ToDbModel();
            item.TypeNavigation = null;
            db.Service.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public ServiceModel Update(ServiceModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Service.Any(x =>
                x.Type == model.serviceType.id &&
                x.Name == model.name &&
                x.Id != model.id))
                throw new Exception($"Ya existe un servicio nombrado '{model.name}'");

            ServiceDB item = model.ToDbModel();
            item.TypeNavigation = null;
            db.Service.Update(item);
            db.SaveChanges();

            return model;
        }

        public void Delete(short serviceId)
        {
            if (serviceId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            ServiceDB item = db.Service
                .Include(x => x.Transaction)
                .FirstOrDefault(x => x.Id == serviceId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Transaction.Count > 0)
                throw new Exception("Este servicio cuenta con transacciones");

            db.Service.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(ServiceModel model, DatabaseEvent dbEvent)
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

                    if (model.serviceType == null || model.serviceType.id.IsNull())
                        throw new Exception("El tipo es requerido");
                }
            }
        }
        #endregion
    }
}
