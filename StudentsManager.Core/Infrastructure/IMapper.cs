using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsManager.Core.Infrastructure
{
    public interface IMapper
    {
        TDestination Adapt<TDestination>(object source);

        TDestination Adapt<TSource, TDestination>(TSource source);

        TDestination Adapt<TSource, TDestination>(TSource source, TDestination destination);

        object Adapt(object source, Type sourceType, Type destinationType);

        object Adapt(object source, object destination, Type sourceType, Type destinationType);
    }
}
