namespace EisaAwards.Repository
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Implements those basic  operations that are general (do not depend on data types),
    /// leaves the rest to the descendants.
    /// </summary>
    /// <typeparam name="T">A generic type (limited to classes).</typeparam>
    public abstract class RepositoryClass<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryClass{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
        protected RepositoryClass(ref DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAll()
        {
            return this.dbContext.Set<T>();
        }

        /// <inheritdoc/>
        public abstract T GetOne(int id);

        /// <inheritdoc/>
        public abstract T GetOne(string name);

        /// <inheritdoc/>
        public void Insert(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
            this.dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Remove(T entity)
        {
            this.dbContext.Set<T>().Remove(entity);
            this.dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public abstract void Remove(int id);

        /// <inheritdoc/>
        public void Update(T entity)
        {
            this.dbContext.Set<T>().Update(entity);
            this.dbContext.SaveChanges();
        }
    }
}