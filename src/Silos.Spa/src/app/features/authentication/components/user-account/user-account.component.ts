import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from 'src/app/core/services/notification.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { UserService } from 'src/app/features/recordings/services/user.service';
import { RegisterUserRequest } from 'src/app/features/recordings/models/requests/RegisterUserRequest';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.scss'],
})
export class UserAccountComponent implements OnInit {
  accountForm!: FormGroup;
  returnUrl!: string;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private loaderService: LoaderService,
    private userService: UserService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.accountForm = this.formBuilder.group({
      email: ['', Validators.required],
      name: ['', Validators.required],
      password: ['', Validators.required],
      passwordConfirm: ['', Validators.required]
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get isLoading() {
    return this.loaderService.loading$;
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.accountForm.get(fieldName);
    return field!.invalid && field!.touched;
  }

  onSubmit() {
    // stop here if form is invalid
    if (this.accountForm.invalid) {
      return;
    }

    const userRegistration = new RegisterUserRequest(
      this.f['email'].value,
      this.f['name'].value,
      this.f['password'].value,
      this.f['passwordConfirm'].value
    );

    this.userService
      .registerUser(userRegistration)
      .subscribe((result) => {
        if (result.success) {
          this.notificationService.showSuccess('Account successfully created!');
          this.router.navigate([this.returnUrl]);
        }
      });
  }

  // getter for easy access to form fields
  private get f() {
    return this.accountForm.controls;
  }
}
