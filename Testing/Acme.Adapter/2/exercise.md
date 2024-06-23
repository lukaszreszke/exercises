## Exercise

Take a look SupplierARestApiClient and at SupplierBSoapAdapter classes.
Both of them return the data in different format. The goal of this exercise is to create two
adapters that will convert the data from SupplierARestApiClient and SupplierBSoapAdapter to the
the format of `InventoryItem` class. The Adapters should implement the IInventoryService interface.

Besides implementing adapters, implement the facade class that will connect both adapters and return
the data in the format of `InventoryItem` class.

Write tests confirming that everything works fine. Consider carefully at which layer you should write the tests.