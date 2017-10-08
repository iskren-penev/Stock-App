WAREHOUSE INVENTORY TRACKER
----------------------------------------------------------------------------------
Application description:

Warehouse Inventory Tracker (W.I.T.) is used to track the current state of your company warehouses.
You can see a list of all the warehouses, and if you are registered (as a company), you can add new product entries (incoming and outgoing operations)
for a selected warehouse. You can also see all the recorded entries for a selected warehouse.
All entries in the warehouses are saved in a database. You can track them using the "Inventory Records" list. There you can find information about
every entry in the database: it's type, corresponding warehouse, amount, amount before and after the entry, date and the name of the company that made it. 

Technologies used to create the application: Microsoft ASP.NET MVC 5, Microsoft Entity Framework 6.1, Microsoft SQL Server Express, C#, JavaScript, jQuery
----------------------------------------------------------------------------------

Before you start:

1. Open "App.config" (WIT.Data) and change the "data source" attribute (connectionString tag, row 14) to your locally instaled istance of SQL Server.
2. Open "Web.config" (WIT.App) and change the "data source" attribute (connectionString tag, row 95) to your locally instaled istance of SQL Server.
3. Open the Package Manager Console, choose the WIT.Data project and type the "update-database" command in order to create and populate the database with sample data,
including user roles, users, warehouses and stock entries.

----------------------------------------------------------------------------------
Role description:

1. Role "User":
The common role for every registered user in the application.
Users with that role can view the warehouse and records lists, and add new entries (input or output operations) in the existing warehouses.

2. Role "Moderator":
The moderator can do everything a normal user can.
The moderator can add and/or edit the warehouses in the application.
The moderator can open a list of all registered users in the application and add personal comments about them.

3. Role "Admin":
The administrator can do everything a moderator can.
The administrator can add the "Moderator" role to registered users.

----------------------------------------------------------------------------------
CLASS DESCRIPTION:
------------------MODELS
-------------------------ENTRY MODELS
1. Warehouse: contains information about the warehouse and the stock entry operations
	-Id
	-Name
	-Address
	-Capacity
	-CurrentStock amount
	-StockEntries in the warehouse

2. StockEntry: single entry operation, has corresponding record
	-Id
	-Type
	-Date
	-Amount
	-Record

3. Record: extends the information about the corresponding stock entry
	-Id
	-StockEntry
	-Amount before the entry
	-Amount after the entry
	-User that made the entry

4. User: application user, extends the ASP.NET.Identity IdentityUser class, and contains information about the comments about the user and entries made by the user
	-IdentityUser properties
	-Company Name
	-Comments
	-Records

5. Comment: additional info about the user, provided by an administrator or moderator
	-Id
	-User
	-Content

-------------------------BINDING MODELS
Used to add or edit data in the database

1. WarehouseAddBindingModel: contains inital data to add a new warehouse
	-Name
	-Address
	-Capacity

2. WarehouseEditBindingModel: edits an existing warehouse
	-Id
	-Name
	-Address
	-Capacity
---------------------
3. EntryAddBindingModel: contains initial data to add a new entry and record
	-EntryType
	-WhId - warehouse id as string
	-Amount
---------------------
4. CommentAddBindingModel: contains data to add a new comment about an user
	-UserId
	-Content

-------------------------VIEW MODELS
Used to vizualize the data about the entity models in the application

1. WarehouseListViewModel: used to vizualize the info about a warehouse in a list
	-Id
	-Name
	-Address
	-Capacity
	-CurrentStock amount

2. WarehouseDetailedViewModel: detailed info about the warehouse
	-WarehouseListViewModel
	-Entries: EntryListViewModels collection, containing information about all the warehouse's entries

3. EntryListViewModel: contains information about stock entries
	-Type
	-Date
	-Amount

4. WarehouseAddViewModel: used to vizualize the form for adding a new warehouse
	-Name
	-Address
	-Capacity

5. WarehouseEditViewModel: used to vizualize the form for editing existing warehouse
	-Id
	-Name
	-Address
	-Capacity
---------------------
6. RecordListViewModel: used to vizualize the info about a record in a list
	-Company name
	-Warehouse name
	-Type
	-Date
	-Amount of the entry
	-Amount before the entry
	-Amount after the entry

7. EntryAddViewModel: used to vizualize the form for adding a new entry
	-EntryType
	-WhId - warehouse id as string
	-Amount
---------------------
8. UserListViewModel: used to vizualize the info about a user in a list
	-Id
	-Email
	-Company name
	-User roles
	-Comments about the user

9. CommentAddViewModel: used to vizualize the form for adding a new comment
	-UserId
	-Content
---------------------
10. Microsoft.ASP.NET.IDENTITY: Account and Manage view models, used to vizualize and accomplish user management in the application

------------------DATA
1.IWitContext: interface describing what the application context must contain

2.WitContext: the application context class, extending the IdentityDbContext and IWitContext
	-Warehouses DbSet
	-StockEntries DbSet
	-Records DbSet
	-Comments DbSet
	-DbSets used by ASP.NET Identity (Users, UserRoles, UserClaims, etc.)

3. Configuration: database configuration 
	-Seed method to populate the database with sample data

------------------SERVICES
---------------------------Interfaces
1. IService : common properties and methods used in all other services
	-Context
	-GetUserById
	-GetAllWarehouses
	-GetWarehouseById

2. IWarehouseService: describes the WarehouseService
	-GetListViewModels
	-SearchWarehouses
	-AddWarehouse
	-EditWarehouse
	-GetDetailedViewModel
	-GetAddViewModel
	-GetEditViewModel

3. IRecordService: describes the RecordService
	-GetRecordListViewModels
	-SearchRecords
	-AddEntry
	-GetAddEntryViewModel
	-GetWarehouseSelectListItems

4. IUserService: 
	-GetAllUsers
	-SearchUsers
	-SetRoleNameForModel
	-GetUserSelectListItems
	-GetAddCommentViewModel
	-AddComment

---------------------------Implementations
1. Service: abstract, contains common properties and methods used in all other services
	-Context: istance of the WitContext
	-GetUserById: find a user by a given Id
	-GetAllWarehouses: get all existing warehouses in the database
	-GetWarehouseById: find a warehouse by a given Id

2. WarehouseService: contains methods used in the WarehouseController
	-GetListViewModels: generate list view models for the warehouses in the database
	-SearchWarehouses: searches through the list view models and filters them by given search string
	-AddWarehouse: adds new warehouse to the database by given WarehouseAddBindingModel
	-EditWarehouse: edits an existing warehouse by given WarehouseEditBindingModel
	-GetDetailedViewModel: generates detailed view model by given warehouse Id
	-GetAddViewModel: generates add view model by given WarehouseAddBindingModel
	-GetEditViewModel: generates edit view model by given warehouse Id

3. RecordService: contains methods used in the RecordController
	-GetRecordListViewModels: generate list view models for the records in the database
	-SearchRecords: searches through the list view models and filters them by given search string
	-AddEntry: adds new entry to the database by given EntryAddBindingModel and userId
	-GetAddEntryViewModel: generates add view model by given EntryAddBindingModel
	-GetWarehouseSelectListItems: generates a selectListItems for the warehouses in the database
	-InputEntry (private): adds the given amount to the given warehouse's current stock if possible or throws an exception
	-OutputEntry (private): subtracts the given amount from the given warehouse's current stock if possible or throws an exception
	-GetAllRecords (private): get all existing records in the database 

4. UserService: contains methods used in the UserController
	-GetAllUsers: generates list view models for the users in the database
	-SearchUsers: searches through the list view models and filters them by given search string
	-SetRoleNameForModel: adds the given user roles to a given user list view model
	-GetUserSelectListItems: generates a selectListItems for the users in the database
	-GetAddCommentViewModel: generates add comment view model by given CommentAddBindingModel
	-AddComment: adds comment about a user by given CommentAddBindingModel


------------------APP
1. StartUp: configures the MVC Application

2. Global.asax.cs: MvcApplication
	-ApplicationStart (protected): registers the automapper mappings, routes, areas, bundles and filters

----------------------App_Start
1. AutomapperConfig (static): registers the mappings for entity, view and binding models
	-RegisterMappings(static)

2. BundleConfig: registers the css and javascript bundles used in the application
	-RegisterBundles (static)

3. FilterConfig: registers the action, authorization, exception and result filters used in the application
	-RegisterGlobalFilters (static)

4. IdentityConfig: configures the ASP.NET Identity classes used in the application
	-EmailService
	-SmsService
	-ApplicationUserManager
		-Create: creates an istance of the ApplicationUserManager by given options and context
	-ApplicationSignInManager
		-Create: creates an istance of the ApplicationSignInManager by given options and context
		-CreateUserIdentityAsync: creates new user asyncronously

5. NinjectWebCommon (static): configures Ninject, used as a Dependency Injection Container in the application

6. RouteConfig: registers the routes in the application
	-RegisterRoutes (static)

7. StartUp.Auth
	-StartUp (partial): configurates the user authentication using ASP.NET Identity

----------------------Controllers
1. AccountController: manages user login and registration using ASP.NET Identity

2. ManageController: manages user information (password, telephone number, two-factor-authentication and external logins) using ASP.NET Identity

3. HomeController: vizualizes the home page of the application
	-Index (get): renders the index page view

4. WarehouseController: 
	-IWarehouseService (private)
	-All (get): renders the all view, displaying all warehouses using the WarehouseListViewModel
	-Display (get, partial): displays the warehouses, based on a given search string, using AJAX
	-Add (get): displays the form for adding new warehouse, using the WarehouseAddViewModel
	-Add (post): adds new warehouse in the database or displays the Add form again, depending on whether the given WarehouseAddBindingModel is valid
	-Edit (get): displays the form for editing an existing warehouse, using the WarehouseEditViewModel by given warehouse Id
	-Edit (post): updates the info about a warehose or displays the Edit form again, denpending on whether the given WarehouseEditBindingModel is valid
	-Details (get): displays detailed info about a warehouse by given Id

5.  RecordsController:
	-IRecordService (private)
	-All (get): renders the all view, displaying all records using the RecordListViewModel
	-Display (get, partial): displays the records, based on a given search string, using AJAX
	-AddEntry (get): displays the form for adding new entry, using the EntryAddViewModel
	-AddEntry (post): adds new entry and record in the database or displays the Add form again, depending on whether the given EntryAddBindingModel is valid

6. UserController:
	-IUserService (private)
	-ApplicationUserManager (private)
	-All (get): renders the all view, displaying all users using the UserListViewModel
	-Display (get, partial): displays the users, based on a given search string, using AJAX
	-MakeModerator: adds the moderator role to the user by given userId
	-AddComment (get): displays the form for adding new comment, using the CommentAddViewModel
	-AddComment (post): adds new comment in the database or displays the Add form again, depending on whether the given CommentAddBindingModel is valid
	-AddRolesToViewModels (private): adds the user roles for every user in a given list of UserListViewModels

----------------------------------------------------------------------------------

