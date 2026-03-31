import { Component, OnInit, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserShortDto } from '../../../models/user-short-dto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.html',
  styleUrl: './contact-page.css',
  imports: [FormsModule],
})
export class ContactPage implements OnInit {
  private http = inject(HttpClient);

  leaders: UserShortDto[] = [];
  isLoading = true;

  ngOnInit() {
    // Example API call to get leaders
    this.http.get<UserShortDto[]>('/api/auth/all-users').subscribe({
      next: (data) => {
        this.leaders = data;
        this.isLoading = false;
      },
      error: () => (this.isLoading = false),
    });
  }

  submitForm(form: any) {
    // Replace with your actual API call
    console.log('Form submitted:', form);
  }
}
