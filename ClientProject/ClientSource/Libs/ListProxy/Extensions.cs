using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public static class ListProxy_Extensions
  {
    //SimpleCast
    public static IList<TResult> As<TResult>(this IList source)
      => new ListProxy_IList<TResult>(source);

    public static IList<TResult> As<TSource, TResult>(this IList<TSource> source)
      => new ListProxy_IListT<TSource, TResult>(source);

    public static IReadOnlyList<TResult> As<TSource, TResult>(this IReadOnlyList<TSource> source)
      => new ListProxy_IReadOnlyListT<TSource, TResult>(source);

    //CustomConversion
    public static IList<TResult> As<TResult>(
      this IList source,
      Func<object, TResult> forward,
      Func<TResult, object> backward = null
    ) => new CustomListProxy_IList<TResult>(source, forward, backward);

    public static IList<TResult> As<TSource, TResult>(
      this IList<TSource> source,
      Func<TSource, TResult> forward,
      Func<TResult, TSource> backward = null
    ) => new CustomListProxy_IListT<TSource, TResult>(source, forward, backward);

    public static IReadOnlyList<TResult> As<TSource, TResult>(
      this IReadOnlyList<TSource> source,
      Func<TSource, TResult> forward
    ) => new CustomListProxy_IReadOnlyListT<TSource, TResult>(source, forward);
  }
}