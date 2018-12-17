using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStoreAPI.Objects
{
    public class ProductDto
    {
        private string _productName;
        private string _productType;
        private string _productDesc;
        private double _productPrice;
        private int _productQuantity;

        public ProductDto(string productName, string productType, string productDesc, double productPrice, int productQuantity)
        {
            ProductName = productName;
            ProductType = productType;
            ProductDesc = productDesc;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                _productName = value;
            }
        }

        public string ProductType
        {
            get
            {
                return _productType;
            }

            set
            {
                _productType = value;
            }
        }

        public string ProductDesc
        {
            get
            {
                return _productDesc;
            }

            set
            {
                _productDesc = value;
            }
        }

        public double ProductPrice
        {
            get
            {
                return _productPrice;
            }

            set
            {
                _productPrice = value;
            }
        }

        public int ProductQuantity
        {
            get
            {
                return _productQuantity;
            }

            set
            {
                _productQuantity = value;
            }
        }
    }
}