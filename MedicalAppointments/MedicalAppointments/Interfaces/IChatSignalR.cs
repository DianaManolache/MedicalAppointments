namespace MedicalAppointments.Interfaces
{
    public interface IChatSignalR
    {
        Task ReceiveMessage(string message);
    }
}
