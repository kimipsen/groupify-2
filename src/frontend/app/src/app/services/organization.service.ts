import { Injectable } from '@angular/core';
import { Organization } from '../models/Organization';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  constructor() { }

  public async getOrganizations(): Promise<Organization[]> {
    const response = await fetch('/api/organizations');
    const data = await response.json();
    return data.map((org: any) => new Organization(org.id, org.name));
  }

  public async getOrganization(id: string): Promise<Organization> {
    const response = await fetch(`/api/organizations/${id}`);
    const data = await response.json();
    return new Organization(data.id, data.name);
  }
}
