using _0_Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory commnad);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduse(ReduceInventory command);
        OperationResult Reduse(List<ReduceInventory> command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
