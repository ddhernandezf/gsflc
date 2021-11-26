using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.BL.Enum;
using Transporte.DAL;
using PilotModel = Transporte.Model.Catalog.Vehicles.Pilot;
using PilotDB = Transporte.DAL.Models.Pilot;

namespace Transporte.BL.Catalog.Vehicles
{
    public class Pilot : LogicBase
    {
        private string connectionString { get; }

        public Pilot(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Pilot(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
        }

        public List<PilotModel> Get()
        {
            using Database db = new Database(this.connectionString);

            return db.Pilot.ToList().ToModel();
        }

        public PilotModel Add(PilotModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Pilot.Any(x => x.CompleteName == model.completeName))
                throw new Exception($"Ya existe un piloto llamado '{model.completeName}'");

            PilotDB item = model.ToDbModel();
            db.Pilot.Add(item);
            db.SaveChanges();
            model.id = item.Id;

            return model;
        }

        public PilotModel Update(PilotModel model)
        {
            Validate(model, DatabaseEvent.Insert);
            using Database db = new Database(this.connectionString);

            if (db.Pilot.Any(x => x.CompleteName == model.completeName && x.Id != model.id))
                throw new Exception($"Ya existe un piloto llamado '{model.completeName}'");

            db.Pilot.Update(model.ToDbModel());
            db.SaveChanges();

            return model;
        }

        public void Delete(byte pilotId)
        {
            if (pilotId == 0)
                throw new Exception("Registro no reconocido");

            using Database db = new Database(this.connectionString);

            PilotDB item = db.Pilot
                .FirstOrDefault(x => x.Id == pilotId);

            if (item == null)
                throw new Exception("Registro no reconocido");

            db.Pilot.Remove(item);
            db.SaveChanges();
        }

        #region Privates
        private void Validate(PilotModel model, DatabaseEvent dbEvent)
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
                    if (model.completeName.IsNull())
                        throw new Exception("El nombre es requerido");
                }
            }
        }
        #endregion
    }
}
