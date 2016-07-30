﻿using BeyondEarthApp.Data;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    interface IAllBuildingsProcessor
    {
        PagedDataInquiryResponse<BuildingPrecis> GetBuildings(PagedDataRequest requestInfo);
    }
}
