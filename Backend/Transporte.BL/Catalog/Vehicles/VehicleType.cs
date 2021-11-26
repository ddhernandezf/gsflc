using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using VehicleTypeModel = Transporte.Model.Catalog.Vehicles.VehicleType;
using VehicleTypeDB = Transporte.DAL.Models.VehicleType;

namespace Transporte.BL.Catalog.Vehicles
{
    public class VehicleType : LogicBase
    {
        private string connectionString { get; }

        public VehicleType(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public VehicleType(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<VehicleTypeModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.VehicleType.ToList().ToModel();
        }

        public VehicleTypeModel Add(VehicleTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.VehicleType.Any(x => x.Name == model.name))
                throw new Exception($"Ya existe un tipo nombrado '{model.name}'");

            VehicleTypeDB item = model.ToDbModel();
            db.VehicleType.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public VehicleTypeModel Update(VehicleTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.VehicleType.Any(x => x.Name == model.name && x.Id != model.id))
                throw new Exception($"Ya existe un tipo nombrado '{model.name}'");

            db.VehicleType.Update(model.ToDbModel());
            db.SaveChanges();

            return model;
        }

        public void Delete(byte serviceId)
        {
            if (serviceId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            VehicleTypeDB item = db.VehicleType
                .Include(x => x.Brand)
                .Include(x => x.Vehicle)
                .FirstOrDefault(x => x.Id == serviceId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Brand.Count > 0)
                throw new Exception("Este tipo cuenta con marcas relacionados");

            if (item.Vehicle.Count > 0)
                throw new Exception("Este tipo cuenta con vehículos relacionados");

            db.VehicleType.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(VehicleTypeModel model, DatabaseEvent dbEvent)
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
