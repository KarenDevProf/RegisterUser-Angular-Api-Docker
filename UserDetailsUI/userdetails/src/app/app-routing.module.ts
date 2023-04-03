import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddUserComponent } from './components/add-user/add-user.component';
import { AddUserFinishComponent } from './components/add-user-finish/add-user-finish.component';

const routes: Routes = [
  { path: '', component: AddUserComponent },
  { path: 'register-step2', component: AddUserFinishComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
