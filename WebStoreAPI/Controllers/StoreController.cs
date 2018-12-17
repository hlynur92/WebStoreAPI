using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebStoreAPI.Objects;

namespace WebStoreAPI.Controllers
{
    //GetProducts
    //PostOrder
    [RoutePrefix("api/Store")]
    public class StoreController : ApiController
    {
        // GET: api/Store/products
        [Route("products")]
        public List<ProductDto> Get()
        {
            DBFacade db = new DBFacade();
            return db.GetProducts();
        }

        // POST: api/Store/order
        [Route("order")]
        public void Post(JObject jsonresult)
        {
            
            try
            {
                /*
                JObject results2 = JObject.Parse(jsonresult.ToString());
                var result = JsonConvert.DeserializeObject(jsonresult.ToString());
                OrderDto order = JsonConvert.DeserializeObject<OrderDto>(jsonresult.ToString());
                */

                OrderDto order = new OrderDto();

                order.OrderPrice = (double)jsonresult["_orderPrice"];
                order.Country = (string)jsonresult["_country"];
                order.Street = (string)jsonresult["_street"];
                order.PostalCode = (string)jsonresult["_postalCode"];
                Array products = jsonresult["_products"].ToArray();

                foreach (var product in products)
                {
                    var product2 = (ProductDto)product;
                    order.Products[0].ProductName = product2.ProductName;
                }

                //Object List ?
                order.CardType = (string)jsonresult["_cardType"];
                order.CardNumber = (string)jsonresult["_cardNumber"];
                order.CardName = (string)jsonresult["_cardName"];
                order.CardCVC = (string)jsonresult["_cardCVC"];
                order.CardExpiration = (string)jsonresult["_cardExpiration"];
                /*
                    private double _orderPrice;
                    private string _country;
                    private string _street;
                    private string _postalCode;
                    private List<ProductDto> _products;
                    private string _cardType;
                    private string _cardNumber;
                    private string _cardName;
                    private string _cardCVC;
                    private string _cardExpiration;
                */
                DBFacade db = new DBFacade();
                //db.StoreOrder(result);
            }
            catch (Exception err)
            {

                Console.Write(err.ToString());
            }
            
        }

        // GET: api/Store/test?id=5
        [Route("test")]
        public string Get(int id)
        {
            DBFacade db = new DBFacade();
            ProductDto product = new ProductDto("Black Hoodie", "cloths", "A Stylish Black Hoodie", 10.99, 1);
            ProductDto product2 = new ProductDto("Red Hoodie", "cloths", "A Stylish Red Hoodie", 9.99, 2);

            List<ProductDto> products = new List<ProductDto>();
            products.Add(product);
            products.Add(product2);

            OrderDto order = new OrderDto(10, "Denmark", "Hedelundvej 112", "6705", products,
                "CreditCard", "1111222212341234", "Hlynur Geir Agnarsson", "321", "2021");
            db.StoreOrder(order);

            return "Success!";
        }

        // PUT: api/Store/5
        public void Put(int id)
        {
        }

        // DELETE: api/Store/5
        public void Delete(int id)
        {
        }
    }
}
