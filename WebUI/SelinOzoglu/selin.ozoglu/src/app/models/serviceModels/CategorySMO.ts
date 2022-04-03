export class CategorySMO {
    id: number;
    title: string;
    description: string;
    isActive: boolean;
    sort:number;
    createdDate:Date;
    updatedDate:Date;
}
export class CategoryAddSMO{
    categoryId: number;
}
export class CategoryUpdateSMO{
    categoryId:number;
}