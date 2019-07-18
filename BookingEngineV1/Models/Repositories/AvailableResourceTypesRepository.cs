using BookingEngineV1.Models.DBQueries;
using BookingEngineV1.Models.DBViews;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class AvailableResourceTypesRepository : IAvailableResourceTypesRepository
    {
        private DataContext context;
        public AvailableResourceTypesRepository(DataContext ctx) => context = ctx;

    
        public List<OfferedResourceType> GetOfferedResourceTypes(Inquiry inquiry)
        {
            IQueryable<CreateBookingDisplayAll> allLines;
            List<OfferedRateComposition> offeredRateCompositions = new List<OfferedRateComposition>();
            List<OfferedResourceType> offeredResourceTypes = new List<OfferedResourceType>();

            string sDateOfBooking = inquiry.DateOfInquiry.ToString("yyyy-MM-dd");
            string sDateOfArrival = inquiry.DateOfArrival.ToString("yyyy-MM-dd");
            string sDateOfDeparture = inquiry.DateOfDeparture.ToString("yyyy-MM-dd");

            SqlParameter pDateOfBooking = new SqlParameter("@DateOfBooking", sDateOfBooking);
            SqlParameter pDateOfArrival = new SqlParameter("@DateOfArrival", sDateOfArrival);
            SqlParameter pDateOfDeparture = new SqlParameter("@pDateOfDeparture", sDateOfDeparture);
            SqlParameter pCompanyID = new SqlParameter("@CompanyID", inquiry.CompanyID);

            string sqlAllLines = $@"SELECT *
                From fnResourceTypeRateSummaryOffer({pDateOfBooking}, {pDateOfArrival}, {pDateOfDeparture}, {pCompanyID}) V
                option(maxrecursion 0)";

            allLines = context.CreateBookingDisplayAllLines.FromSql(sqlAllLines, pDateOfBooking, pDateOfArrival, pDateOfDeparture, pCompanyID);

            // CREATE THE COLLECTION offeredResourceTypes
            foreach (CreateBookingDisplayAll line in allLines)
            {
                bool rtExists = offeredResourceTypes.Any(x => x.ResourceTypeID == line.ResourceTypeID);
                if (!rtExists)
                {
                    OfferedResourceType rt = new OfferedResourceType
                    {
                        CompanyID = line.CompanyID,
                        ResourceTypeID = line.ResourceTypeID,
                        ResourceTypeName = line.ResourceTypeName,
                        UnitsLeftForSale = line.UnitsLeftForSale
                    };
                    offeredResourceTypes.Add(rt);
                }
            }

            // CREATE THE COLLECTION  offeredRateCompositions
            foreach (CreateBookingDisplayAll line in allLines)
            {
                OfferedRateComposition rc = new OfferedRateComposition
                {
                    RateCompositionID = line.RateCompositionID,
                    RateCompositionName = line.RateCompositionName,
                    CancellationPolicyName = line.CancellationPolicyName,
                    ResourceTypeID = line.ResourceTypeID,
                    TotalPriceBeforePromotion = line.TotalPriceBeforePromotion,
                    TotalEarlyBookingPromotion = line.TotalEarlyBookingPromotion,
                    TotalLastMinutePromotion = line.TotalLastMinutePromotion,
                    TotalLongStayPromotion = line.TotalLongStayPromotion,
                    TotalPrice = line.TotalPrice,
                    AveragePricePerUnit = line.AveragePricePerUnit
                };
                offeredRateCompositions.Add(rc);
            }

            foreach (OfferedResourceType ort in offeredResourceTypes)
            {
                List<OfferedRateComposition> rcListForRT = new List<OfferedRateComposition>();

                foreach (OfferedRateComposition orc in offeredRateCompositions)
                {
                    if (orc.ResourceTypeID == ort.ResourceTypeID)
                    {
                        rcListForRT.Add(orc);
                    }
                }
                ort.OfferedRateCompositions = rcListForRT;
            }

            return offeredResourceTypes;
        }

        public List<ResourceTypeUnitsAvailableForSale> GetResourceTypesNumberOfUnitsAvailable(Inquiry inquiry)
        {
            string sDateFrom = inquiry.DateOfArrival.ToString("yyyy-MM-dd");
            string sDateTo = inquiry.DateOfDeparture.ToString("yyyy-MM-dd");
            string sql = $@"SELECT CompanyID, DateEffective, ResourceTypeID, UnitsInStock, UnitsBlocked, UnitsSold, UnitsAvailableForSale From fnResourceAvailableForSale('{inquiry.CompanyID}', '{sDateFrom}','{sDateTo}');";

            return context.ResourceTypeUnitsAvailableForSalesDateRange.FromSql(sql).ToList();
        }
    }
}
