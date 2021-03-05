using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI_0.Middleware.ApiRespondeMiddleware
{
    public class APIResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public APIResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await this._next(context);
            }                
            else
            {
                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    try
                    {
                        await _next.Invoke(context);

                        // se non ci sono errori wrappa la risposta
                        
                        if (context.Response.StatusCode != (int)HttpStatusCode.InternalServerError && context.Response.StatusCode != (int)HttpStatusCode.BadRequest)
                        {
                            var body = await FormatResponse(context.Response);
                            string bodyText = string.Empty;
                            if (!body.ToString().IsValidJson())
                                bodyText = JsonConvert.SerializeObject(body);
                            else
                                bodyText = body.ToString();
                            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);
                            await HandleSuccessRequestAsync(context, bodyContent, context.Response.StatusCode);
                        }
                        else
                        {
                            //nothing
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }

        }
                

        private static Task HandleSuccessRequestAsync(HttpContext context, object bodyContent, int code)
        {
            context.Response.ContentType = "application/json";
            string jsonString;
            ApiResponseModel apiResponse = null;
                                    
            apiResponse = new ApiResponseModel(code, true, null, bodyContent);
            jsonString = apiResponse.ToString();// JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(jsonString);
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return plainBodyText;
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");

        }

    }
}
