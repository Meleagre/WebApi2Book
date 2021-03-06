﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace WebApi2Book.Web.Common
{
    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            _configuration = config;
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(
                InitializeControllerDictionary);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            if (routeData == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var controllerName = GetControllerName(routeData);
            if (controllerName == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var namespaceName = GetVersion(routeData);
            if (namespaceName == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var controllerKey = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", 
                namespaceName, controllerName);

            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(controllerKey, out controllerDescriptor))
                return controllerDescriptor;

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(
                StringComparer.OrdinalIgnoreCase);

            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllerResolover = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllerResolover.GetControllerTypes(assembliesResolver);

            foreach (var controllerType in controllerTypes)
            {
                var segments = controllerType.Namespace.Split(Type.Delimiter);
                var controllerName = controllerType.Name.Remove(
                    controllerType.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
                var controllerKey = String.Format(CultureInfo.InvariantCulture, "{0}.{1}",
                    segments.Last(), controllerName);

                if (!dictionary.Keys.Contains(controllerKey))
                {
                    dictionary[controllerKey] = new HttpControllerDescriptor(
                        _configuration, controllerType.Name, controllerType);
                }
            }

            return dictionary;
        }

        private string GetControllerName(IHttpRouteData routeData)
        {
            var subroute = routeData.GetSubRoutes().FirstOrDefault();
            if (subroute == null) return null;

            var dataTokenValue = subroute.Route.DataTokens.First().Value as HttpActionDescriptor[];
            if (dataTokenValue == null) return null;

            var controllerName = dataTokenValue.First().ControllerDescriptor.ControllerName
                .Replace("Controller", String.Empty);
            return controllerName;
        }

        private string GetVersion(IHttpRouteData routeData)
        {
            var subRouteData = routeData.GetSubRoutes().FirstOrDefault();
            if (subRouteData == null) return null;
            return GetRouteVariable<string>(subRouteData, "apiVersion");
        }

        private T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result;
            return routeData.Values.TryGetValue(name, out result)
                ? (T)result : default(T);
        }
    }
}
