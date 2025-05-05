import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app/app.routes';
import { importProvidersFrom } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    importProvidersFrom(CommonModule),
    importProvidersFrom(BrowserAnimationsModule),
    importProvidersFrom(FormsModule),
    provideToastr({
      positionClass: 'toast-bottom-left',
      toastClass: 'custom-toast bootstrap-toast'
    })
      ]
}).catch(err => console.error(err));
