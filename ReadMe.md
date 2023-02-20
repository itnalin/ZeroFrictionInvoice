- Project architecture is defined according to the Domain Driven Design. The following layers are included in the architecture.
     1.ZeroFrictionInvoice.API
       This is the application layer (API Layer) that exposes REST APIs to the frontend.
       API layer directly references ZeroFrictionInvoice.Domain.

     2.ZeroFrictionInvoice.Domain
       This is the core layer of the project that contains the business logic of the system. According to the invoice, all the domain related business logic 
       functionalities are add in the Invoice class that implements the IInvoiceService interface.Domain layer directly references ZeroFrictionInvoice.Infra.
              
     3.ZeroFrictionInvoice.Models
       This layer defines the all the relevant models to the domain. Ex: Invoice, InvoiceLine

     4.ZeroFrictionInvoice.Infra
       This layer's responsibility is to handle the Db layer that defines the InvoiceContext connecting with Cosmos DB.
       ZeroFrictionInvoice.Infra directly refences ZeroFrictionInvoice.Models.

 - The API layer requests and responds data through DTO model objects which are defined in the ZeroFrictionInvoice.Models layer. The main focus of these DTOs
   is to stop exposing Db Context models to presentation layer. Since there is data mapping part in between DTOs and Models, That has been addressed by
   using AutoMapper.

 - Exception Handling is implemented through the middleware. ZeroFrictionInvoice.Domain layer defines the BusinessException (derived by Exception), business related errors
 and errorcodes.

 - Invoice model adds an attibute "InvoiceNumber" that is used to uniquely identify an invoice. It assumes that duplicate invoice number should not be allowed
  to create.

 - Assume that "Amount" field in InvoiceLine holds the same value of "LineAmount" field.

 - API layer additioanlly exposes two GET API services "GetInvoiceByNumberAsync" and "GetAllInvoicesAsync".
     GetInvoiceByNumberAsync - used to get an invoice by an invoice number.
     GetAllInvoicesAsync - used to get all the existing invoices.

 - Swagger Open API is integrated as the API documentation.

 - xUnit has been used to build the unit testing project.
     InvoiceServiceTest class difines the unit test scenarios.
     ZeroFrictionInvoice.Test.Utilities project handles the InMemoryDatabase factory.

 - Postman workspace invite link
   https://app.getpostman.com/join-team?invite_code=7dd92cc6eb300455a9f7caeded07b4ca&target_code=ca1f661a8737251191172613ee2e9252

   In the Postman collection, it consumes the APIs in localhost (https://localhost:7009). Therefore, first run the project in localhost and check the APIs
   in Postman.

 - CosmosDB instance is setup on Azure. appsettings.json file in the API layer configures the connection string to CosmosDB. Below section configures connection string to CosmosDB.
   
    "AccountEndpoint": "https://b6a62d30-0ee0-4-231-b9ee.documents.azure.com:443/",
    "AccountKey": "utyVHxLK2WK4cNkgSSXNOu008pDzPPnkQxzvzfYMWgNm4RfEANnJSluIsXeP6xE1XGezuyKUGsYgACDbaLga0Q==",
    "DatabaseName": "cosmozerofrictioninvoice" 

    If there is a defferent CosmosDB instance to be connected, change the values of AccountEndpoint, AccountKey and DatabaseName parameters in the appsettings.json file.

    steps to setup cosmosdb
    -----------------------
    - Log in to Azure subscription.
    - Go to "Azure Cosmos DB" from the Azure services.
    - Create an Azure Cosmos DB account.
    - Select "Azure Cosmos DB for NoSQL" as API.
    - Add Account details and Create.
    - Go to Azure Cosmos DB account.
    - Create a new container. (This will be the databasename).
    - Select "Keys" in the leftside navigation menu.
    - Pick the keys to be used in connection string in the project appsettings.json.
      
      URI ----> AccountEndpoint
      PRIMARY KEY ----> AccountKey
      ContainerId ----> DatabaseName


    

