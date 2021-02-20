using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Extension.Domain
{
    public static class PaginatedEnumerableExtensions
    {

        /// <summary>
        /// Crea un enumerable che sia paginabile
        /// </summary>
        /// <param name="enumerable">enumerable da paginare</param>
        /// <param name="pageIndex">indice di pagina corrente (per es. 2 di 5)</param>
        /// <param name="pageSize">grandezza della pagina (per es. una pagina contiene 10 elementi)</param>
        /// <param name="totalCount">numeri di elementi da processare (per es. 50 elementi)</param>
        public static PaginatedEnumerable<T> ToPaginated<T>(this IEnumerable<T> enumerable, int? totalCount = null, int pageIndex = 0, int pageSize = 10)
        {
            return new PaginatedEnumerable<T>(enumerable, totalCount, pageIndex, pageSize);
        }
    }
    public abstract class PaginatedEnumerable
    {
        public int PageIndex { get; protected set; }
        public int PageSize { get; protected set; }
        public int TotalCount { get; protected set; }
        public int TotalPages { get; protected set; }

        /// <summary>
        /// Da quale elemento si inizia a iterare (es. 21)
        /// </summary>
        public int ItemsStartCount => PageIndex * PageSize + 1;

        /// <summary>
        /// Da quale elemento si finisce di iterare (es. 21)
        /// </summary>
        public int ItemsEndCount => Math.Min((PageIndex + 1) * PageSize, TotalCount);

        public bool HasPreviousPage => (PageIndex > 0);
        public bool HasNextPage => (PageIndex + 1 < TotalPages);
    }

    /// <summary>
    /// Consente di paginare una collezione
    /// </summary>
    public sealed class PaginatedEnumerable<T> : PaginatedEnumerable, IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        /// <summary>
        /// Crea una collezione vuota
        /// </summary>
        public static PaginatedEnumerable<T> Empty()
        {
            return new PaginatedEnumerable<T>();
        }

        /// <summary>
        /// Crea un enumerable che sia paginabile
        /// </summary>
        public PaginatedEnumerable()
            : this(null)
        {
        }

        /// <summary>
        /// Crea un enumerable che sia paginabile
        /// </summary>
        /// <param name="enumerable">enumerable da paginare</param>
        /// <param name="pageIndex">indice di pagina corrente (per es. 2 di 5)</param>
        /// <param name="pageSize">grandezza della pagina (per es. una pagina contiene 10 elementi)</param>
        /// <param name="totalCount">numeri di elementi da processare (per es. 50 elementi)</param>
        public PaginatedEnumerable(IEnumerable<T> enumerable, int? totalCount = null, int pageIndex = 0, int pageSize = 10)
        {
            if (enumerable == null)
                enumerable = Enumerable.Empty<T>();
            _enumerable = enumerable;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount ?? enumerable.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        /// <summary>
        /// Copia le stesse info di paginazione in una collezione con un tipo diverso.
        /// Utile quando si converte da una collezione a un'altra collezione e si vogliono
        /// mantenere le info di paginazione
        /// </summary>
        public PaginatedEnumerable<TDestination> Clone<TDestination>(IEnumerable<TDestination> enumerableDestination)
        {
            return new PaginatedEnumerable<TDestination>(enumerableDestination, TotalCount, PageIndex, PageSize);
        }
    }
}
