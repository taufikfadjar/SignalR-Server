// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.SignalR.Hosting;

namespace Microsoft.AspNet.SignalR.Hubs
{
    public class HubCallerContext
    {
        /// <summary>
        /// Gets the connection id of the calling client.
        /// </summary>
        public virtual string ConnectionId { get; private set; }

        /// <summary>
        /// Gets the cookies for the request.
        /// </summary>
        public virtual IReadableStringCollection RequestCookies
        {
            get
            {
                return Request.Cookies;
            }
        }

        /// <summary>
        /// Gets the headers for the request.
        /// </summary>
        public virtual IReadableStringCollection Headers
        {
            get
            {
                return Request.Headers;
            }
        }

        /// <summary>
        /// Gets the querystring for the request.
        /// </summary>
        public virtual IReadableStringCollection QueryString
        {
            get
            {
                return Request.Query;
            }
        }

        /// <summary>
        /// Gets the <see cref="IPrincipal"/> for the request.
        /// </summary>
        public virtual IPrincipal User
        {
            get
            {
                return HttpContext.User;
            }
        }

        /// <summary>
        /// Gets the <see cref="HttpRequest"/> for the current HTTP request.
        /// </summary>
        public virtual HttpRequest Request
        {
            get
            {
                return HttpContext.Request;
            }

        }

        /// <summary>
        /// Gets the <see cref="HttpContext"/> for the current HTTP request.
        /// </summary>
        public virtual HttpContext HttpContext { get; private set; }

        /// <summary>
        /// This constructor is only intended to enable mocking of the class. Use of this constructor 
        /// for other purposes may result in unexpected behavior.   
        /// </summary>
        protected HubCallerContext() { }

        public HubCallerContext(HttpContext context, string connectionId)
        {
            ConnectionId = connectionId;
            HttpContext = context;
        }
    }
}
