import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { LoginFormComponent } from './login-form/login-form.component';
import { RegisterComponent } from './register/register.component';
import { LandpageComponent } from './landpage/landpage.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { MarketplaceComponent } from './market/marketplace/marketplace.component';
import { OrdersComponent } from './order/orders/orders.component';
import { MenuComponent } from './menu/menu.component';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MarketCardComponent } from './market/market-card/market-card.component';
import {MatTabsModule} from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';
import {MatBadgeModule} from '@angular/material/badge';
import {MatTableModule} from '@angular/material/table';
import { OrderPanelComponent } from './order/order-panel/order-panel.component';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { OrderPanelDetailsComponent } from './order/order-panel-details/order-panel-details.component';
import { AddProductModalComponent } from './market/add-product-modal/add-product-modal.component';
import {MatDialogModule} from '@angular/material/dialog';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    RegisterComponent,
    LandpageComponent,
    MarketplaceComponent,
    OrdersComponent,
    MenuComponent,
    MarketCardComponent,
    OrderPanelComponent,
    OrderPanelDetailsComponent,
    AddProductModalComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    MatGridListModule,
    MatCardModule,
    MatToolbarModule,
    MatTabsModule,
    MatIconModule,
    MatBadgeModule,
    MatTableModule,
    MatExpansionModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule
  ],
  providers: [DatePipe,
    {provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
