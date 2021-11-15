import { InterventionType } from "./interventionType";
import { MutualisationPoint } from "./mutualisationPoint";
import { Operator } from "./operator";
import { User } from "./user";

export interface Intervention{
    interventionId: number;
    name: String,

    beginDate?: Date,
    unloadedCode?: String,

    creationDate?: Date,
    type?: InterventionType,
    operator?: Operator,
    mutualisationPoint?: MutualisationPoint,
    interventionPto?: String,
    user?: User,
}