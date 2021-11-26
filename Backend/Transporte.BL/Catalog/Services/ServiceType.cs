using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using ServiceTypeModel = Transporte.Model.Catalog.Services.ServiceType;
using ServiceTypeDB = Transporte.DAL.Models.ServiceType;

namespace Transporte.BL.Catalog.Services
{
    public class ServiceType : LogicBase
    {
        private string connectionString { get; }

        public ServiceType(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ServiceType(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<ServiceTypeModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.ServiceType.ToList().ToModel();
        }

        public ServiceTypeModel Add(ServiceTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.ServiceType.Any(x => x.Name == model.name))
                throw new Exception($"Ya existe un servicio nombrado '{model.name}'");

            ServiceTypeDB item = model.ToDbModel();
            db.ServiceType.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public ServiceTypeModel Update(ServiceTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.ServiceType.Any(x => x.Name == model.name && x.Id != model.id))
                throw new Exception($"Ya existe un servicio nombrado '{model.name}'");

            db.ServiceType.Update(model.ToDbModel());
            db.SaveChanges();

            return model;
        }

        public void Delete(byte serviceId)
        {
            if (serviceId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            ServiceTypeDB item = db.ServiceType
                .Include(x=>x.Service)
                .FirstOrDefault(x => x.Id == serviceId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Service.Count > 0)
                throw new Exception("Este tipo cuenta con servicios relacionados");

            db.ServiceType.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(ServiceTypeModel model, DatabaseEvent dbEvent)
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
