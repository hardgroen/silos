import { firstValueFrom, Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { appConstants } from 'src/app/features/recordings/constants/appConstants';
import { UserService } from 'src/app/features/recordings/services/user.service';
import { User } from 'src/app/features/recordings/models/User';
import {
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import {
  faList,
  faShoppingBasket,
  faShoppingCart,
  faSignOutAlt,
  faUser,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss'],
})
export class NavMenuComponent implements OnInit, OnDestroy {
  @ViewChild('storedEventViewerContainer', { read: ViewContainerRef })
  storedEventViewerContainer!: ViewContainerRef;

  faList = faList;
  faUser = faUser;
  faSignOutAlt = faSignOutAlt;
  faShoppingCart = faShoppingCart;
  isExpanded = false;
  isModalOpen = false;
  isLoggedIn = false;
  subscription!: Subscription;
  user!: User;

  constructor(
    private cdr: ChangeDetectorRef,
    private authService: AuthService,
    private userService: UserService,
    private tokenStorageService: TokenStorageService,
    private localStorageService: LocalStorageService
  ) {}

  async ngOnInit() {
    this.subscription = this.authService.isLoggedAnnounced$.subscribe(
      async (response) => {
        this.isLoggedIn = response;
        if (this.isLoggedIn) {
          await this.loadUserDetails();
        }
      }
    );
  }

  async ngAfterViewInit() {
    this.isLoggedIn = !!this.tokenStorageService.getToken();
    this.cdr.detectChanges();
  }

  get loadStoredUser() {
    return this.authService.currentUser;
  }

  logout() {
    this.localStorageService.clearAllKeys();
    this.authService.logout();
    this.isLoggedIn = !!this.tokenStorageService.getToken();
  }

  ngOnDestroy() {
    // prevent memory leak when component destroyed
    this.subscription.unsubscribe();
    this.isModalOpen = false;
  }

  private async storeLoadedUser() {
    // storing user in the localstorage
    this.localStorageService.setValue(
      appConstants.storedUser,
      JSON.stringify(this.user)
    );
  }

  private async loadUserDetails() {
    await firstValueFrom(this.userService.loadUserDetails()).then(
      (result) => {
        if (result.success) {
          var data = result.data;
          this.user = new User(
            data.id,
            data.name,
            data.email
          );
          this.storeLoadedUser();
        }
      }
    );
  }
}
