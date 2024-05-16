import { Component, OnInit, VERSION } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { IUserRequest } from '../../core/auth/models/user.request.interface';
import { AuthService } from '../../core/auth/auth.service';
import { MessageService } from 'primeng/api';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    InputTextModule,
    ReactiveFormsModule,
    ToastModule,
    HttpClientModule,
  ],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
  providers: [AuthService, MessageService],
})
export class SignupComponent {
  signUpForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService
  ) {
    this.signUpForm = new FormGroup({
      username: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }

  onSubmit() {
    if (this.signUpForm.invalid) {
      return;
    }
    const _user: IUserRequest = {
      username: this.signUpForm.controls['username'].value,
      email: this.signUpForm.controls['email'].value,
      password: this.signUpForm.controls['password'].value,
    };
    this.authService.registerUser(_user).subscribe({
      next: () => this.router.navigate(['/signin']),
      error: () =>
        this.messageService.add({
          severity: 'error',
          summary: 'error!',
          detail: 'something went wrong!',
        }),
    });
  }
}
