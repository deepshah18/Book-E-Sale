using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class PublisherRepository : BaseRepository
    {
        public List<Publisher> GetPublishers(string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
           //var publisher = _context.Publishers.AsQueryable();
            var publisher = _context.Publishers.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            return publisher.ToList();
        }

        public Publisher GetPublisher(int id)
        {
            if (id > 0)
            {
                return _context.Publishers.Where(w => w.Id == id).FirstOrDefault();
            }

            return null;
        }

        public Publisher AddPublisher(Publisher publisher)
        {
            var entry = _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Publisher UpdatePublisher(Publisher publisher)
        {
            var entry = _context.Publishers.Update(publisher);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeletePublisher(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.Publisherid == id);
            if (book != null)
                return false;

            var publisher = _context.Publishers.FirstOrDefault(c=> c.Id == id);
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return true;

        }

    }

}
