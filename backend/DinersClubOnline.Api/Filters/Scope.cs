using System;

namespace DinersClubOnline.Api.Filters
{
    /// <summary>Scopes válidos a soportar por ScopeAuthorize</summary>
    [Flags]
    public enum Scopes
    {
        /// <summary>Usa credenciales de la tarjeta</summary>
        Registro = 1 << 0,

        /// <summary>Usa credenciales de Clave digital</summary>
        Consultas = 1 << 1,

        /// <summary>Autoriza a usar operaciones donde se debe confirmar la clave digital</summary>
        Operaciones = 1 << 2
    }
}