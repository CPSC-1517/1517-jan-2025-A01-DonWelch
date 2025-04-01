
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region setup the context connection variable and class constructor
        private readonly WestWindContext _context;

        //constructor to be used in the creation of the instance of this class
        //the registered reference for the context connection (database connection)
        //  will be passed from the IServiceCollection registered services
        internal ProductServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Queries

        //to retreive products for a particular category
        public List<Product> Product_GetByCategoryID(int categoryid)
        {
            //IEnumerable<Product> info = _context.Products
            //                                    .Where(x => x.CategoryID ==  categoryid)
            //                                    .OrderBy(x => x.ProductName);

            //return info.ToList();

            //alternative
            List<Product> info = _context.Products
                                                .Where(x => x.CategoryID ==  categoryid)
                                                .OrderBy(x => x.ProductName)
                                                .ToList();
            return info;
        }

        //if you wish to use the Include technique to obtain the Supplier company name
        //public List<Product> Product_GetByCategoryID(int categoryid)
        //{
        //    List<Product> info = _context.Products
        //                                        .Include(x => x.Supplier)
        //                                        .Where(x => x.CategoryID ==  categoryid)
        //                                        .OrderBy(x => x.ProductName)
        //                                        .ToList();
        //    return info;
        //}

        public Product Product_GetByID(int productid)
        {
            Product info = _context.Products
                                    .FirstOrDefault(x => x.ProductID == productid);
            return info;

            //alternative
            //.Find search using ONLY the pkey value
            //return _context.Products.Find(productid);
        }
        #endregion
    }
}
