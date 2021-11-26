using System.Collections.Generic;
using System.Linq;
using Transporte.Model.Catalog.Vehicles;
using Transporte.Model.Catalog.Services;
using Transporte.Model.Catalog.Expenses;
using Transporte.Model.Security;
using Transporte.Model.Operation;
using RegistrationTypeDB = Transporte.DAL.Models.RegistrationType;
using ServiceTypeDB = Transporte.DAL.Models.ServiceType;
using ServiceDB = Transporte.DAL.Models.Service;
using ExpenseTypeDB = Transporte.DAL.Models.ExpenseType;
using ExpenseDB = Transporte.DAL.Models.Expense;
using VehicleTypeDB = Transporte.DAL.Models.VehicleType;
using BrandDB = Transporte.DAL.Models.Brand;
using BrandModelDB = Transporte.DAL.Models.BrandModel;
using PilotDB = Transporte.DAL.Models.Pilot;
using VehicleDB = Transporte.DAL.Models.Vehicle;
using TransactionDB = Transporte.DAL.Models.Transaction;
using TransactionDetailDB = Transporte.DAL.Models.TransactionDetail;
using UserDB = Transporte.DAL.Models.User;
using RoleDB = Transporte.DAL.Models.Role;
using System;
using System.Text;
using Transporte.Model.Report;

namespace Transporte.BL
{
    public static class Extension
    {
        #region ModelList
        public static List<RegistrationType> ToModel(this List<RegistrationTypeDB> data)
        {
            return data?.Select(x => new RegistrationType()
            {
                id = x.Id,
                name = x.Name
            }).ToList();
        }
        public static List<ServiceType> ToModel(this List<ServiceTypeDB> data)
        {
            return data?.Select(x => new ServiceType()
            {
                id = x.Id,
                name = x.Name
            }).ToList();
        }
        public static List<Service> ToModel(this List<ServiceDB> data)
        {
            return data?.Select(x => new Service()
            {
                id = x.Id,
                name = x.Name,
                serviceType = new ServiceType() { id = x.TypeNavigation.Id, name = x.TypeNavigation.Name }
            }).ToList();
        }
        public static List<ServiceGrouped> ToGroupedModel(this List<ServiceTypeDB> data)
        {
            return data?.Select(x => new ServiceGrouped()
            {
                id = x.Id,
                name = x.Name,
                services = ToModel(x.Service.ToList())
            }).ToList();
        }
        public static List<Expense> ToModel(this List<ExpenseDB> data)
        {
            return data?.Select(x => new Expense()
            {
                id = x.Id,
                name = x.Name,
                expenseType = new ExpenseType() { id = x.TypeNavigation.Id, name = x.TypeNavigation.Name }
            }).ToList();
        }
        public static List<ExpenseGrouped> ToGroupedModel(this List<ExpenseTypeDB> data)
        {
            return data?.Select(x => new ExpenseGrouped()
            {
                id = x.Id,
                name = x.Name,
                expenses = ToModel(x.Expense.ToList())
            }).ToList();
        }
        public static List<ExpenseType> ToModel(this List<ExpenseTypeDB> data)
        {
            return data?.Select(x => new ExpenseType()
            {
                id = x.Id,
                name = x.Name
            }).ToList();
        }
        public static List<VehicleType> ToModel(this List<VehicleTypeDB> data)
        {
            return data?.Select(x => new VehicleType()
            {
                id = x.Id,
                name = x.Name,
                canService = x.CanService,
                canExpense = x.CanExpense
            }).ToList();
        }
        public static List<Brand> ToModel(this List<BrandDB> data)
        {
            return data?.Select(x => new Brand()
            {
                id = x.Id,
                name = x.Name,
                vehicleType = new VehicleType() { id = x.TypeNavigation.Id, name = x.TypeNavigation.Name }
            }).ToList();
        }
        public static List<BrandGrouped> ToGroupedModel(this List<VehicleTypeDB> data)
        {
            return data?.Select(x => new BrandGrouped()
            {
                id = x.Id,
                name = x.Name,
                brands = ToModel(x.Brand.ToList())
            }).ToList();
        }
        public static List<BrandModel> ToModel(this List<BrandModelDB> data)
        {
            return data?.Select(x => new BrandModel()
            {
                id = x.Id,
                name = x.Name,
                brand = new Brand()
                {
                    id = x.BrandNavigation.Id,
                    name = x.BrandNavigation.Name,
                    vehicleType = new VehicleType()
                    {
                        id = x.BrandNavigation.TypeNavigation.Id,
                        name = x.BrandNavigation.TypeNavigation.Name
                    }
                }
            }).ToList();
        }
        public static List<BrandModelGrouped> ToGroupedModel(this List<BrandDB> data)
        {
            return data?.Select(x => new BrandModelGrouped()
            {
                id = x.Id,
                name = x.Name,
                models = ToModel(x.BrandModel.ToList())
            }).ToList();
        }
        public static List<Pilot> ToModel(this List<PilotDB> data)
        {
            return data?.Select(x => new Pilot()
            {
                id = x.Id,
                bornDate = x.BornDate,
                hiringDate = x.HiringDate,
                isMale = x?.IsMale ?? false,
                completeName = x.CompleteName
            }).ToList();
        }
        public static List<Vehicle> ToModel(this List<VehicleDB> data)
        {
            return data?.Select(x => new Vehicle()
            {
                id = x.Id,
                active = x.Active ?? false,
                year = x.Year,
                registration = x.Registration,
                vehicleType = x.TypeNavigation?.ToModel(),
                brand = x.BrandNavigation?.ToModel(),
                brandModel = x.ModelNavigation?.ToModel(),
                name = x.Name,
                registrationType = x.RegistrationTypeNavigation?.ToModel()
            }).ToList();
        }
        public static List<VehicleGrouped> ToVehicleGrouped(this List<VehicleTypeDB> data)
        {
            return data?.Select(x => new VehicleGrouped()
            {
                id = x.Id,
                name = x.Name,
                vehicles = ToModel(x.Vehicle.ToList())
            }).ToList();
        }
        public static List<Transaction> ToModel(this List<TransactionDB> data)
        {
            return data?.Select(x => new Transaction()
            {
                id = x.Id,
                registerDate = x.RegisterDate,
                transactionDate = x.TransactionDate,
                total = x.Total ?? 0,
                vehicle = x.VehicleNavigation.ToModel(),
                service = x.ServiceNavigation?.ToModel(),
                expense = x.ExpenseNavigation?.ToModel(),
                userId = x.UserNavigation?.Id ?? (byte)0,
                userName = x.UserNavigation?.Email
            }).ToList();
        }
        public static List<TransactionDetail> ToModel(this List<TransactionDetailDB> data)
        {
            return data?.Select(x => new TransactionDetail()
            {
                id = x.Id,
                quantity = x.Quantity,
                description = x.Description,
                unitPrice = x.UnitPrice,
                totalPrice = x.TotalPrice,
                transaction = x.Transaction
            }).ToList();
        }
        public static List<UserInfo> ToModel(this List<UserDB> data)
        {
            return data?.Select(x => new UserInfo()
            {
                
                email = x.Email,
                name = x.CompleteName,
                role = x.RoleUser?.First().RoleNavigation?.ToModel(),
                reset = x.ResetPassword,
            }).ToList();
        }
        public static List<User> ToListModel(this List<UserDB> data)
        {
            return data?.Select(x => new User()
            {

                id = x.Id,
                email = x.Email,
                name = x.CompleteName,
                password = x.Password,
                role = x.RoleUser?.First().RoleNavigation?.ToModel(),
                reset = x.ResetPassword,
                active = x.Active ?? false
            }).ToList();
        }
        public static List<Role> ToModel(this List<RoleDB> data)
        {
            return data?.Select(x => new Role()
            {
                id = x.Id,
                name = x.Name,
            }).ToList();
        }
        #endregion

        #region Model
        public static Vehicle ToModel(this VehicleDB item)
        {
            return new Vehicle()
            {
                id = item.Id,
                active = item.Active ?? false,
                year = item.Year,
                registration = item.Registration,
                vehicleType = item.TypeNavigation?.ToModel(),
                brand = item.BrandNavigation?.ToModel(),
                brandModel = item.ModelNavigation?.ToModel(),
                registrationType = item.RegistrationTypeNavigation?.ToModel(),
                name = item.Name
            };
        }
        public static Pilot ToModel(this PilotDB item)
        {
            return new Pilot()
            {
                id = item.Id,
                bornDate = item.BornDate,
                completeName = item.CompleteName,
                isMale = item?.IsMale ?? false
            };
        }
        public static VehicleType ToModel(this VehicleTypeDB item)
        {
            return new VehicleType()
            {
                id = item.Id,
                name = item.Name,
                canExpense = item.CanExpense,
                canService = item.CanService
            };
        }
        public static Brand ToModel(this BrandDB item)
        {
            return new Brand()
            {
                id = item.Id,
                name = item.Name,
                vehicleType = item.TypeNavigation?.ToModel()
            };
        }
        public static BrandModel ToModel(this BrandModelDB item)
        {
            return new BrandModel()
            {
                id = item.Id,
                name = item.Name,
                brand = item.BrandNavigation?.ToModel()
            };
        }
        public static RegistrationType ToModel(this RegistrationTypeDB item)
        {
            return new RegistrationType()
            {
                id = item.Id,
                name = item.Name
            };
        }
        public static Service ToModel(this ServiceDB item)
        {
            return new Service()
            {
                id = item.Id,
                name = item.Name,
                serviceType = item.TypeNavigation?.ToModel()
            };
        }
        public static ServiceType ToModel(this ServiceTypeDB item)
        {
            return new ServiceType()
            {
                id = item.Id,
                name = item.Name
            };
        }
        public static Expense ToModel(this ExpenseDB item)
        {
            return new Expense()
            {
                id = item.Id,
                name = item.Name,
                expenseType = item.TypeNavigation?.ToModel()
            };
        }
        public static ExpenseType ToModel(this ExpenseTypeDB item)
        {
            return new ExpenseType()
            {
                id = item.Id,
                name = item.Name
            };
        }
        public static Movement ToModel(this Service item)
        {
            return new Movement()
            {
                id = item.id ?? 0,
                name = item.name,
                type = item.serviceType?.ToModel()
            };
        }
        public static MovementType ToModel(this ServiceType item)
        {
            return new MovementType()
            {
                id = item.id,
                name = item.name
            };
        }
        public static Movement ToModel(this Expense item)
        {
            return new Movement()
            {
                id = item.id ?? 0,
                name = item.name,
                type = item.expenseType?.ToModel()
            };
        }
        public static MovementType ToModel(this ExpenseType item)
        {
            return new MovementType()
            {
                id = item.id,
                name = item.name
            };
        }
        public static UserInfo ToModel(this UserDB data)
        {
            return new UserInfo()
            {
                email = data.Email,
                name = data.CompleteName,
                role = data.RoleUser.FirstOrDefault()?.RoleNavigation.ToModel(),
                reset = data.ResetPassword
            };
        }
        public static Role ToModel(this RoleDB data)
        {
            return new Role()
            {
                id = data.Id,
                name = data.Name
            };
        }
        public static Option ToReportModel(this ServiceDB item)
        {
            return new Option()
            {
                id = item.Id,
                name = item.Name,
                type = item.TypeNavigation?.ToReportModel()
            };
        }
        public static OptionType ToReportModel(this ServiceTypeDB item)
        {
            return new OptionType()
            {
                id = item.Id,
                name = item.Name
            };
        }
        public static Option ToReportModel(this ExpenseDB item)
        {
            return new Option()
            {
                id = item.Id,
                name = item.Name,
                type = item.TypeNavigation?.ToReportModel()
            };
        }
        public static OptionType ToReportModel(this ExpenseTypeDB item)
        {
            return new OptionType()
            {
                id = item.Id,
                name = item.Name
            };
        }
        #endregion

        #region DbModel
        public static TransactionDB ToDbModel(this Transaction item)
        {
            return new TransactionDB()
            {
                Id = item.id ?? 0,
                TransactionDate = item.transactionDate,
                RegisterDate = DateTime.Now,
                Expense = item.expense?.id,
                Service = item.service?.id,
                Vehicle = item.vehicle.id ?? 0,
                Total = item.total,
                User = item.userId
            };
        }
        public static TransactionDetailDB ToDbModel(this TransactionDetail item)
        {
            return new TransactionDetailDB()
            {
                Id = item.id ?? 0,
                Transaction = item.transaction,
                Description = item.description,
                Quantity = item.quantity,
                UnitPrice = item.unitPrice,
                TotalPrice = item.quantity == 0 || item.unitPrice == 0 ? 0 : (item.quantity * item.unitPrice),
            };
        }
        public static RegistrationTypeDB ToDbModel(this RegistrationType item)
        {
            return new RegistrationTypeDB()
            {
                Id = item.id.ToUpper(),
                Name = item.name
            };
        }
        public static ExpenseTypeDB ToDbModel(this ExpenseType item)
        {
            return new ExpenseTypeDB()
            {
                Id = item.id ?? 0,
                Name = item.name
            };
        }
        public static ServiceTypeDB ToDbModel(this ServiceType item)
        {
            return new ServiceTypeDB()
            {
                Id = item.id ?? 0,
                Name = item.name
            };
        }
        public static ExpenseDB ToDbModel(this Expense item)
        {
            return new ExpenseDB()
            {
                Id = item.id ?? 0,
                Name = item.name,
                Type = item.expenseType?.id ?? 0,
                TypeNavigation = item.expenseType.ToDbModel()
            };
        }
        public static ServiceDB ToDbModel(this Service item)
        {
            return new ServiceDB()
            {
                Id = item.id ?? 0,
                Name = item.name,
                Type = item.serviceType?.id ?? 0,
                TypeNavigation = item.serviceType.ToDbModel()
            };
        }
        public static VehicleTypeDB ToDbModel(this VehicleType item)
        {
            return new VehicleTypeDB()
            {
                Id = item.id ?? 0,
                Name = item.name,
                CanService = item.canService,
                CanExpense = item.canExpense
            };
        }
        public static BrandDB ToDbModel(this Brand item)
        {
            return new BrandDB()
            {
                Id = item.id ?? 0,
                Name = item.name,
                Type = item.vehicleType?.id ?? 0,
                TypeNavigation = item.vehicleType?.ToDbModel()
            };
        }
        public static BrandModelDB ToDbModel(this BrandModel item)
        {
            return new BrandModelDB()
            {
                Id = item.id ?? 0,
                Name = item.name,
                Brand = item.brand?.id ?? 0,
                Vehicle = null
            };
        }
        public static PilotDB ToDbModel(this Pilot item)
        {
            return new PilotDB()
            {
                Id = item.id ?? 0,
                BornDate = item.bornDate,
                HiringDate = item.hiringDate,
                IsMale = item.isMale,
                CompleteName = item.completeName
            };
        }
        public static VehicleDB ToDbModel(this Vehicle item)
        {
            return new VehicleDB()
            {
                Id = item.id ?? 0,
                Type = item.vehicleType?.id ?? 0,
                TypeNavigation = item.vehicleType?.ToDbModel(),
                RegistrationType = item.registrationType?.id,
                RegistrationTypeNavigation = item.registrationType?.ToDbModel(),
                Registration = item.registration.ToUpper(),
                Brand = item.brand?.id ?? 0,
                BrandNavigation = item.brand?.ToDbModel(),
                Model = item.brandModel?.id ?? 0,
                ModelNavigation = item.brandModel?.ToDbModel(),
                Year = item.year,
                Name = item.name,
                Active = item.active
            };
        }
        public static UserDB ToDbModel(this User item)
        {
            return new UserDB()
            {
                Id = item.id ?? 0,
                CompleteName = item.name,
                Password = item.password,
                Email = item.email,
                ResetPassword = item.reset,
                Active = item.active
            };
        }
        public static RoleDB ToDbModel(this Role item)
        {
            return new RoleDB()
            {
                Id = item.id ?? 0,
                Name = item.name,
            };
        }
        #endregion

        #region Common
        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
        public static bool IsNull(this int? value)
        {
            return value == null;
        }
        public static bool IsNull(this byte? value)
        {
            return value == null;
        }
        public static bool IsNull(this long? value)
        {
            return value == null;
        }
        public static bool IsNull(this DateTime? value)
        {
            return value == null;
        }
        public static bool IsNull(this short? value)
        {
            return value == null;
        }
        public static string ToMonth(this int value)
        {
            if (value < 1 || value > 12)
                throw new Exception("Formato incorrecto de mes");

            return value < 10 ? $"0{value.ToString()}" : value.ToString();
        }
        public static string CustomResponse(this Exception ex)
        {
            return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
        }
        public static string ToBase64Url(this string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_");
        }
        public static string FromBase64Url(this string value)
        {
            value = value.Replace("-", "+").Replace("_", "/");
            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.ASCII.GetString(bytes);
        }
        #endregion
    }
}
