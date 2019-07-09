using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    public abstract class ExtendedDbContext : DbContext
    {
        private readonly DbContextExtendedOptions options;
        private readonly IDateTimeProvider _dateTimeProvider;

        private void SetMetaData<T>(T entity, DateTimeOffset? createdDate = null, DateTimeOffset? modifiedDate = null)
        {
            if (SetMetaDataOnInsert && entity is ICreated createdEntity)
                createdEntity.Created = createdDate ?? _dateTimeProvider.Now;

            if (SetMetaDataOnUpdate && entity is IModified modifiedEntity)
                modifiedEntity.Modified = modifiedDate ?? _dateTimeProvider.Now;
        }

        protected bool SetMetaDataOnInsert { get; set; } = true;
        protected bool SetMetaDataOnUpdate { get; set; } = true;

        protected ExtendedDbContext(DbContextOptions options, DbContextExtendedOptions dbContextExtendedOptions, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            options = dbContextExtendedOptions;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public override Task<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            SetMetaData(entity);
            return base.AddAsync(entity, cancellationToken);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return AddAsync(entity, CancellationToken.None).Result;
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            var keyProperties = entity.GetKeyProperties().ToArray();
            
            //Uses overload if more than key property is defined
            var foundEntity = keyProperties.Length > 1 
                ? Find<TEntity>(keyProperties) 
                : Find<TEntity>(keyProperties.Single());

            //Detaches the entity so the provided entity can be used to update instead
            Entry(foundEntity).State = EntityState.Detached;

            if(foundEntity is ICreated createdFoundEntity) 
                SetMetaData(entity, createdFoundEntity.Created);

            return base.Update(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (options.SingulariseTableNames)
                foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
                {
                    mutableEntityType.Relational().TableName = mutableEntityType.Relational()
                        .TableName.Singularize();
                }

            base.OnModelCreating(modelBuilder);
        }
    }
}