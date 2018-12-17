using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStoreAPI.Objects
{
    public class OrderDto
    {
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

        public OrderDto(double orderPrice, string country, string street, string postalCode, List<ProductDto> products,
            string cardType, string cardNumber, string cardName, string cardCVC, string cardExpiration)
        {
            OrderPrice = orderPrice;
            Country = country;
            Street = street;
            PostalCode = postalCode;
            Products = products;
            CardType = cardType;
            CardNumber = cardNumber;
            CardName = cardName;
            CardCVC = cardCVC;
            CardExpiration = cardExpiration;
        }
        public OrderDto()
        {

        }

        public double OrderPrice
        {
            get
            {
                return _orderPrice;
            }

            set
            {
                _orderPrice = value;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }

            set
            {
                _country = value;
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }

            set
            {
                _street = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return _postalCode;
            }

            set
            {
                _postalCode = value;
            }
        }

        public List<ProductDto> Products
        {
            get
            {
                return _products;
            }

            set
            {
                _products = value;
            }
        }

        public string CardType
        {
            get
            {
                return _cardType;
            }

            set
            {
                _cardType = value;
            }
        }

        public string CardNumber
        {
            get
            {
                return _cardNumber;
            }

            set
            {
                _cardNumber = value;
            }
        }

        public string CardName
        {
            get
            {
                return _cardName;
            }

            set
            {
                _cardName = value;
            }
        }

        public string CardCVC
        {
            get
            {
                return _cardCVC;
            }

            set
            {
                _cardCVC = value;
            }
        }

        public string CardExpiration
        {
            get
            {
                return _cardExpiration;
            }

            set
            {
                _cardExpiration = value;
            }
        }

        public static implicit operator OrderDto(string v)
        {
            throw new NotImplementedException();
        }
    }
}