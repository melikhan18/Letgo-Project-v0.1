using Letgo.DataAccess;
using Letgo.Entity;

namespace Letgo.Business.Services
{
	public class BaseService<T> : IBaseService<T> where T : class
	{
		readonly LetgoContext _dbContext = new LetgoContext();
		public CommandResult Create(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(T));
			try
			{
				_dbContext.Set<T>().Add(entity);
				_dbContext.SaveChanges();
				return CommandResult.Success();
			}
			catch (Exception ex)
			{

				return CommandResult.Error(ex);
			}
		}

		public CommandResult Delete(int id)
		{
			if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
			try
			{
				var entity = GetById(id);
				_dbContext.Set<T>().Remove(entity);
				_dbContext.SaveChanges();
				return CommandResult.Success();
			}
			catch (Exception ex)
			{

				return CommandResult.Error(ex);
			}
		}

		public IEnumerable<T> GetAll()
		{
			try
			{
				return _dbContext.Set<T>().ToList();
			}
			catch (Exception ex)
			{
				return new List<T>();
			}
		}

		public T GetById(int id)
		{
			if (id < 0) throw new ArgumentOutOfRangeException();
			try
			{
				var entity = _dbContext.Set<T>().Find(id);
				return entity;
			}
			catch (Exception ex)
			{

				return null;
			}
		}

		public CommandResult Update(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			try
			{
				_dbContext.Set<T>().Update(entity);
				_dbContext.SaveChanges();
				return CommandResult.Success();
			}
			catch (Exception ex)
			{

				return CommandResult.Error(ex);
			}
		}


	}
}
