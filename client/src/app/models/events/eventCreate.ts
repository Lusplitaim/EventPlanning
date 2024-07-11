export interface EventCreate {
    name: string,
    description?: string,
    venue?: string,
    isOnline: boolean,
    startDate: Date,
    endDate: Date,
}