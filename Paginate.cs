using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace WishList
{
    public class Paginate<T> : List<T>
    {
        public int pageIndex {get; private set;}
        public int totalPages {get; private set;}

        public Paginate(List<T> items, int count, int PageIndex, int pageSize)
        {
            pageIndex = PageIndex; 
            totalPages = (int)Math.Ceiling(count /(double)pageSize); 

            this.AddRange(items); 
        }

        public bool HasPreviousPage
        {
            get{return (pageIndex > 1);}
        }

        public bool HasNextPage
        {
            get{return (pageIndex < totalPages);}
        }

       public static async Task<Paginate<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new Paginate<T>(items, count, pageIndex, pageSize);
        }
    }
}