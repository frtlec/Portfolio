import { KeyValue } from "../KeyValue";

export class SaveWorkSMO{
    workId:number;
    hasIsBeenAdded:KeyValue<string,number>

    constructor(workId: number, hasIsBeenAdded: KeyValue<string,number>) {
        this.workId = workId
        this.hasIsBeenAdded = hasIsBeenAdded
    }

}