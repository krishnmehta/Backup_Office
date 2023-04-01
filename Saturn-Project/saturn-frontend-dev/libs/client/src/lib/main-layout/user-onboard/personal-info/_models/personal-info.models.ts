export class PersonalInfo {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  streetLine: string;
  city: string;
  state: string;
  professionalSummary: string;
  companyUserType: number;
  competencyIds: number[] = [];
  companyCompetencyMappings?: companyCompetencyMappings[];
  ProfessionalPhotoName: string;
  professionalPhotoUrl: string;

  constructor() {
    this.firstName = '';
    this.lastName = '';
    this.email = '';
    this.phoneNumber = '';
    this.streetLine = '';
    this.city = '';
    this.state = '';
    //  this.professionalPhotoName = '';
    this.professionalSummary = '';
    this.companyUserType = 0;
    this.competencyIds = [];
    this.ProfessionalPhotoName = '';
    this.professionalPhotoUrl = '';
  }
}

export class CompetencyList {
  id: number | undefined;
  title: string | undefined;
}

export class companyCompetencyMappings {
  id?: number | undefined;
  companyInfoId?: string | undefined;
  competencyId?: number | undefined;
}
