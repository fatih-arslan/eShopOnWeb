using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Pages.CatalogItemPage;
using BlazorAdmin.Services;
using BlazorShared;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.OrderPage;

public partial class Orders : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService{ get; set; }

    private List<Order> orders = new List<Order>();
    private OrderItems orderItems;
    private bool showDetailsModal = false;
    private bool showUpdateResultModal = false;
    private string updateResult;
   

    protected override async Task OnInitializedAsync()
    {
        await ReloadOrders();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orders = await OrderService.ListPaged();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task ReloadOrders()
    {
        orders = await OrderService.ListPaged();
        StateHasChanged();
    }

    private async Task<OrderItems> GetOrderWithItems(int orderId)
    {
        var order = await OrderService.GetOrderItems(orderId);
        orderItems = order;
        StateHasChanged();
        return order;
    }

    private async Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest request)
    {
        var response = await OrderService.UpdateOrder(request);
        updateResult = response.Success ? $"Order status changed successfully." : "Couldn't update order status. Please try again later.";
        return response;
    }

    private async Task ShowOrderDetails(int orderId)
    {
        await GetOrderWithItems(orderId);
        showDetailsModal = true; 
    }

    private void CloseDetailsModal()
    {
        showDetailsModal = false;
    }

    void ShowUpdateResultModal()
    {
        showUpdateResultModal = true;
    }

    void CloseUpdateResultModal()
    {
        showUpdateResultModal = false;
    }
}
