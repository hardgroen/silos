import { Component, OnInit, ViewChild } from '@angular/core';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { SiloService } from '../../services/silo.service';
import { Silo } from '../../models/Silo';
import { GetSilosRequest } from '../../models/requests/GetSilosRequest';
import { firstValueFrom, of } from 'rxjs';

@Component({
  selector: 'app-silo-selection',
  templateUrl: './silo-selection.component.html',
  styleUrls: ['./silo-selection.component.scss'],
})
export class SiloSelectionComponent implements OnInit {
  userId!: string;
  silos: Silo[] = [];
  faPlusCircle = faPlusCircle;

  constructor(
    public loaderService: LoaderService,
    private siloService: SiloService,
    private localStorageService: LocalStorageService
  ) {}

  async ngOnInit() {
    await this.loadSilos();
  }

  get isLoading() {
    return this.loaderService.loading$;
  }

  async loadSilos() {
    await firstValueFrom(
      this.siloService.getSilos(
        new GetSilosRequest()
      )
    ).then((result) => {
      this.silos = result.data;
    });
  }
}
