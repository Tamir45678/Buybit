import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandpageComponent } from './landpage/landpage.component';
import { MarketplaceComponent } from './market/marketplace/marketplace.component';
import { OrdersComponent } from './order/orders/orders.component';

const routes: Routes = [
  {path:"",component:LandpageComponent},
  {path:"Orders",component:OrdersComponent},
  {path:"Marketplace",component:MarketplaceComponent},
  {path:"",component:LandpageComponent,pathMatch:'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
