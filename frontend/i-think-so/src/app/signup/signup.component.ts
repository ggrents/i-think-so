import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent {
  // signupForm: FormGroup;
  // constructor(private fb: FormBuilder) { }
  // ngOnInit() {
  //   this.signupForm = this.fb.group({
  //     username: ['', Validators.required],
  //     email: ['', [Validators.required, Validators.email]],
  //     password: ['', [Validators.required, Validators.minLength(6)]]
  //   });
  // }
  // onSubmit() {
  //   // Обработка отправки формы
  //   console.log(this.signupForm.value);
  // }
}
