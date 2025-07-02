import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PeopleService } from './services/people.service';
import { OrganizationService } from './services/organization.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  peopleService = inject(PeopleService);
  organizationsService = inject(OrganizationService);

  ngOnInit(): void {
    this.test();
  }
  title = 'app';

  test(): void {
    fetch('/api/weatherforecast')
      .then(response => console.log(`Fetched from backend: `, response.json()))
    ;

    this.peopleService.getPeople().then(people => console.log('People:', people));
    this.organizationsService.getOrganizations().then(orgs => console.log('Organizations:', orgs));
  }
}
