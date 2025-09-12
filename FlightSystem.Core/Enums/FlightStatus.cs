namespace FlightSystem.Core.Enums
{
    public enum FlightStatus
    {
        Scheduled = 1,      // Төлөвлөсөн
        CheckinOpen = 2,    // Бүртгэж байна  
        CheckinClosed = 3,  // Бүртгэл хаагдсан
        Boarding = 4,       // Онгоцонд сууж байна
        LastCall = 5,       // Сүүлчийн дуудлага
        GateClosed = 6,     // Хаалга хаагдсан
        Departed = 7,       // Ниссэн
        Delayed = 8,        // Хойшилсон
        Cancelled = 9       // Цуцалсан
    }
}
