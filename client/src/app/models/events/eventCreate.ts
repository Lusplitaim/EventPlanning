export interface EventCreate {
    name: string,
    description?: string,
    venue?: string,
    isOnline: boolean,
    maxMembersCount?: number;
    startDate: Date,
    endDate: Date,
}