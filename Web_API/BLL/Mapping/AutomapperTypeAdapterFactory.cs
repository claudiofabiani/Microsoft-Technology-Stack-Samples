using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Mapping
{
    public class AutomapperTypeAdapterFactory
        : ITypeAdapterFactory
    {
        private readonly IMapper _mapper;

        #region Constructor

        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory(IMapper mapper)
        {
            _mapper = mapper;


        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter(_mapper);
        }

        #endregion
    }
}
