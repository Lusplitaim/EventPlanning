import { EventMember } from "../eventMember";

export interface UserEvent {
    id: number,
    name: string,
    description?: string,
    venue?: string,
    isOnline: boolean,
    startDate: Date,
    endDate: Date,
    maxMembersCount?: number,
    creatorId: number,
    members: EventMember[],
}