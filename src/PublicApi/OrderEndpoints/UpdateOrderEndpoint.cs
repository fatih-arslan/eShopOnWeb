using System;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderEndpoint : IEndpoint<IResult, UpdateOrderRequest, IRepository<Order>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/orders/{orderId}",
          async (UpdateOrderRequest request, IRepository<Order> orderRepository) =>
          {

              return await HandleAsync(request, orderRepository);
          })
          .Produces<UpdateOrderResponse>()
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderRequest request, IRepository<Order> orderRepository)
    {
        var response = new UpdateOrderResponse(request.CorrelationId());
        var existingOrder = await orderRepository.GetByIdAsync(request.Id);
        if (existingOrder == null)
        {
            return Results.NotFound($"Order with ID {request.Id} not found.");
        }
        try
        {
            existingOrder.UpdateStatus(request.Status);
            await orderRepository.UpdateAsync(existingOrder);
            response.Success = true;
            return Results.Ok(response);
        }
        catch(Exception ex) 
        {
            response.Success = false;
            response.ErrorMessage = ex.Message;
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }                
    }
}
