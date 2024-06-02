namespace TP2.Models.Repositories
{
    public interface IRepository<T>
    {
		List<T> Search(string term);
		T Get(int Id);
        IEnumerable<T> GetAll();
        T FindByID(int id);

        T Add(T t);
        T Update(int id, T t);
        T Delete(int Id);
        string? FindById(int id);
		Product Update(Product product);
	}

}
