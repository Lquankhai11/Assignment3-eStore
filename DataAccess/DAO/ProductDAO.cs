using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetListProducts()
        {
            var listProducts = new List<Product>();
            using (var db = new FStoreContext())
            {
                listProducts = db.Products.ToList();
            }
            return listProducts;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            using (var db = new FStoreContext())
            {
                product = db.Products.Find(id);
            }
            return product;
        }

        

        public Product GetProductByID(int productID)
        {
            Product product = null;
            try
            {
                using var context = new FStoreContext();
                product = context.Products.SingleOrDefault(p => p.ProductId == productID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }

        public void InsertProduct(Product product)
        {
            Product checkProduct = GetProductById(product.ProductId);
            if (checkProduct == null)
            {
                using (var db = new FStoreContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Product exists already!");
            }
        }

        public void UpdateProduct(Product product)
        {
            Product check = GetProductById(product.ProductId);
            if (check != null)
            {
                using (var db = new FStoreContext())
                {
                    db.Products.Update(product);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Product does not exist!");
            }
        }

        public void RemoveProduct(int id)
        {
            Product check = GetProductById(id);
            if (check != null)
            {
                using (var db = new FStoreContext())
                {
                    db.Products.Remove(check);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Product does not exist!");
            }
        }



        public List<Product> GetProductsByUnitPrice_UnitInStock(int unitPrice, int unitslnStock)
        {
            List<Product> list = new List<Product>();
            using (var db = new FStoreContext())
            {
                list = db.Products.Where(pro => pro.UnitPrice == unitPrice && pro.UnitsInStock == unitslnStock).ToList();
            }
            return list;
        }

 
        public static List<Product> getProductByUnitPrice(string unitPrice)
        {
            List<Product> listPro = null;
            try
            {
                using (var dbContext = new FStoreContext())
                {
                    listPro = dbContext.Products.Where(product => product.UnitPrice.ToString().Contains(unitPrice.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPro;
        }

        public static List<Product> getProductByUnitsSlnStock(string unitSlnStock)
        {
            List<Product> listPro = null;
            try
            {
                using (var dbContext = new FStoreContext())
                {
                    listPro = dbContext.Products.Where(product => product.UnitsInStock.ToString().Contains(unitSlnStock.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPro;
        }

        public static List<Product> getProductByName(string productName)
        {
            List<Product> listPro = null;
            try
            {
                using (var context = new FStoreContext())
                {
                    listPro = context.Products.Where(pro => pro.ProductName.ToLower().Contains(productName.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPro;
        }
    }
}
