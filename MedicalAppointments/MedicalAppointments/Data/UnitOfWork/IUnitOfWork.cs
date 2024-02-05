namespace MedicalAppointments.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Save();
    }
}
