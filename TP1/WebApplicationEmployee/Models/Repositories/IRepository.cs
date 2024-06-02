namespace WebApplicationEmployee.Models.Repositories
{
    public interface IRepository<T>
    {
        List<T> Search(string term);

        IList<T> GetAll();
        T FindByID(int id);
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        double SalaryAverage();
        double MaxSalary();
        int HrEmployeesCount();

    }

}