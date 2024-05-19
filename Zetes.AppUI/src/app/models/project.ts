import { Customer } from "./customer";

export interface Project {
    projectId: number;
    customerId:number;
    customer?:Customer;
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
}