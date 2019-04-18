using Hex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hex.ViewModel
{
    public class ResultViewModel
    {
        #region Fields
        
        private ResponseModel response;
        private ICommand getResponseCommand;        

        #endregion

        #region Public Properties/Commands

        public ResponseModel CurrentProduct
        {
            get { return response; }            
        }
        

        public ICommand GetResponseCommand
        {
            get
            {
                if (getResponseCommand == null)
                {
                    getResponseCommand = new RelayCommand(
                        param => GetProduct(),
                        param => ProductId > 0
                    );
                }
                return getResponseCommand;
            }
        }
       
        #endregion

        #region Private Helpers

        private void GetProduct()
        {
            // You should get the product from the database
            // but for now we'll just return a new object
            //ProductModel p = new ProductModel();
            //p.ProductId = ProductId;
            //p.ProductName = "Test Product";
            //p.UnitPrice = 10.00;
            //CurrentProduct = p;
        }
        

        #endregion
    }
}
