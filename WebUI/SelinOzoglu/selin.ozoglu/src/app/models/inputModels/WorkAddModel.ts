import { WorkItemsTemplateType } from "src/shared/constants/workItemTypeEnum";

export  class WorkAddModel{
    mainPicture:string;
    title:string;
    description:string;
    isActive:boolean;
    workItems:WorkItemAddModel[];
    categoryId:number;
}
export class WorkItemAddModel{
    id:number;
    pictures:string[];
    templateType:WorkItemsTemplateType;
    title:string;
    description:string;
}
export class WorkFilterModel{
    limit:number;
    search:string;
    isActive:boolean=true;
    categoryId:number;
    isShowMainPage:boolean=true;
}