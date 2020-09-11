import { KeyValuePair } from "../_helpers/keyValuePair"

export class Day{
    public date: Date;
    public total: number;
    public entertainments: number;
    public food: number;
    public household: number;
    public shops: KeyValuePair[];

    public constructor(init?:Partial<Day>) {
        Object.assign(this, init);
    }
}