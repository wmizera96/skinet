using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Data;

public static class ModelBuilderExtensions
{
    private const string SqliteProviderName = "Microsoft.EntityFrameworkCore.Sqlite";
    
    public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
    {
        var entities = modelBuilder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.IsAssignableTo(typeof(TInterface)))
            .Select(e => e.ClrType);
        
        foreach (var entity in entities)
        {
            var newParam = Expression.Parameter(entity);
            var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);    
            modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newBody, newParam));
        }
    }

    public static void FixSqliteDateTimeOffset(this ModelBuilder modelBuilder, DatabaseFacade databaseFacade)
    {
        if (databaseFacade.ProviderName == SqliteProviderName)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var dateTimeOffsetProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTimeOffset) || p.PropertyType == typeof(DateTimeOffset?));
                
                foreach (var property in dateTimeOffsetProperties)
                {
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion<string>();
                }
            }
        }
    }
}