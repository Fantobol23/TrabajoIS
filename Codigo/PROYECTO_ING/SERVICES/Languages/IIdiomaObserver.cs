using BE_Entities.Languages;

namespace SERVICES.Languages
{
    /// <summary>
    /// OBSERVER. Cualquier objeto que muestre leyendas (en este sistema, cada formulario
    /// a través de su adaptador) implementa esta interfaz para enterarse del cambio de idioma.
    /// </summary>
    public interface IIdiomaObserver
    {
        void ActualizarIdioma(Idioma nuevoIdioma);
    }
}
