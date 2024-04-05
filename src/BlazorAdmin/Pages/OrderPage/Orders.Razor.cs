using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Pages.CatalogItemPage;
using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.OrderPage;

public partial class Orders : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService{ get; set; }

    private List<Order> orders = new List<Order>();

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
}
