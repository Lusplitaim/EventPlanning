import { Component, inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared.module';
import { LoginUser } from '../../models/loginUser';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);

  email = new FormControl<string>('');
  password = new FormControl<string>('');

  login() {
    const logUserData: LoginUser = {
      email: this.email.value ?? '',
      password: this.password.value ?? '',
    };
    this.accountService.login(logUserData)
      .subscribe(_ => {
        this.router.navigate([""]);
      });
  }
}
