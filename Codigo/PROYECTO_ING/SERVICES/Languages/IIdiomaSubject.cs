namespace SERVICES.Languages
{
    /// <summary>SUBJECT del patrón Observer.</summary>
    public interface IIdiomaSubject
    {
        void Suscribir(IIdiomaObserver observador);
        void Desuscribir(IIdiomaObserver observador);
        void Notificar();
    }
}
