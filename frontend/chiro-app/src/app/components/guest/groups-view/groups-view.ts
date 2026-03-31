import { Component, OnInit, inject } from '@angular/core';
import { GroupService } from '../../../services/group-service';
import { GroupDto } from '../../../models/group-dto';


@Component({
  selector: 'app-groups-view',
  templateUrl: './groups-view.html',
  styleUrl: './groups-view.css',
})
export class GroupsView implements OnInit {

  private groupService = inject(GroupService);

  groups: GroupDto[] = [];
  isLoading = true;

  ngOnInit() {
    this.groupService.getAll().subscribe({
      next: (data) => {
        this.groups = data;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }
}
