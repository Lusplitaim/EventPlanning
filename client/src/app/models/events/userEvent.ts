export interface UserEvent {
    id: number,
    name: string,
    description?: string,
    venue?: string,
    isOnline: boolean,
    startDate: Date,
    endDate: Date,
    creatorId: number,
}