using Aafp.Also.Api.Daos.Commands.Interfaces;
using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Helpers;
using Aafp.Also.Api.Models;
using Aafp.Also.Api.Tasks.Interfaces;
using System;

namespace Aafp.Also.Api.Tasks
{
    public class DiscountTasks : IDiscountTasks
    {
        public IActivityQuery ActivityQuery { get; set; }

        public IProductQuery ProductQuery { get; set; }

        public IDiscountQuery DiscountQuery { get; set; }

        public IDiscountCommand DiscountCommand { get; set; }

        public IDiscountAttributeCommand DiscountAttributeCommand { get; set; }

        public Guid CreateDiscount(DiscountDto discount)
        {
            var prcKey = new Guid();

            var activity = ActivityQuery.GetActivity(discount.ActivityNumber);
            var activityDiscount = DiscountQuery.GetDiscount(discount.ActivityNumber);

            if (activityDiscount != Guid.Empty)
            {
                prcKey = UpdateDiscount(activity, discount.WebLogin, activityDiscount);
            }
            else
            {
                prcKey = AddDiscount(activity, discount.WebLogin);
            }

            return prcKey;
        }

        public Guid AddDiscount(ActivityDto activity, string webLogin)
        {
            var prcKey = new Guid();

            try
            {
                var product = new ProductDto();

                var price = new Price
                {
                    AddDate = DateTime.Now,
                    AddUser = webLogin,
                    PriceCode = activity.ActivityNumber,
                    PriceAmount = 0.00M,
                    PricePercent = 100.0000M,
                    PriceStartDate = DateTime.Now,
                    PriceEndDate = activity.ActivityEndDate.AddDays(1),
                    PriceEwebCode = activity.ActivityNumber,
                    RenewUnpaidOrdersFlag = true,
                    AllowUnpaidOrdersFlag = true,
                };

                if (activity.ActivityCourseType == "ALSO Provider")
                {
                    product = ProductQuery.GetProduct(ApplicationConfig.ALSOProviderProduct);

                    price.ProductKey = product.ProductKey;
                    price.ProductTypeKey = product.ProductTypeKey;
                    price.ProductCompanyKey = product.ProductCompanyKey;
                    price.PriceDisplayName = product.ProductName;
                    price.PriceRevenueKey = ApplicationConfig.ALSOProviderBLSORevenueKey;
                }

                if (activity.ActivityCourseType == "ALSO Instructor")
                {
                    product = ProductQuery.GetProduct(ApplicationConfig.ALSOProviderProduct);

                    price.ProductKey = product.ProductKey;
                    price.ProductTypeKey = product.ProductTypeKey;
                    price.ProductCompanyKey = product.ProductCompanyKey;
                    price.PriceDisplayName = product.ProductName;
                    price.PriceRevenueKey = ApplicationConfig.ALSOInstructorRevenueKey;
                }
                if (activity.ActivityCourseType == "BLSO Provider")
                {
                    product = ProductQuery.GetProduct(ApplicationConfig.ALSOProviderProduct);

                    price.ProductKey = product.ProductKey;
                    price.ProductTypeKey = product.ProductTypeKey;
                    price.ProductCompanyKey = product.ProductCompanyKey;
                    price.PriceDisplayName = product.ProductName;
                    price.PriceRevenueKey = ApplicationConfig.ALSOProviderBLSORevenueKey;
                }

                DiscountCommand.Store(price);

                var priceAttribute = new PriceAttribute
                {
                    AddDate = DateTime.Now,
                    AddUser = webLogin,
                    Code = activity.ActivityNumber,
                    PriceKey = price.Key
                };

                DiscountAttributeCommand.Store(priceAttribute);

                prcKey = price.Key;
            }

            catch (Exception ex)
            {
                var message = $"Unable to add discount for activity: {activity.ActivityNumber}, User: {webLogin}, Error: {ex.Message}.";
                Logger.LogError(message);
            }

            return prcKey;
        }

        public Guid UpdateDiscount(ActivityDto activity, string webLogin, Guid priceKey)
        {
            var price = new Price();

            price = DiscountCommand.GetByKey(priceKey);

            price.PriceStartDate = DateTime.Now;
            price.PriceEndDate = activity.ActivityEndDate.AddDays(1);
            price.ChangeDate = DateTime.Now;
            price.ChangeUser = webLogin;

            DiscountCommand.Store(price);

            return priceKey;
        }
    }
}