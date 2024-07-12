import { Component, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from '../../models/loginUser';
import { AccountService } from '../../services/account.service';
import { SharedModule } from '../../shared.module';
import { NgbModal, NgbModalConfig, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { RegisterUser } from '../../models/registerUser';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [SharedModule],
  providers: [NgbModalConfig, NgbModal],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  formBuilder = inject(FormBuilder);
  modalService = inject(NgbModal);

  confirmEmailLink = '';
  @ViewChild("modalContent")
  modal: NgbModalRef | undefined;

  registerForm = this.formBuilder.group({
    userName: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });

  signup(): void {
    const logUserData: RegisterUser = {
      userName: this.registerForm.get("userName")?.value ?? "",
      email: this.registerForm.get("email")?.value ?? "",
      password: this.registerForm.get("password")?.value ?? "",
    };
    this.accountService.register(logUserData)
      .subscribe(model => {
        this.confirmEmailLink = model.emailLink;
        this.modalService.open(this.modal);
      });
  }
}
