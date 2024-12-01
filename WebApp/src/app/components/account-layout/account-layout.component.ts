import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
import {MatTabsModule} from '@angular/material/tabs';

@Component({
  selector: 'app-account-layout',
  standalone: true,
  imports: [RouterOutlet, MatTabsModule, RouterLink],
  templateUrl: './account-layout.component.html',
  styleUrl: './account-layout.component.scss'
})
export class AccountLayoutComponent {
  selectedIndex = 0;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.route.url.subscribe(url => {
      const activeTab = this.router.url.split('/').pop();
      this.selectedIndex = this.getTabIndex(activeTab);
    });
  }

  getTabIndex(tab: string): number {
    switch (tab) {
      case '': return 0;
      case 'change-password': return 1;
      case 'preferences': return 2;
      default: return 0;
    }
  }

  onTabChange(event: any) {
    const tabIndex = event.index;
    const route = this.getRouteFromIndex(tabIndex);
    this.router.navigate([route], { relativeTo: this.route });
  }

  getRouteFromIndex(index: number): string {
    switch (index) {
      case 0: return '/account';
      case 1: return '/account/change-password';
      case 2: return '/account/preferences';
      default: return '';
    }
  }

  navigateTo = (route: string) => {
    this.router.navigate([route]);
  }
}
