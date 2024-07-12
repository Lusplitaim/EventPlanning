import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from '../../models/loginUser';
import { AccountService } from '../../services/account.service';
import { SharedModule } from '../../shared.module';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  formBuilder = inject(FormBuilder);

  registerForm = this.formBuilder.group({
    userName: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });

  signup() {
    const logUserData: LoginUser = {
      email: this.registerForm.get("email")?.value ?? "",
      password: this.registerForm.get("password")?.value ?? "",
    };
    this.accountService.login(logUserData)
      .subscribe(_ => {
        this.router.navigate([""]);
      });
  }
}
