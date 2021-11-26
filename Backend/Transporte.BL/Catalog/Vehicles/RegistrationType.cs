using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using RegistrationTypeModel = Transporte.Model.Catalog.Vehicles.RegistrationType;
using RegistrationTypeDB = Transporte.DAL.Models.RegistrationType;

namespace Transporte.BL.Catalog.Vehicles
{
    public class RegistrationType : LogicBase
    {
        private string connectionString { get; }

        public RegistrationType(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public RegistrationType(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<RegistrationTypeModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.RegistrationType.ToList().ToModel();
        }

        public RegistrationTypeModel Add(RegistrationTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            RegistrationTypeDB item = model.ToDbModel();
            db.RegistrationType.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public RegistrationTypeModel Update(RegistrationTypeModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            db.RegistrationType.Update(model.ToDbModel());
            db.SaveChanges();
            
            return model;
        }

        public void Delete(string typeId)
        {
            if (typeId.IsNull())
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            RegistrationTypeDB item = db.RegistrationType
                .Include(x => x.Vehicle)
                .FirstOrDefault(x => x.Id == typeId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            db.RegistrationType.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(RegistrationTypeModel model, DatabaseEvent dbEvent)
        {
            if (dbEvent != DatabaseEvent.Select)
            {
                if (model.id.IsNull())
                    throw new Exception("El identificador es requerido");

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
