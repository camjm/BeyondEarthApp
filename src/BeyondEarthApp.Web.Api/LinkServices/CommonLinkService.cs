﻿using System;
using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common.Extensions;
using BeyondEarthApp.Web.Common.Security;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class CommonLinkService : ICommonLinkService
    {
        private readonly IWebUserSession _userSession;

        public CommonLinkService(IWebUserSession userSession)
        {
            _userSession = userSession;
        }

        /// <summary>
        /// Creates the Link Uri by prepending a versioned base path prefix to the specified path fragment
        /// </summary>
        public virtual Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod)
        {
            // use _userSession.RequestUri.GetBaseUri() instead?
            const string delimitedVersionedApiRouteBaseFormatString =
                Constants.CommonRoutingDefinitions.ApiSegmentName + "/{0}/";

            var path = string.Concat(
                string.Format(
                    delimitedVersionedApiRouteBaseFormatString, 
                    _userSession.ApiVersionInUse),
                pathFragment);

            // construct a properly formed uri to assign to the Href of the link
            var uriBuilder = new UriBuilder
            {
                Scheme = _userSession.RequestUri.Scheme,
                Host = _userSession.RequestUri.Host,
                Port = _userSession.RequestUri.Port,
                Path = path
            };

            return GetLink(uriBuilder.Uri, relValue, httpMethod);
        }

        /// <summary>
        /// Factory method, creating an appropriate Link instance based on the specified Uri
        /// </summary>
        public virtual Link GetLink(Uri uri, string relValue, HttpMethod httpMethod)
        {
            var link = new Link
            {
                Href = uri.AbsoluteUri,
                Rel = relValue,
                Method = httpMethod.Method
            };

            return link;
        }

        public void AddPageLinks(
            IPageLinkContaining linkContainer, 
            string currentPageQueryString, 
            string previousPageQueryString,
            string nextPageQueryString)
        {
            var versionedBaseUri = _userSession.RequestUri.GetBaseUri();

            AddCurrentPageLink(linkContainer, versionedBaseUri, currentPageQueryString);

            var addPreviousPageLink = ShouldAddPreviousPageLink(linkContainer.PageNumber);
            if (addPreviousPageLink)
            {
                AddPreviousPageLink(linkContainer, versionedBaseUri, previousPageQueryString);
            }

            var addNextPageLink = ShouldAddNextPageLink(linkContainer.PageNumber, linkContainer.PageCount);
            if (addNextPageLink)
            {
                AddNextPageLink(linkContainer, versionedBaseUri, nextPageQueryString);
            }
        }

        public virtual void AddCurrentPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var currentPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };

            var currentPageLink = GetLink(
                currentPageUriBuilder.Uri, 
                Constants.CommonLinkRelValues.CurrentPage, 
                HttpMethod.Get);
            linkContainer.AddLink(currentPageLink);
        }

        public virtual void AddPreviousPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var previousPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };

            var previousPageLink = GetLink(
                previousPageUriBuilder.Uri,
                Constants.CommonLinkRelValues.PreviousPage,
                HttpMethod.Get);
            linkContainer.AddLink(previousPageLink);
        }

        public virtual void AddNextPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var nextPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };

            var nextPageLink = GetLink(
                nextPageUriBuilder.Uri,
                Constants.CommonLinkRelValues.NextPage,
                HttpMethod.Get);
            linkContainer.AddLink(nextPageLink);
        }

        public bool ShouldAddPreviousPageLink(int pageNumber)
        {
            return pageNumber > 1;
        }

        public bool ShouldAddNextPageLink(int pageNumber, int pageCount)
        {
            return pageNumber < pageCount;
        }
    }
}