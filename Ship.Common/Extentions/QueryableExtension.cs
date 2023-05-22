﻿
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ship.Common.Extentions;
using Ship.Common.Models;

namespace Ship.Common.Extentions
{
    public static class QueryableExtension
    {
        public static async Task<PageList<T>> ToPageList<T>(this IQueryable<T> records, BaseSearchViewModel searchBase) where T : class
        {
            if (!string.IsNullOrEmpty(searchBase.SortBy))
            {
                records = records.OrderBy(searchBase.SortBy, searchBase.SortDes.Value);
            }
            var model = new PageList<T>()
            {
                Items = searchBase.Take.HasValue ? await records.Skip(searchBase.Skip.Value).Take(searchBase.Take.Value).ToArrayAsync() : await records.ToArrayAsync(),
                Total = records.Count()
            };
            //var response = new Response<PageList<T>>()
            //{
            //    Success = Constants.ResponseStatus.Success,
            //    data = model
            //};
            return model;
        }
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
