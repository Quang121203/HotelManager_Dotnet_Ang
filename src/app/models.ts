class Response {
    ec: number = 0;
    em: string = "";
    dt: any = "";
}

class Room {
    roomID: string = "";
    roomNumber: string = "";
    roomTypeID: string = "";
    roomTypeName: string = "";
    dateCreated: string = "";
    isAvaiable: boolean = true;
}

class RoomType {
    roomTypeID: string = "";
    name: string = "";
    description: string = "";
    dailyPrice: number = 0;
    dateCreated: string = "";
}

export { Response, Room, RoomType };