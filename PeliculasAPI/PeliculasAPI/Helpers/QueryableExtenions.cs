using PeliculasAPI.DTOs;
using System.Linq;

namespace PeliculasAPI.Helpers
{
    public static class QueryableExtenions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            return queryable
                    .Skip((paginacionDTO.Pagina - 1) * paginacionDTO.CantidadRegistrosPorPagina)
                    .Take(paginacionDTO.CantidadRegistrosPorPagina);

        }
    }
}
