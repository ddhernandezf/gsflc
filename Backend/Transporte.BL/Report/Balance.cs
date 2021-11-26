using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transporte.Model.Report;
using Database = Transporte.DAL.Database;
using GeneralBL = Transporte.BL.General;
using TransactionDetailDB = Transporte.DAL.Models.TransactionDetail;
using BalanceModel = Transporte.Model.Report.Balance;
using ItemModel = Transporte.Model.Report.Item;
using TransactionTypeModel = Transporte.Model.Operation.TransactionType;

namespace Transporte.BL.Report
{
    public class Balance : LogicBase
    {
        private string connectionString { get; }
        private GeneralBL blGeneral { get; }

        public Balance(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Balance(Configuration settings)
        {
            configuration = settings;
            this.connectionString = configuration.connectionString.transport;
            this.blGeneral = new GeneralBL();
        }

        public BalanceModel General(BalanceFilter filter, BalanceOptions option)
        {
            ValidateBalanceFilter(filter);
            BalanceModel result = new BalanceModel();
            List<TransactionTypeModel> tranType = blGeneral.GetTransactionType();

            using Database db = new Database(this.connectionString);

            IQueryable<TransactionDetailDB> query = db.TransactionDetail
                .Include(x => x.TransactionNavigation)
                .ThenInclude(x => x.VehicleNavigation)
                .ThenInclude(x => x.RegistrationTypeNavigation)
                .Include(x => x.TransactionNavigation)
                .ThenInclude(x => x.VehicleNavigation)
                .ThenInclude(x => x.BrandNavigation)
                .Include(x => x.TransactionNavigation)
                .ThenInclude(x => x.VehicleNavigation)
                .ThenInclude(x => x.ModelNavigation)
                .Include(x => x.TransactionNavigation)
                .ThenInclude(x => x.ServiceNavigation)
                .ThenInclude(x => x.TypeNavigation)
                .Include(x => x.TransactionNavigation)
                .ThenInclude(x => x.ExpenseNavigation)
                .ThenInclude(x => x.TypeNavigation)
                .Where(x =>
                    filter.vehicles.Contains(x.TransactionNavigation.Vehicle.ToString())
                );

            if (filter.monthAndYear)
                query = query
                    .Where(x =>
                        x.TransactionNavigation.TransactionDate.Month == filter.month &&
                        x.TransactionNavigation.TransactionDate.Year == filter.year);
            else
            {
                if (filter.dateRange)
                    query = query
                        .Where(x =>
                            x.TransactionNavigation.TransactionDate >= filter.startDate &&
                            x.TransactionNavigation.TransactionDate <= filter.endDate);
                else
                    query = query
                        .Where(x =>
                            x.TransactionNavigation.TransactionDate == filter.startDate);
            }

            IQueryable<TransactionDetailDB> services = query
                .Where(x =>
                    filter.services.Contains(x.TransactionNavigation.Service.ToString()));

            IQueryable<TransactionDetailDB> expenses = query
                .Where(x =>
                    filter.expenses.Contains(x.TransactionNavigation.Expense.ToString()));

            List<TransactionDetailDB> preData = new List<TransactionDetailDB>();
            switch (option)
            {
                case BalanceOptions.BALANCE:
                    preData = services.Union(expenses).ToList();
                    break;
                case BalanceOptions.SERVICE:
                    preData = services.ToList();
                    break;
                case BalanceOptions.EXPENSE:
                    preData = expenses.ToList();
                    break;
            }

            result.data = preData
                .Select(x => new ItemModel()
                {
                    id = x.Id,
                    transaction = new Header()
                    {
                        id = x.TransactionNavigation.Id,
                        vehicle = x.TransactionNavigation.VehicleNavigation.ToModel(),
                        type = tranType.FirstOrDefault(y => y.id == (x.TransactionNavigation.Service.IsNull() ? 2 : 1)),
                        option = x.TransactionNavigation.Service.IsNull()
                            ? x.TransactionNavigation.ExpenseNavigation.ToReportModel()
                            : x.TransactionNavigation.ServiceNavigation.ToReportModel(),
                        registerDate = x.TransactionNavigation.RegisterDate,
                        transactionDate = x.TransactionNavigation.TransactionDate,
                        total = x.TransactionNavigation.Total ?? 0,
                    },
                    quantity = x.Quantity,
                    description = x.Description,
                    unitPrice = x.UnitPrice,
                    totalPrice = x.TotalPrice
                })
                .ToList();
            result.serviceTotal = result.data.Where(x => x.transaction.type.id == 1).Sum(x => x.totalPrice);
            result.expenseTotal = result.data.Where(x => x.transaction.type.id == 2).Sum(x => x.totalPrice);

            result.resumeChart = new List<ResumeChart>()
            {
                new ResumeChart(){ description = "Servicios", amount = result.serviceTotal },
                new ResumeChart(){ description = "Gastos", amount = result.expenseTotal },
            };

            result.data = result.data.OrderBy(x => x.transaction.type.id).ThenBy(x => x.transaction.transactionDate)
                .ToList();

            return result;
        }

        #region Private

        private void ValidateBalanceFilter(BalanceFilter filter)
        {
            if (filter.vehicles == null || filter.vehicles.Count == 0)
                throw new Exception("Debe especificar los vehículos");

            if (filter.services == null || filter.services.Count == 0)
                throw new Exception("Debe especificar los servicios");

            if (filter.expenses == null || filter.expenses.Count == 0)
                throw new Exception("Debe especificar los gastos");

            //if (filter.dateRange && (filter.startDate.IsNull() || filter.endDate.IsNull()))
            //    throw new Exception("Los rangos de fecha no deben ser nulos");

            //if (filter.dateRange && filter.endDate < filter.startDate)
            //    throw new Exception("La fecha inicial no debe ser mayor que la fecha final");

            //if (!filter.dateRange && filter.startDate.IsNull())
            //    throw new Exception("Debe especificar la fecha");

            //if (filter.monthAndYear && (filter.month.IsNull() || filter.year.IsNull()))
            //    throw new Exception("Debe especificar el mes y el año");

            //if (filter.monthAndYear && !filter.month.IsNull() && !(filter.month > 0 && filter.month <= 12))
            //    throw new Exception("Formato de mes incorrecto");

            //if (filter.monthAndYear && !filter.year.IsNull() && filter.year.ToString().Length != 4)
            //    throw new Exception("Formato de año incorrecto");
        }
        #endregion
    }
}
