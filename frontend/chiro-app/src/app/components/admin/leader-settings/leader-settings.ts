import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth-service';
import { UserShortDto } from '../../../models/user-short-dto';
import { GroupDto } from '../../../models/group-dto';
import { GroupService } from '../../../services/group-service';

@Component({
  selector: 'app-leader-settings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './leader-settings.html',
  styleUrl: './leader-settings.css',
})
export class LeaderSettings implements OnInit {
  private authService = inject(AuthService);
  private groupService = inject(GroupService);

  users: UserShortDto[] = [];
  groups: GroupDto[] = [];

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    // Fetch users
    this.authService.getAllUsers().subscribe(data => this.users = data);
    // Fetch groups for the dropdown
    this.groupService.getAll().subscribe(data => this.groups = data);
  }

  saveUser(user: UserShortDto) {
    this.authService.modifyUserDetails(user).subscribe({
      next: () => alert('User updated successfully!'),
      error: (err) => console.error('Update failed', err)
    });
  }
}
