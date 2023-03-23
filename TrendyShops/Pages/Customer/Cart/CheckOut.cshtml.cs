using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrendyShops.DataAccess.DbInitializer;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using TrendyShops.Model;
using TrendyShops.DataAccess.Data;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using TrendyShops.Utility;
using Stripe.Checkout;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace TrendyShops.Pages.Customer.Cart
{
    public class CheckOutModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public double CartTotal { get; set; }
        public double FinalPrice { get; set; }
        public double CurrentPrices { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public CheckOutModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
            FinalPrice = 0;
            CurrentPrices = 0;
            OrderHeader = new OrderHeader();
        }
        [BindProperty]
        public IList<TrendyShops.Model.ShoppingCart> ShoppingCart { get; set; }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; private set; }
        public List<User_Address> User_Address { get; private set; }
        [BindProperty]
        public ShoppingCart ShoppingCartss { get; set; }
        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public IEnumerable<TrendyShops.Model.ProCatSub> ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();
            if (claim != null)
            {
                ShoppingCartss = _unitOfWork.ShoppingCart.GetFirstOrDefault(filter: u => u.ApplicationUserId == UserId);

                ShoppingCart = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product,ProductColors,ProductSizes").ToList();
                foreach (var cartItem in ShoppingCart)
                {
                    CartTotal += (cartItem.Product.CurrnetPrice * cartItem.Count);
                    FinalPrice = CartTotal + 5;
                    CurrentPrices = (cartItem.Product.CurrnetPrice * cartItem.Count);
                }
                User_Address = _unitOfWork.User_Address.GetAll(filter: u => u.UserId == UserId).ToList();
                ViewData["User_Address"] = new SelectList(User_Address, "Id", "Address", "PostalCode", "ExtraDetailes");
            }
            if (UserId == null)
            {
                ViewData["Count"] = 0;
            }

            if (UserId != null)
            {
                ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");
                ViewData["Count"] = ShoppingCarts.Count();
            }


        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string PickupName, string Phone_Number, DateTime PickUpDate, DateTime PickUpTime, int User_AddressId, string Comments,double CartTotal)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
           
            ViewData["PickupName"] = PickupName;
  
            if (claim != null)
            {
                ShoppingCart = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product,ProductColors,ProductSizes").ToList();

                foreach (var cartItem in ShoppingCart)
                {
                     CartTotal += (cartItem.Product.CurrnetPrice * cartItem.Count) + 8;
                }
                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderTotal = CartTotal;
                //OrderHeader.ShoppingCartId = cartItem.Id;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.ApplicationUserId = UserId;
                OrderHeader.PickupName = PickupName;
                OrderHeader.Phone_Number = Phone_Number;
                OrderHeader.User_AddressId = User_AddressId;
                 OrderHeader.Comments = Comments;

                OrderHeader.PickUpTime = Convert.ToDateTime(PickUpDate.ToShortDateString() + " " +
                PickUpTime.ToShortTimeString());

                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCart)
                {
                    OrderDetails orderDetails = new()
                    {
                        ProductId = item.ProductId,
                        OrderId = OrderHeader.Id,
                        ProductColorId = item.ProductColorsId,
                        ProductSizeId = item.ProductSizesId,
                        ProductName = item.Product.Name,
                        ProductPrice = item.Product.CurrnetPrice,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetail.Add(orderDetails);

                }
                

                //_unitOfWork.ShoppingCart.RemoveRange(ShoppingCart);
                _unitOfWork.Save();


                var domain = "https://localhost:7243/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>()
                ,
                    PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                    Mode = "payment",
                    SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "Customer/Cart/Index",
                };

                //add line items
                foreach (var item in ShoppingCart)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            //7.99->799
                            UnitAmount = (long)(item.Product.CurrnetPrice * 100 + 500),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            },
                            
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }



                var service = new SessionService();
                Session session = service.Create(options);
                Response.Headers.Add("Location", session.Url);

                OrderHeader.SessionId = session.Id;
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
                _unitOfWork.Save();
                return new StatusCodeResult(303);
            }

            return Page();
        }
    }
}
