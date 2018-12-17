using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using WebStoreAPI.Objects;

namespace WebStoreAPI
{
    public class DBFacade
    {
        private List<int> productIDs;
        public DBFacade()
        {
            productIDs = new List<int>();
        }

        public List<ProductDto> GetProducts()
        {
            MySqlConnection connection;
            string server = "localhost";
            string database = "dewebstore2";
            string uid = "admin";
            string password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            
            try
            {
                using (connection = new MySqlConnection(connectionString))
                {
                    List<ProductDto> products = new List<ProductDto>();

                    connection.Open();

                    MySqlCommand command = new MySqlCommand("GetProducts2", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ProductDto product = new ProductDto(
                                reader.GetString(1), 
                                reader.GetString(4), 
                                reader.GetString(2), 
                                reader.GetDouble(3), 
                                0);

                            products.Add(product);
                        }
                    }

                    connection.Close();
                    return products;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StoreOrder(OrderDto order)
        {
            MySqlConnection connection;
            string server = "localhost";
            string database = "dewebstore2";
            string uid = "admin";
            string password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try
            {
                using (connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StoreAddress(connection, order);
                    string lastAddressID = GetLastAddressID(connection);

                    StorePayment(connection, order);
                    string lastPaymentID = GetLastPaymentID(connection);

                    StoreOrderHistory(connection, order, lastAddressID, lastPaymentID);
                    string lastOrderID = GetLastOrderID(connection);

                    foreach (var product in order.Products)
                    {
                        productIDs = GetProductIDs(order, product, connection);
                    }

                    StoreOrderLines(connection, order, lastOrderID);

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void StoreOrderLines(MySqlConnection connection, OrderDto order, string lastOrderID)
        {
            //OrderLines
            for (int i = 0; i < productIDs.Count; i++)
            {
                MySqlCommand command = new MySqlCommand("StoreOrderLines2", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("OrderID", lastOrderID);
                command.Parameters.AddWithValue("ProductID", productIDs[i]);
                command.Parameters.AddWithValue("Quantity", order.Products[i].ProductQuantity);
                command.ExecuteNonQuery();
            }
        }

        private void StoreOrderHistory(MySqlConnection connection, OrderDto order, string lastAddressID, string lastPaymentID)
        {
            MySqlCommand command = new MySqlCommand("StoreOrder2", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Order
            command.Parameters.AddWithValue("OrderPrice", order.OrderPrice);
            command.Parameters.AddWithValue("AddressID", lastAddressID);
            command.Parameters.AddWithValue("PaymentID", lastPaymentID);
            command.ExecuteNonQuery();
        }

        private void StorePayment(MySqlConnection connection, OrderDto order)
        {
            MySqlCommand command = new MySqlCommand("StorePayment2", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Payment
            command.Parameters.AddWithValue("CardType", order.CardType);
            command.Parameters.AddWithValue("CardNumber", order.CardNumber);
            command.Parameters.AddWithValue("CardName", order.CardName);
            command.Parameters.AddWithValue("CardCVC", order.CardCVC);
            command.Parameters.AddWithValue("CardExpiration", order.CardExpiration);
            command.ExecuteNonQuery();
        }

        private void StoreAddress(MySqlConnection connection, OrderDto order)
        {
            MySqlCommand command = new MySqlCommand("StoreAddress2", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Address
            command.Parameters.AddWithValue("Country", order.Country);
            command.Parameters.AddWithValue("Street", order.Street);
            command.Parameters.AddWithValue("PostalCode", order.PostalCode);
            command.ExecuteNonQuery();
        }

        private List<int> GetProductIDs(OrderDto order, ProductDto product, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand("GetProductID2", connection);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataReader reader = null;

            //Products
            command.Parameters.AddWithValue("ProductName", product.ProductName);
            command.Parameters.AddWithValue("ProductDesc", product.ProductDesc);
            command.Parameters.AddWithValue("ProductPrice", product.ProductPrice);
            command.Parameters.AddWithValue("ProductType", product.ProductType);

            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                productIDs.Add(reader.GetInt32(0));
            }
            /*
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productIDs.Add(reader.GetInt32(0));
                    }
                }
                */
            reader.Close();

            return productIDs;
        }

        private string GetLastOrderID(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand("GetLastOrderID2", connection);
            command.CommandType = CommandType.StoredProcedure;
            string lastOrderID = "";

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lastOrderID = reader.GetInt32(0).ToString();
                }
            }
            reader.Close();
            return lastOrderID;
        }

        private string GetLastPaymentID(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand("GetLastPaymentID2", connection);
            command.CommandType = CommandType.StoredProcedure;
            string lastPaymentID = "";

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lastPaymentID = reader.GetInt32(0).ToString();
                }
            }

            reader.Close();
            return lastPaymentID;
        }

        private string GetLastAddressID(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand("GetLastAddressID2", connection);
            command.CommandType = CommandType.StoredProcedure;
            string lastAddressID = "";

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lastAddressID = reader.GetInt32(0).ToString();
                }
            }

            reader.Close();
            return lastAddressID;
        }
    }
}