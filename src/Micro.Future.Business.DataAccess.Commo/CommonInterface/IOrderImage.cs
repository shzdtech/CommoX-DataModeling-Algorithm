using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IOrderImage
    {
        bool BulkSaveOrderImages(IList<OrderImage> imageList);

        IList<OrderImage> QueryOrderImages(int orderId);

        IList<OrderImage> QueryOrderImagesByType(int orderId, int imageTypeId);

        OrderImage QueryOrderImageInfo(int imageId);

        OrderImage CreateOrderImage(OrderImage newImage);

        bool UpdateOrderImage(OrderImage image);

        bool DeleteOrderImage(int imageId);
    }
}
