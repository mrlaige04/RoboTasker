import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import {providePrimeNG} from 'primeng/config';
import {MyPreset} from '../mytheme';
import {apiConfigProvider} from './config/http.config';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {passTokenInterceptor} from './utils/interceptors/pass-token.interceptor';
import {ConfirmationService, MessageService} from 'primeng/api';
import {handleUnauthorizedInterceptor} from './utils/interceptors/handle-unauthorized.interceptor';
import {handleServerNotRespondingInterceptor} from './utils/interceptors/handle-server-not-responding.interceptor';
import {DialogService} from 'primeng/dynamicdialog';
import {provideEchartsCore} from 'ngx-echarts';
import * as echarts from 'echarts/core';
import {CanvasRenderer} from 'echarts/renderers';
import {GridComponent, LegendComponent, TooltipComponent} from 'echarts/components';
import {BarChart, PieChart} from 'echarts/charts';
import {registerTheme} from 'echarts/core';

echarts.use([GridComponent, CanvasRenderer, TooltipComponent, LegendComponent]);
echarts.use([BarChart, PieChart]);

registerTheme('dark', {
  backgroundColor: 'transparent',
  legend: { textStyle: { color: '#fff' } },
  grid: {
    left: '40px',
    top: '40px',
    right: '40px',
    bottom: '40px',
  }
});

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: MyPreset,
        options: {
          darkModeSelector: '.dark-theme'
        }
      },
      ripple: true
    }),
    apiConfigProvider,
    provideHttpClient(withInterceptors([
      passTokenInterceptor,
      handleServerNotRespondingInterceptor,
      handleUnauthorizedInterceptor
    ])),
    DialogService,
    MessageService,
    ConfirmationService,
    provideEchartsCore({ echarts })
  ]
};
