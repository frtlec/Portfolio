import { WorkItemsTemplateType } from "src/shared/constants/workItemTypeEnum";

export  class WorkUpdateModel{
    workId:number;
    mainPicture:string;
    title:string;
    description:string;
    isActive:boolean;
    workItems:WorkItemUpdateModel[];
    categoryId:number;
}
export class WorkItemUpdateModel{
    id:number;
    pictures:string[];
    templateType:WorkItemsTemplateType;
    title:string;
    workId:number;
    description:string;
}