import { Component, inject } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-toasts',
  standalone: true,
  imports: [NgbToast],
  templateUrl: './toasts.component.html',
  styleUrl: './toasts.component.scss'
})
export class ToastsComponent {
  toastService = inject(ToastService);
}
