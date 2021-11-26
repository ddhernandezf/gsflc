using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using BrandModelModel = Transporte.Model.Catalog.Vehicles.BrandModel;
using BrandModelGroupedModel = Transporte.Model.Catalog.Vehicles.BrandModelGrouped;
using BrandModelDB = Transporte.DAL.Models.BrandModel;

namespace Transporte.BL.Catalog.Vehicles
{
    public class BrandModel : LogicBase
    {
        private string connectionString { get; }

        public BrandModel(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public BrandModel(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<BrandModelModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.BrandModel
                .Include(x=>x.BrandNavigation)
                .Include(x=> x.BrandNavigation.TypeNavigation)
                .ToList()
                .ToModel();
        }

        public List<BrandModelGroupedModel> GetGrouped()
        {
            using Database db = new Database(this.connectionString);

            return db.Brand
                .Include(x => x.BrandModel)
                .Include(x => x.TypeNavigation)
                .Where(x=>x.BrandModel.Count > 0)
                .ToList()
                .ToGroupedModel();
        }

        public BrandModelModel Add(BrandModelModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.BrandModel.Any(x =>
                x.Brand == model.brand.id &&
                x.Name == model.name))
                throw new Exception($"Ya existe un modelo nombrado '{model.name}'");

            BrandModelDB item = model.ToDbModel();
            item.BrandNavigation = null;
            db.BrandModel.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public BrandModelModel Update(BrandModelModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.BrandModel.Any(x =>
                x.Brand == model.brand.id &&
                x.Name == model.name &&
                x.Id != model.id))
                throw new Exception($"Ya existe un modelo nombrado '{model.name}'");

            BrandModelDB item = model.ToDbModel();
            item.BrandNavigation = null;
            db.BrandModel.Update(item);
            db.SaveChanges();

            return model;
        }

        public void Delete(byte brandModelId)
        {
            if (brandModelId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            BrandModelDB item = db.BrandModel
                .Include(x => x.Vehicle)
                .FirstOrDefault(x => x.Id == brandModelId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Vehicle.Count > 0)
                throw new Exception("Este modelo cuenta con vehículos relacionados");

            db.BrandModel.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(BrandModelModel model, DatabaseEvent dbEvent)
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

                    if (model.brand == null)
                        throw new Exception("La marca del modelo es requerida");
                }
            }
        }
        #endregion
    }
}
