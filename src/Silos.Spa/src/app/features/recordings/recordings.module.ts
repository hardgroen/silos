import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { RouterModule } from '@angular/router';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { SiloSelectionComponent } from './components/silo-selection/silo-selection.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SortPipe } from 'src/app/core/pipes/sort.pipe';

@NgModule({
  declarations: [
    HomeComponent,
    SiloSelectionComponent,
    UserDetailsComponent,
    SortPipe
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    NgbDatepickerModule
  ],
  providers: [],
})
export class RecordingsModule {}
