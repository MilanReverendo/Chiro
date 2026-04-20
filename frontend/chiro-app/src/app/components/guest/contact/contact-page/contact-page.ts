import { Component, OnInit, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserShortDto } from '../../../../models/user-short-dto';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { ContactForm } from '../contact-form/contact-form';
import { ContactGroupLeaders } from '../contact-group-leaders/contact-group-leaders';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.html',
  styleUrl: './contact-page.css',
  imports: [FormsModule, ContactForm, ContactGroupLeaders],
})
export class ContactPage implements OnInit {
  private http = inject(HttpClient);

  leaders: UserShortDto[] = [];
  isLoading = signal(true);

  // contact formgroup
  contactForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    message: new FormControl('', Validators.required),
  });

  // fetch group leaders when the component is generated
  ngOnInit() {
    this.http.get<UserShortDto[]>('/api/auth/all-users').subscribe({
      next: (data) => {
        this.leaders = data;
        this.isLoading.set(false);
      },
      error: () => (this.isLoading.set(false)),
    });
  }

  //send the contact form
  submitContactForm(contactForm: FormGroup) {
    console.log('submitContactForm', contactForm.value);
  }
}
