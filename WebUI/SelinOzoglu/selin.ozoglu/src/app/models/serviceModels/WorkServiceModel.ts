import { WorkItemsTemplateType } from "src/shared/constants/workItemTypeEnum";


export class WorkSMO {
    id: number;
    title: string;
    description: string;
    creatorUserId: number;
    mainPicture:string;
    workItems: WorkItemSMO[];
    
}

export class WorkItemSMO {
    pictures: string[];
    templateType: number;
    title?: any;
    workId: number;
    description?: any;
}

