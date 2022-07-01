using System;
using System.Linq.Expressions;
using System.Reflection;
using Acudir.Domain.Interfaces;

namespace Acudir.Infrastructure.Extensions
{
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType mutableEntityType)
        {
            MethodInfo? methodToCall = typeof(SoftDeleteQueryExtension)?
                .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)?
                .MakeGenericMethod(mutableEntityType.ClrType) ?? throw new NullReferenceException();
            var filter = methodToCall?.Invoke(null, new object[] { }) ?? throw new NullReferenceException();
            mutableEntityType.SetQueryFilter((LambdaExpression)filter);
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => x.Activo;
            return filter;
        }
    }
}

