using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using BrandMdl = Transporte.Model.Catalog.Vehicles.Brand;
using BrandDB = Transporte.DAL.Models.Brand;
using BrandGroupedModel = Transporte.Model.Catalog.Vehicles.BrandGrouped;

namespace Transporte.BL.Catalog.Vehicles
{
    public class Brand : LogicBase
    {
        private string connectionString { get; }

        public Brand(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Brand(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<BrandMdl> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.Brand.Include(x=>x.TypeNavigation).ToList().ToModel();
        }

        public List<BrandGroupedModel> GetGrouped()
        {
            using Database db = new Database(this.connectionString);

            return db.VehicleType.Include(x => x.Brand)
                .Where(x=>x.Brand.Count > 0)
                .ToList()
                .ToGroupedModel();
        }

        public BrandMdl Add(BrandMdl model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Brand.Any(x =>
                x.Type == model.vehicleType.id &&
                x.Name == model.name))
                throw new Exception($"Ya existe una marca nombrada '{model.name}'");

            BrandDB item = model.ToDbModel();
            item.TypeNavigation = null;
            db.Brand.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public BrandMdl Update(BrandMdl model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Brand.Any(x =>
                x.Type == model.vehicleType.id &&
                x.Name == model.name &&
                x.Id != model.id))
                throw new Exception($"Ya existe una marca nombrada '{model.name}'");

            BrandDB item = model.ToDbModel();
            item.TypeNavigation = null;
            db.Brand.Update(item);
            db.SaveChanges();

            return model;
        }

        public void Delete(byte brandId)
        {
            if (brandId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            BrandDB item = db.Brand
                .Include(x => x.Vehicle)
                .Include(x => x.BrandModel)
                .FirstOrDefault(x => x.Id == brandId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Vehicle.Count > 0)
                throw new Exception("Esta marca cuenta con vehículos relacionados");

            if (item.BrandModel.Count > 0)
                throw new Exception("Esta marca cuenta con modelos relacionados");

            db.Brand.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(BrandMdl model, DatabaseEvent dbEvent)
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

                    if (model.vehicleType == null)
                        throw new Exception("El tipo de vehículo es requerido");
                }
            }
        }
        #endregion
    }
}
