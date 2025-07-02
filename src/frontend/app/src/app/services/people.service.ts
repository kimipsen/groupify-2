import { Injectable } from '@angular/core';
import { Person } from '../models/Person';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  constructor() { }

  public async getPeople(searchTerm: string = '', pageSize: number = 20, lastId?: string): Promise<Person[]> {
    const params = new URLSearchParams();
    if (searchTerm) params.append('searchTerm', searchTerm);
    if (pageSize) params.append('pageSize', pageSize.toString());
    if (lastId) params.append('lastPersonId', lastId);
    const response = await fetch(`/api/people?${params.toString()}`);
    const data = await response.json();
    return data.map((person: any) => new Person(person.id, person.name));
  }

  public async getPerson(id: string): Promise<Person> {
    const response = await fetch(`/api/people/${id}`);
    const data = await response.json();
    return new Person(data.id, data.name);
  }
}
