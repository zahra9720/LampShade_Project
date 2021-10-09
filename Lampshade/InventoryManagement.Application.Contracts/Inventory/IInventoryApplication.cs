using _0_Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory commnad);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduse(ReduseInventory command);
        OperationResult Reduse(List<ReduseInventory> command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
