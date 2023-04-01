export class DiagnoseProductsDto {
    id!: number;
    name!: string;
    description!: string;
}

export class QuestionnaireDto {
    id!: number;
    name!: string;
    questionnaireDescription!: string;
    typeformLink!: string;
}

export class ProductDataPointListDto
{
    dataUploadTypeformLink!: string;
    productDataUploadPoints!: ProductDataPointDto[];
}

export class ProductDataPointDto
{
    dataPointName!: string;
    templateLink!: string;
}