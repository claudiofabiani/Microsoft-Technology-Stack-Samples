using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class PaginatedEnumerableDto<T> : PaginatedEnumerableDto, IEnumerable<T>
    {
        private IEnumerable<T> _enumerable;
        public IEnumerable<T> Items 
        {
            get { return _enumerable; }   // get method
            set { _enumerable = value; }
        }


        

System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }
    }

    public class PaginatedEnumerableDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public int ItemsStartCount { get; set; }

        public int ItemsEndCount { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
