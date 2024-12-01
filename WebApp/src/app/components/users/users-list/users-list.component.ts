import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SHARED_MATERIAL } from '../../../helpers/shared_modules/shared-material';
import { UsersService } from '../../../services/users.service';
import { User } from '../../../models/User';
import { Response } from '../../../models/Response';
import { NgFor, NgForOf, NgIf } from '@angular/common';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [RouterLink, NgForOf, NgIf, NgFor, SHARED_MATERIAL],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent {

  readonly _usersService = inject(UsersService);

  users = signal<User[]>([]);
  loadingUsers = signal(true);

  constructor() {
    this.loadUsers();
  }

  loadUsers = () => {
    this._usersService.getAll().subscribe({
      next: (res: Response) => {
        if(res.succeeded){
          this.users.set(res.data);
        }
      }
    }).add(() => this.loadingUsers.set(false));
  }
}
