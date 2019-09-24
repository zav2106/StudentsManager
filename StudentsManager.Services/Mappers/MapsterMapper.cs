using Mapster;
using StudentsManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsManager.Services.Mappers
{
    public class MapsterMapper : IMapper
    {
        private readonly Adapter _adapter;
        protected readonly TypeAdapterConfig _config;

        public MapsterMapper()
        {
            _config = new TypeAdapterConfig();
            _adapter = new Adapter(_config);

            ConfigureMappers();
        }

        protected virtual void ConfigureMappers()
        {
        }

        protected TypeAdapterSetter<TSource, TDestination> CreateMapper<TSource, TDestination>()
        {
            return _config.NewConfig<TSource, TDestination>();
        }

        public TDestination Adapt<TDestination>(object source)
        {
            return _adapter.Adapt<TDestination>(source);
        }

        public TDestination Adapt<TSource, TDestination>(TSource source)
        {
            return _adapter.Adapt<TSource, TDestination>(source);
        }

        public TDestination Adapt<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _adapter.Adapt(source, destination);
        }

        public object Adapt(object source, Type sourceType, Type destinationType)
        {
            return _adapter.Adapt(source, sourceType, destinationType);
        }

        public object Adapt(object source, object destination, Type sourceType, Type destinationType)
        {
            return _adapter.Adapt(source, destination, sourceType, destinationType);
        }
    }
}
