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
    guest?: Guest ;
    reservation?: Reservation ;
    diffDays:number=0;
}

class RoomType {
    roomTypeID: string = "";
    name: string = "";
    description: string = "";
    dailyPrice: number = 0;
    dateCreated: string = "";
}

class Guest {
    guestID: string = "";
    fullName: string = "";
    age: number = 0;
    email: string = "";
    phoneNumber: string = "";
    dateCreated: string = "";
}

class Reservation {
    reservationID: string = "";
    guestID: string = "";
    startTime: string = "";
    endTime: string = "";
    isConfirmed: boolean = false;
    confirmationTime: string = "";
    dateCreated: string = "";
}

class ReservationRoom {
    reservationID: string = "";
    roomID: string = "";
}

class Bill{
    id: string = "";
    sum: number=0;
    status:boolean=false;
    idGuest: string = "";
    dateCreated: string = "";
}

export { Response, Room, RoomType, Guest,Reservation,ReservationRoom,Bill};