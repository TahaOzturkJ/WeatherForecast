using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.IntRep
{
    public interface IRepository<T>
    {
        //List Commands: Sorgulama yapmak için kullanılan komutlar
        List<T> GetAll();
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();

        //Modify Commands: Değişiklik yapmak için kullanılan komutlar
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);
        void Destroy(T item);

        //Linq Expressions: Linq Sorguları
        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, bool>> exp);

        //Find: Primary Key'e göre veri sorgulayan ve döndüren metod
        T Find(int id);
    }
}
