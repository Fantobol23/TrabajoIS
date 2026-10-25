namespace BE_Entities.Integrity
{
    public enum TipoErrorIntegridad
    {
        /// <summary>El contenido de un registro no coincide con su DVH (modificación externa o datos intercambiados).</summary>
        DVH_Invalido = 1,

        /// <summary>La suma de DVH de la tabla no coincide con el DVV guardado (se agregaron o quitaron registros por fuera del sistema).</summary>
        DVV_Invalido = 2,

        /// <summary>No existe el DVV de la tabla (nunca se generó o fue borrado).</summary>
        DVV_Inexistente = 3
    }
}
