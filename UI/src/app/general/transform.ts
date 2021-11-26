import {VehicleGroupedModel} from '../model/api/catalog/vehicle/vehicleGroupedModel';
import {SelectBoxCategoryModel} from '../model/template/selectBoxCategoryModel';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import {VehicleModel} from '../model/api/catalog/vehicle/vehicleModel';
import {ServiceGroupedModel} from '../model/api/catalog/service/serviceGroupedModel';
import {ExpenseGroupedModel} from '../model/api/catalog/expense/expenseGroupedModel';
import {ServiceModel} from '../model/api/catalog/service/serviceModel';
import {ExpenseModel} from '../model/api/catalog/expense/expenseModel';
import {BrandGroupedModel} from '../model/api/catalog/vehicle/brandGroupedModel';
import {BrandModel} from '../model/api/catalog/vehicle/brandModel';

export class Transform {
  public static VehicleGroupedToCategoryModel(data: VehicleGroupedModel[]): DataSource {
    const items: SelectBoxCategoryModel[] = this.InitCategoryModel();

    data.forEach(x => {
      x.vehicles.forEach(y => {
        const item: SelectBoxCategoryModel = {
          id: y.id,
          name: y.text,
          Category: x.name
        };
        items.push(item);
      });
    });

    const result: DataSource = new DataSource({
      store: new ArrayStore({
        data: items,
        key: 'id'
      }),
      group: 'Category'
    });

    return result;
  }

  public static ServiceGroupedToCategoryModel(data: ServiceGroupedModel[]): DataSource {
    const items: SelectBoxCategoryModel[] = this.InitCategoryModel();

    data.forEach(x => {
      x.services.forEach(y => {
        const item: SelectBoxCategoryModel = {
          id: y.id,
          name: y.name,
          Category: x.name
        };
        items.push(item);
      });
    });

    const result: DataSource = new DataSource({
      store: new ArrayStore({
        data: items,
        key: 'id'
      }),
      group: 'Category'
    });

    return result;
  }

  public static ExpenseGroupedToCategoryModel(data: ExpenseGroupedModel[]): DataSource {
    const items: SelectBoxCategoryModel[] = this.InitCategoryModel();

    data.forEach(x => {
      x.expenses.forEach(y => {
        const item: SelectBoxCategoryModel = {
          id: y.id,
          name: y.name,
          Category: x.name
        };
        items.push(item);
      });
    });

    const result: DataSource = new DataSource({
      store: new ArrayStore({
        data: items,
        key: 'id'
      }),
      group: 'Category'
    });

    return result;
  }

  public static VehicleGroupedToVehicleModel(data: VehicleGroupedModel[]): VehicleModel[] {
    const result: VehicleModel[] = [];

    data.forEach(x => {
      x.vehicles.forEach(y => {
        result.push(y);
      });
    });

    return result;
  }

  public static ServiceGroupedToServiceModel(data: ServiceGroupedModel[]): ServiceModel[] {
    const result: ServiceModel[] = [];

    data.forEach(x => {
      x.services.forEach(y => {
        result.push(y);
      });
    });

    return result;
  }

  public static ExpenseGroupedToExpenseModel(data: ExpenseGroupedModel[]): ExpenseModel[] {
    const result: ExpenseModel[] = [];

    data.forEach(x => {
      x.expenses.forEach(y => {
        result.push(y);
      });
    });

    return result;
  }

  public static BrandGroupedToCategoryModel(data: BrandGroupedModel[]): DataSource {
    const items: SelectBoxCategoryModel[] = this.InitCategoryModel();

    data.forEach(x => {
      x.brands.forEach(y => {
        const item: SelectBoxCategoryModel = {
          id: y.id,
          name: y.name,
          Category: x.name
        };
        items.push(item);
      });
    });

    const result: DataSource = new DataSource({
      store: new ArrayStore({
        data: items,
        key: 'id'
      }),
      group: 'Category'
    });

    return result;
  }

  public static BrandGroupedToBrandModel(data: BrandGroupedModel[]): BrandModel[] {
    const result: BrandModel[] = [];

    data.forEach(x => {
      x.brands.forEach(y => {
        result.push(y);
      });
    });

    return result;
  }

  private static InitCategoryModel(): SelectBoxCategoryModel[] {
    const items: SelectBoxCategoryModel[] = [];
    return items;
  }
}
