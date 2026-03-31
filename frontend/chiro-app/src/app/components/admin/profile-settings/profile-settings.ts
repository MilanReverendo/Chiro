import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../../services/auth-service';
import { UserShortDto } from '../../../models/user-short-dto';


@Component({
  selector: 'app-profile-settings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile-settings.html',
})
export class ProfileSettingsComponent implements OnInit {
  private http = inject(HttpClient);
  private authService = inject(AuthService);

  isSaving = signal(false);
  isChangingPassword = signal(false);
  saveSuccess = signal(false);
  passwordSuccess = signal(false);
  errorMessage = signal<string | null>(null);

  form: Partial<UserShortDto> = {
    id: '',
    username: '',
    firstName: '',
    lastName: '',
    isGroupLeader: false,
  };

  passwordForm = {
    current: '',
    newPassword: '',
    confirm: '',
  };

  ngOnInit() {
    const user = this.authService.currentUser();
    if (!user) return;

    this.http.get<UserShortDto>(`/api/auth/${user.id}`).subscribe(u => {
      this.form = { ...u };
    });
  }

  saveProfile() {
    this.isSaving.set(true);
    this.saveSuccess.set(false);
    this.errorMessage.set(null);

    this.http.put<UserShortDto>('/api/auth/ModifyUserDetails', this.form).subscribe({
      next: updated => {
        this.authService.currentUser.set(updated);
        this.saveSuccess.set(true);
        this.isSaving.set(false);
      },
      error: () => {
        this.errorMessage.set('Opslaan mislukt.');
        this.isSaving.set(false);
      },
    });
  }

  changePassword() {
    if (this.passwordForm.newPassword !== this.passwordForm.confirm) {
      this.errorMessage.set('Wachtwoorden komen niet overeen.');
      return;
    }
    this.isChangingPassword.set(true);
    this.errorMessage.set(null);

    this.http.put('/api/auth/change-password', {
      currentPassword: this.passwordForm.current,
      newPassword: this.passwordForm.newPassword,
    }).subscribe({
      next: () => {
        this.passwordSuccess.set(true);
        this.isChangingPassword.set(false);
        this.passwordForm = { current: '', newPassword: '', confirm: '' };
      },
      error: () => {
        this.errorMessage.set('Wachtwoord wijzigen mislukt. Controleer uw huidig wachtwoord.');
        this.isChangingPassword.set(false);
      },
    });
  }
}
