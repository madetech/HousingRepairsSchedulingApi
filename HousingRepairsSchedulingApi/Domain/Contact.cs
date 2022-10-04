namespace HousingRepairsSchedulingApi.Domain;

/// <summary>
///     We are intentionally using primitives for now as validation has happened elsewhere, this could and _should_ be
///     updated
/// </summary>
public class Contact
{
    public Contact(string name, string phoneNumber, string notificationMobileNumber, string notificationEmail)
    {
        this.PhoneNumber = phoneNumber;
        this.NotificationMobileNumber = notificationMobileNumber;
        this.NotificationEmail = notificationEmail;
    }

    public string PhoneNumber { get; }
    public string NotificationMobileNumber { get; }
    public string NotificationEmail { get; }
}
