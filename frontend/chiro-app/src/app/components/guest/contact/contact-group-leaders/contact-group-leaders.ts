import { Component, input } from '@angular/core';
import { UserShortDto } from '../../../../models/user-short-dto';
import { UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-contact-group-leaders',
  imports: [UpperCasePipe],
  templateUrl: './contact-group-leaders.html',
  styleUrl: './contact-group-leaders.css',
})
export class ContactGroupLeaders {
  groupLeaders = input<UserShortDto[]>();
  isLoading = input<boolean>();
}
