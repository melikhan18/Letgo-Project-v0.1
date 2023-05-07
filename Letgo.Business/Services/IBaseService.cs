namespace Letgo.Business.Services
{
	public interface IBaseService<T> where T : class
	{
		public IEnumerable<T> GetAll();
		public T GetById(int id);
		public CommandResult Create(T entity);
		public CommandResult Update(T entity);
		public CommandResult Delete(int id);

	}
}
