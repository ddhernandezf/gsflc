using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using Transporte.Model.Report;
using VehicleModel = Transporte.Model.Catalog.Vehicles.Vehicle;
using VehicleDB = Transporte.DAL.Models.Vehicle;
using VehicleTypeDB = Transporte.DAL.Models.VehicleType;
using VehicleGroupedModel = Transporte.Model.Catalog.Vehicles.VehicleGrouped;

namespace Transporte.BL.Catalog.Vehicles
{
    public class Vehicle : LogicBase
    {
        private string connectionString { get; }

        public Vehicle(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Vehicle(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<VehicleModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.Vehicle
                .Include(x => x.TypeNavigation)
                .Include(x => x.BrandNavigation)
                .Include(x => x.ModelNavigation)
                .Include(x => x.RegistrationTypeNavigation)
                .Where(x => x.Active == true)
                .ToList()
                .ToModel();
        }

        public List<VehicleGroupedModel> GetGrouped()
        {
            using Database db = new Database(this.connectionString);

            return db.VehicleType
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.TypeNavigation)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.BrandNavigation)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.ModelNavigation)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.RegistrationTypeNavigation)
                .Include(x => x.Vehicle)
                .Where(x => x.Vehicle.FirstOrDefault().Active == true)
                .ToList()
                .ToVehicleGrouped();
        }

        public List<VehicleGroupedModel> GetGrouped(BalanceOptions option)
        {
            using Database db = new Database(this.connectionString);

            IQueryable<VehicleTypeDB> data = db.VehicleType
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.TypeNavigation)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.BrandNavigation)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.ModelNavigation)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.RegistrationTypeNavigation)
                .Include(x => x.Vehicle)
                .Where(x => x.Vehicle.FirstOrDefault().Active == true);

            switch (option)
            {
                case BalanceOptions.EXPENSE:
                    data = data.Where(x => x.CanExpense == true);
                    break;
                case BalanceOptions.SERVICE:
                    data = data.Where(x => x.CanService == true);
                    break;
            }

            return data.ToList().ToVehicleGrouped();
        }

        public VehicleModel Add(VehicleModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Vehicle.Any(x =>
                x.RegistrationType == model.registrationType.id &&
                x.Registration == model.registration))
                throw new Exception($"Ya existe un vehículo con esta matricula '{model.registrationType.id}-{model.registration}'");

            VehicleDB item = model.ToDbModel();
            item.TypeNavigation = null;
            item.RegistrationTypeNavigation = null;
            item.BrandNavigation = null;
            item.ModelNavigation = null;
            
            db.Vehicle.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public VehicleModel Update(VehicleModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Vehicle.Any(x =>
                x.RegistrationType == model.registrationType.id &&
                x.Registration == model.registration &&
                x.Id != model.id))
                throw new Exception($"Ya existe un vehículo con esta matricula '{model.registrationType.id}-{model.registration}'");

            VehicleDB item = model.ToDbModel();

            item.TypeNavigation = null;
            item.RegistrationTypeNavigation = null;
            item.BrandNavigation = null;
            item.ModelNavigation = null;
            
            db.Vehicle.Update(item);
            db.SaveChanges();

            return model;
        }

        public void Delete(byte vehicleId)
        {
            if (vehicleId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            VehicleDB item = db.Vehicle
                .Include(x => x.Transaction)
                .FirstOrDefault(x => x.Id == vehicleId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            if (item.Transaction.Count > 0)
                throw new Exception("Este vehiculo cuenta con transacciones");

            db.Vehicle.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(VehicleModel model, DatabaseEvent dbEvent)
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
                    if (model.registrationType == null || model.registrationType.id.IsNull())
                        throw new Exception("El tipo de matricula es requerido");

                    if (model.registration.IsNull())
                        throw new Exception("La matricula es requerida");

                    if (model.brand == null || model.brand.id.IsNull())
                        throw new Exception("La marca es requerida");

                    if (model.brandModel == null || model.brandModel.id.IsNull())
                        throw new Exception("El modelo es requerido");

                    if (model.year == 0 || model.year.ToString().Length != 4)
                        throw new Exception("El formato de año no es reconocido");
                }
            }
        }
        #endregion
    }
}
