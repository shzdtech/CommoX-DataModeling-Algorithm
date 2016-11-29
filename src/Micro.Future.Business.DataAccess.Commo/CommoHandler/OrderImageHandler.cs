using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class OrderImageHandler : IOrderImage
    {
        private CommoXContext db = null;
        public OrderImageHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }

        public bool BulkSaveOrderImages(IList<OrderImage> imageList)
        {
            if (imageList == null || imageList.Count == 0)
                return false;
            foreach (var image in imageList)
            {
                db.OrderImages.Add(image);
            }
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public OrderImage CreateOrderImage(OrderImage newImage)
        {
            db.OrderImages.Add(newImage);
            int result = db.SaveChanges();
            if (result > 0)
                return newImage;
            else
                return null;
        }

        public bool DeleteOrderImage(int imageId)
        {
            var image = QueryOrderImageInfo(imageId);
            db.OrderImages.Remove(image);
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public OrderImage QueryOrderImageInfo(int imageId)
        {
            return db.OrderImages.FirstOrDefault(p => p.ImageId == imageId);
        }

        public IList<OrderImage> QueryOrderImages(int orderId)
        {
            return db.OrderImages.Where(f=> f.OrderId == orderId).ToList();
        }

        public IList<OrderImage> QueryOrderImagesByType(int orderId, int imageTypeId)
        {
            return db.OrderImages.Where(f => f.OrderId == orderId && f.ImageTypeId == imageTypeId).ToList();
        }

        public bool UpdateOrderImage(OrderImage image)
        {
            var orderImage = QueryOrderImageInfo(image.ImageId);
            if (orderImage != null)
            {
                orderImage.Position = image.Position;
                orderImage.ImagePath = image.ImagePath;
                orderImage.ImageTypeId = image.ImageTypeId;
                orderImage.UpdateTime = DateTime.Now;
            }
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
