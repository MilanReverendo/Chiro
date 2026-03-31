import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GroupDto } from '../../../models/group-dto';
import { GroupService } from '../../../services/group-service';

@Component({
  selector: 'app-group-settings',
  imports: [CommonModule, FormsModule],
  templateUrl: './group-settings.html',
})
export class GroupSettings implements OnInit {
  private groupService = inject(GroupService);

  groups = signal<GroupDto[]>([]);
  isLoading = signal(true);
  showForm = signal(false);
  editingId = signal<string | null>(null);
  deleteConfirmId = signal<string | null>(null);

  form: GroupDto = this.emptyForm();

  ngOnInit() { this.load(); }

  load() {
    this.isLoading.set(true);
    this.groupService.getAll().subscribe({
      next: (data) => { this.groups.set(data); this.isLoading.set(false); },
      error: () => this.isLoading.set(false)
    });
  }

  openCreate() {
    this.form = this.emptyForm();
    this.editingId.set(null);
    this.showForm.set(true);
  }

  openEdit(group: GroupDto) {
    this.form = { ...group };
    this.editingId.set(group.id!);
    this.showForm.set(true);
  }

  save() {
    const id = this.editingId();
    const op = id ? this.groupService.update(id, this.form) : this.groupService.create(this.form);
    op.subscribe(() => { this.showForm.set(false); this.load(); });
  }

  confirmDelete(id: string) { this.deleteConfirmId.set(id); }

  doDelete() {
    const id = this.deleteConfirmId();
    if (!id) return;
    this.groupService.delete(id).subscribe(() => { this.deleteConfirmId.set(null); this.load(); });
  }

  private emptyForm(): GroupDto {
    return { id: undefined, name: '', description: '', leaders: [] };
  }
}
