namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Stati, wekche eine Buchung annehmen kann
    /// </summary>
    public enum BookingStatus
    {
        Open = 1,
        Paid = 2,
        Cancled = 3
    }
}
