import { Component, OnInit, inject, signal } from '@angular/core';
import { UserShortDto } from '../../../../models/user-short-dto';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { ContactForm } from '../contact-form/contact-form';
import { ContactGroupLeaders } from '../contact-group-leaders/contact-group-leaders';
import { AuthService } from '../../../../services/auth-service';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.html',
  styleUrl: './contact-page.css',
  imports: [FormsModule, ContactForm, ContactGroupLeaders],
})
export class ContactPage implements OnInit {
  authService: AuthService = inject(AuthService);

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
    this.authService.getGroupLeaders().subscribe({
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
