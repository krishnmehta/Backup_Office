export class CompanyInformtionDto{
    businessBrief: string | undefined;
    businessActivity: string | undefined;
    primaryIndustry: string | undefined;
    secondaryIndustry: string | undefined;
    primaryCustomer: string | undefined;
}

export class BusinessActivityDto {
    id: number | undefined;
    name: string | undefined;
    disabled?: boolean;
}

export class PrimaryIndustry {
    id: number | undefined;
    name: string | undefined;
}

export class SecondaryIndustry {
    id: number | undefined;
    name: string | undefined;
}

export class PrimaryCustomer {
    id: number | undefined;
    name: string | undefined;
    disabled?: boolean;
}