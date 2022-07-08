using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class CartBook
    {
        public CartBook() { }
        public CartBook(Cart cart)
        {
            Id = cart.Id;
            Userid = cart.Userid;
            Bookid = cart.Bookid;
            Quantity = cart.Quantity;
            BookName = cart.Book.Name;
            Price = cart.Book.Price;

        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public int Bookid { get; set; }
        public int Quantity { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }   
    }
}
