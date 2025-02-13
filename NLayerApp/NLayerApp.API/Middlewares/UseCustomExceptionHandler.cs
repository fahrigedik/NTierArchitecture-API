﻿using Microsoft.AspNetCore.Diagnostics;
using NLayerApp.Core.DTOs;
using NLayerApp.Service.Exceptions;
using System.Text.Json;

namespace NLayerApp.API.Middleware
{
    //bir extensions yazmak için static sınıf kullanmalıyız. 
    public static class UseCustomExceptionHandler 
    {

        public static void UserCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundExcepiton => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode,exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });

        }
    }
}
