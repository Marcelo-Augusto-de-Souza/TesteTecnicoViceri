import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

import { AppComponent } from './app.component';
import { TelaPrincipalComponent } from './components/tela-principal/tela-principal.component';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    // Stand-alone components imported here so templates work
    TelaPrincipalComponent,
    AppComponent,
  ],
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
