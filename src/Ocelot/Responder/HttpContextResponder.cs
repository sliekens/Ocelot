﻿using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ocelot.Responder
{
    /// <summary>
    /// Cannot unit test things in this class due to methods not being implemented
    /// on .net concretes used for testing
    /// </summary>
    public class HttpContextResponder : IHttpResponder
    {
        public async Task<HttpContext> CreateResponse(HttpContext context, HttpResponseMessage response)
        {
            context.Response.OnStarting(x =>
            {
                context.Response.StatusCode = (int)response.StatusCode;
                return Task.CompletedTask;
            }, context);

            await context.Response.WriteAsync(await response.Content.ReadAsStringAsync());
            return context;       
        }

        public async Task<HttpContext> CreateErrorResponse(HttpContext context, int statusCode)
        {
            context.Response.OnStarting(x =>
            {
                context.Response.StatusCode = statusCode;
                return Task.CompletedTask;
            }, context);
            return context;
        }
    }
}