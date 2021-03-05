using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Mapping
{
    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter
        : ITypeAdapter
    {
        private readonly IMapper _mapper;

        #region ITypeAdapter Members

        public AutomapperTypeAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget Adapt<TSource, TTarget>(TSource source) where TSource : class where TTarget : class, new()
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class, new()
        {
            return _mapper.Map<TTarget>(source);
        }


        #endregion
    }
}
