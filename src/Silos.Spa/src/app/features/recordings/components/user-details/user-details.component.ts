import { firstValueFrom } from 'rxjs';
import { faList } from '@fortawesome/free-solid-svg-icons';
import { LoaderService } from 'src/app/core/services/loader.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { User } from '../../models/User';
import { UpdateUserRequest } from '../../models/requests/UpdateUserRequest';
import { UserService } from '../../services/user.service';
import { StoredEventService } from 'src/app/shared/services/stored-event.service';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { appConstants } from '../../constants/appConstants';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss'],
})
export class UserDetailsComponent implements OnInit {
  @ViewChild('storedEventViewerContainer', { read: ViewContainerRef })
  storedEventViewerContainer!: ViewContainerRef;

  userDetailsForm!: FormGroup;
  user!: User;
  faList = faList;

  constructor(
    private _formBuilder: FormBuilder,
    private _loaderService: LoaderService,
    private _userService: UserService,
    private _notificationService: NotificationService,
    private _localStorageService: LocalStorageService,
    private _storedEventService: StoredEventService,
    private _authService: AuthService
  ) {}

  async ngOnInit() {
    await this._loadUserDetails();
    if (this.user) {
      this.userDetailsForm = this._formBuilder.group({
        name: [this.user.name, Validators.required]
      });
    }
  }

  get isLoading() {
    return this._loaderService.loading$;
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.userDetailsForm.get(fieldName);
    return field!.invalid && field!.touched;
  }

  async saveDetails() {
    // stop here if form is invalid
    if (this.userDetailsForm.invalid) {
      return;
    }

    const userUpdate = new UpdateUserRequest(
      this._f['name'].value,
    );

    await firstValueFrom(
      this._userService.updateUser(
        this.user.id,
        userUpdate)
    ).then(async () => {
      this._notificationService.showSuccess('User successfully updated!');
      await this._loadUserDetails();
    });
  }

  async showUserStoredEvents() {
    await firstValueFrom(
      this._userService.getUserStoredEvents(
        this.user.id
      )
    ).then((result) => {
      if (result.success) {
        this._storedEventService.showStoredEvents(
          this.storedEventViewerContainer,
          result.data
        );
      }
    });
  }

  private async _storeLoadedUser() {
    // storing user in the localstorage
    this._localStorageService.setValue(
      appConstants.storedUser,
      JSON.stringify(this.user)
    );
  }

  private async _loadUserDetails() {
    await firstValueFrom(this._userService.loadUserDetails()).then(
      (result) => {
        if (result.success) {
          var data = result.data;
          this.user = new User(
            data.id,
            data.name,
            data.email
          );
          this._storeLoadedUser();
        }
      }
    );
  }

  // getter for easy access to form fields
  private get _f() {
    return this.userDetailsForm.controls;
  }
}
