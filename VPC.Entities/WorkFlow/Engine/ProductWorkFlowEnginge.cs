using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore;

namespace VPC.Entities.WorkFlow.Engine
{
    public static partial class WorkFlowEngine
    {
        public const string _registered = "ACB237D7-20CC-4A28-AE58-720A82E76844";
        public const string _approve = "C4AE2605-3B0F-4E3C-AA87-2D6B29FED34D";
        public const string _checkout = "A918B3B3-FE07-4FA2-A6FD-D701083407BD";
        public const string _checkin = "0D88EF5D-3F1A-4A83-B42A-320A1B2E1033";
        public const string _closeProduct = "5EF8039B-00C6-42D3-AADF-04AEB5EEF0BD";
        
        public const string _product = InfoType.Product;
        public const string _productManufacturedName = "Product";

        [WorkFlowModel(Name = _productManufacturedName, Context = _product, Key = "Registration", Status = "Registered", Description = "RegisteredProductWorkFlowDesc")]
        public static Guid Registered
        {
            get
            {
                return new Guid(_registered);
            }
        }

        [WorkFlowModel(Name = _productManufacturedName, Context = _product, Key = "Approve", Status = "Approved", Description = "ApprovedProductWorkFlowDesc")]
        public static Guid Approve
        {
            get
            {
                return new Guid(_approve);
            }
        }

        [WorkFlowModel(Name = _productManufacturedName, Context = _product, Key = "Checkout", Status = "Checkedout", Description = "CheckedoutProductWorkFlowDesc")]
        public static Guid Checkout
        {
            get
            {
                return new Guid(_checkout);
            }
        }

        [WorkFlowModel(Name = _productManufacturedName, Context = _product, Key = "Checkin", Status = "Toapproval", Description = "ToapprovalProductWorkFlowDesc")]
        public static Guid Checkin
        {
            get
            {
                return new Guid(_checkin);
            }
        }

        [WorkFlowModel(Name = _productManufacturedName, Context = _product, Key = "Close", Status = "Closed", Description = "ClosedProductWorkFlowDesc")]
        public static Guid CloseProduct
        {
            get
            {
                return new Guid(_closeProduct);
            }
        }
    }
}
