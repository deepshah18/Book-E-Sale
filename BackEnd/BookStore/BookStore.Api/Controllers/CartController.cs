using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository = new CartRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetCartItems(string keyword)
        {
            List<Cart> carts = _cartRepository.GetCartItems(keyword);
            IEnumerable<CartBook> cartBook = carts.Select(c => new CartBook(c));
            //IEnumerable<CartBook> cartBook = carts.Select(c => new CartBook(c));
            return Ok(cartBook);
        }
        //Latest is this one do changes in this ***************************************************************8
        /* [HttpGet]
         [Route("list/{id}")]
         public IActionResult GetCart(int id)
         {
             var carts = _cartRepository.GetCarts(id);

             ListResponse<CartModel> listResponse = new ListResponse<CartModel>()
             {
                 Results = carts.Results.Select(c => new CartModel(c)),
                 TotalRecords = carts.TotalRecords,
             };

             return Ok(listResponse);
         }*/
        //*************************************************************************************************88

        [HttpGet]
        [Route("list/{id}")]
        public BaseList<CartResponse> GetAll(int id)
        {
            BaseList<CartResponse> cart = _cartRepository.GetAll(id);
            return cart;
            //return Ok(cart);
        }

        /*  public IActionResult GetCarts(int id)
          {
              if (id == 0)
                  return BadRequest("Id Must Not Be Zero");
              var c = _cartRepository.GetCarts(id);
             *//* if(carts == null)
                  return BadRequest("Cart is null");*//*
              CartModel cartModel = new CartModel(c);
              //IEnumerable<CartBook> cartBook = carts.Select(c => new CartBook(c));
              return Ok(cartModel);
          }*/

        [HttpPost]
        [Route("add")]
        public IActionResult AddCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.Bookid,
                Userid = model.Userid
            };
            cart = _cartRepository.AddCart(cart);
            CartModel cartModel = new CartModel(cart);
            return Ok(cartModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.Bookid,
                Userid = model.Userid
            };
            cart = _cartRepository.UpdateCart(cart);
            CartModel cartModel = new CartModel(cart);
            return Ok(cartModel);
        }

        [HttpDelete]
        [Route("delete/{id}")] //"/{id}"
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
                return BadRequest();

            bool response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }


    }
}
