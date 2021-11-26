import { InjectionToken, FactoryProvider } from '@angular/core';

export const appWindow = new InjectionToken<Window>('window');

const windowProvider: FactoryProvider = {
  provide: appWindow,
  useFactory: () => window
};

export const WindowProviders = [
  windowProvider
];
