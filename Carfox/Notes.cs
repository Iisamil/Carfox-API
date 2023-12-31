﻿/*
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 *                                          Session 01                                                        |
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 *                                # RESTFul APIs #
 *                               ------------------
 * RESTFul APIs -> nafs el URL bs b8er fe el verb [GET | PUT | UPDATE | DELETE]
 * [Controller]     => byt7t ben []
 * ["{Parameter}"]  => el parameter byt7t ben {}
 * ----------------------------------------------
 * [ApiController]
 * [Route ("api/[controller]")]
 * class ProductsController : BaseController
 * {
 * 
 *      [HttpGet]
 *      GetProduct() {}
 *      
 *      [HttpGet("{id}")]
 *      GetProductById(int id)
 *      
 *      [HttpPost]
 *      AddProduct(Product item) {}
 *      
 *      [HttpPut]
 *      UpdateProduct(Product item) {}
 *      
 *      [HttpDelete]
 *      DeleteProduct(Product item) {}
 * }
 * 
 * GraphQL => Collect all Requests in One Request and Response
 * -----------------------------------------------------------------------------------------------------------
 * -----------------------------------------------------------------------------------------------------------
 *                             # Onion Architecture #
 *                            ------------------------
 * 1. Domain | DataAccess Layers : CORE [Class Library]
 *      - Domin Models => Classess lly btt7wl l Tables fe DB [Class/Entity -> Database]
 *      - All Project Here but not Implemented
 * ----------------------------------------------------
 * 2. Repositories : [Class Library]
 *      - DbContext
 *      - FluentAPIs
 *      - Backup for Data
 *      - His Interfaces in Domain [Core]
 * ----------------------------------------------------
 * 3. Service Layer : [Class Library]
 *      - Payment
 *      - Order
 *      - Token
 *      - Caching
 *      - His Interfaces in Domain
 * ----------------------------------------------------
 * 4. Presentation Layer : [Class Library]
 *      - APIs
 *      - MVC
 * -----------------------------------------------------------------------------------------------------------
 * -----------------------------------------------------------------------------------------------------------
 * #1# lma bagy a3ml el Migration bnzl el package bt3tha 3la el Project lly fe l AppSetting[ConnectionString]
 * #2# w at2kd en l project lly nzlt el Package feeh hwa el StartUp Project
 * #3# El Migration b3mlha fe project Repository l2n hwa elly feh AppIdentityDbContext
 * #4# PM> Add-Migration InitialCreate -o Data/Migrations => y3ne hy7ot l migration gwa folder Data
 * -----------------------------------------------------------------------------------------------------------
 * -----------------------------------------------------------------------------------------------------------
 * # Seeding -> Transfer Data to Database 
 *      - First Way :
 *          1. Go to Project Repository
 *          2. Create New Folder in Data Folder
 *          3. Add 3 Json Files [Files Must be Json]
 *          4. Make Class Named AppIdentityDbContextSeed
 * -----------------------------------------------------------------------------------------------------------
 * -----------------------------------------------------------------------------------------------------------
 * # Generic Repository :
 *      - in Core Project Create Folder Named Repositrioes
 *          # Interfaces of Repos
 *          # Create IGenericRepository
 *          # Create Class GenericRepository on Talabat.Repository
 *      - in Core Project Create Folder Named Services
 *          - Interfaces of Services lly h3mlha implement fe Services
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 *                                          Session 02                                                        |
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * # Specification Design Patterns :
 *      - Help Us to Build Dynamic Query
 *      - .Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType);
 *          1. Create Interface Including Property Signuture For Each Component in Query [Core Project]
 *          2. Make Prop for Condition of Where => Named Criteria
 *              A -  h7dd hya eh => Lambda Expression [Expression]
 *              B -  h7dd no3ha eh [Predicit | Func | Action] => Func l2nha bta5od haga w b t return bool
 *              C -  el Func<T , bool> == y3ne bta5od GenericType 'T' and Return Boolean
 *          3. Make Prop for Includes => Named Includes
 *              A - h7dd hya eh => Lambda Expression [Expression]
 *              B - hya bta5od mni eh ? => P.ProductBrand | P.ProductType
 *              C - kda htrg3 List<> w no3 el Expression Func
 *              D - Func(T , object) => y3ne kda hta5od GenericType "T" w trg3 ay Object
 *          4. Make Class on SpecificationsFolder Named "BaseSpecifications"
 *          5. Make Two CTOR One for Where AND One for Includes
 *          6. Make Class in RepositoryProject Named "SpecificationEvalutor" 
 *          7. Implement Function That Build Query Takes 2 Parameter(DbContext , Specifiation) in Class "SpecificationEvalutor"
 *          8. Go to IGenericRepository Makes New Functions Dynamic
 *          9. Go to GenericRepositoryClass Make new Private Function named ApplySpecification
 *          10. Call Function ApplySpecification to new Functions Implemented
 *          11. Go to Controller and Make Update to use Spec
 *          12. Make New Class in Folder Specifications Named "ProductWithBrandAndTypeSpecifications" Inherit from Class BaseSpecifications
 *          13. Call CTOR of "ProductWithBrandAndTypeSpecifications" in Product Controller
 *  ------------------------------------------------------------------------------------------------------------------------------------------
 *  # Applying Specification Design Pattern For New Module :
 *  ---------------------------------------------------------
 *          1. Add Class Named Department to Folder 'Entites' Inherit Id from BaseEntity
 *                  - Has Name     - Date of Creation
 *          2. Add Class Named Employee to Folder 'Entites' Inherit Id from BaseEntity
 *                  - Has Name     - Salary     - Age       - DepartmentId [ForiegnKey]
 *          3. One Department Has Many Employees
 *                  - Create Navgtional Property One in EmployeeEntity
 *                  - Use Fluent APIs 3l4an a3rf el 3laka enha One to Many bas
 *          4. Go To Project API to Add New Controller Named EmployeeController
 *          5. Inherit Routing Attribute from ApiBaseController
 *          6. Create CTOR to Get Object from GenericType<Employee> hyklm class IGenericRepository<Employee> empRepo
 *          7. Create Funtion to GetAllEmployees
 *          8. Go to CORE PROJECT Create New Folder in Specification Named Employee
 *          9. Create New Class in Folder Named Employee Specs
 *          10. Inherit from Class BaseSpecification<>
 *          11. Create Function Named EmployeeWithDepartmentSpecification
 *          12. Include.Add( Expression );
 *          13. Back to Controller and Create Object From This Class "EmployeeWithDepartmentSpecification()"
 * -----------------------------------------------------------------------------------------------------------
 * -----------------------------------------------------------------------------------------------------------
 * # DTO :
 * --------
 * - DTO in APIs == ViewModel in MVC
 *   
 *          1. Create Folder in Projec 'APIs' Named Dtos
 *          2. Add Class ProductToReturnDto [class hymsl 4kl el product lly hyrgt3 ll enduser]
 *                  - m4 hywrs BaseEntity 3l4an hwa m4 Table
 *                  - Same Data in ProductEntity + Property for His Own Id
 *                  - Change ReturnType of ProductBrand|ProductType to String
 *          3. Install AutoMapper on APIs Project
 *          4. Refactor Code for End Points to Return DtoProduct
 *          5. Go To ProductController 
 *          6. Ask CLR to Create Object From Interface IMapper
 *                  - Allow Dependancy Injection of IMapper
 *                      - Go To Program
 *                          - Configure Services
 *                              - builder.Services.AddAutoMapper(M => M.AddProfile());
 *                  - in APIs Project Create New Folder Named Helpers
 *                      - Add Class Named MappingProfiles
 *                          - Inherit from Profile
 *                              - Create Map for Product
 *                                  - Make Config to Change ReturnTypes of Brand and Types to String
 *                                      - CreateMap<lly h7wlo , h7wlo l eh>()
 *                                          .ForMember(d => d.ProductBrand , O => O.MapFrom(s => s.ProductBrand.Name));
 *                                              - No Enable InverseMap() => lw h3ml enable ll inversemap hyb2a 3ndy Create()
 *                  - Go to Class 'Program' Give Profile Object from MappingProfiles 
 *                      - builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); NOT RECOMMENDED
 *                      - builder.Services.AddAutoMapper(typeof(MappingProfiles)); RECOMMENDED
 *         7. Go To Class ProductsController
 *         8. Change Return of Function to Return ProductToReturnDto
 *              - return Ok(_mapper.Map<Product , ProductToReturnDto>(product));
 *         9. Thank You :)
 * -----------------------------------------------------------------------------------------------------------
 * -----------------------------------------------------------------------------------------------------------
 * # Solve Picture URL :
 * ----------------------
 *         1. Go to Folder 'Helpers'
 *         2. Add Class Named 'ProductPictureUrlResolver' Inherit from 'IValueResolver<>' From AutoMapper
 *                  - IValueResolver<Source , Destination , Member[PictureReturnDataType]>
 *                  - IValueResolver<Product , ProductToReturnDto , string>
 *                      - Check Null or Empty
 *                          if (!string.NullOrEmpty(source.PictureUrl))         |
 *                              return $"{"BaseURL"}{source.PictureUrl}"; wrong | => WRONG
 *                          return string.Empty;                                |
 *         3. Go to Class AppSetting
 *                  - "ApiBaseUrl" : "BaseURL/"
 *         4. Create CTOR in 'ProductPictureUrlResolver'
 *                  - Ask to Create Object from Interface IConfiguration
 *                          if (!string.NullOrEmpty(source.PictureUrl))                       |
 *                              return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";  | => Right
 *                          return string.Empty;                                              |
 *         5. Go to MappingProfiles to Add new .ForMember for PictureURL to Use This Function
 *         6. Make New Folder in APIs Project Named 'wwwroot'
 *         7. Take Images Folder on PC and Insert it in wwwroot
 *         8. Go to Program and On ConfigureMiddlewares 'app.UseStaticFiles();'
 * -----------------------------------------------------------------------------------------------------------
 * ----------------------------------------------------------------------------------------------------------- 
 * # Errors Types in APIs :
 * -------------------------
 * 1. Not Found
 * 2. Server Error [NULL Exception]
 * 3. Bad Request       | == User Sent Wrong Request [zai lma btlop mno int f hwa yd5le string]
 * 4. Validation Error  | == User Sent Wrong Request [zai lma btlop mno int f hwa yd5le string]
 * -----------------------------------------------------------------------------------------------------------
 * # We Need to Make Standard Error Page for All Errors Types #
 * -----------------------------------------------------------------------------------------------------------
 * 1) NotFound | BadRequest Erros :
 * ---------------------------------
 *      - Add New Folder in APIs Project Named Error
 *      - Add Class Named ApiResponse
 *          - StatusCode        - Message
 *          - Create CTOR That Takes (statusCode , Message? = null)
 *                  - StatusCode = statusCode;
 *                  - Message = message ?? GetDefaultMessageForStatusCode(statusCode);
 *          - Generate Method Named 'GetDefaultMessageForStatusCode()' Returned Switch {}
 *      - Go to ProductController and Make Scan to EndPoints Thats May Throw NotFound Error(404)
 *          - Function GetProductWithId
 *              - if (product is null) return NotFound(new ApiResponse(404));
 * -----------------------------------------------------------------------------------------------------------
 * 2) Validation Error :
 * ----------------------
 *      - Type of BadRequest Error
 *      - Add Class to Folder Erros Named 'ApiValidationErrorResponse'
 *              - Inherit 2 Properties from Class ApiResponse
 *              - IEnumerable<string> His Own Property
 *              - Make CTOR Inherit from ApiResponse base(400)
 *                  - Errors = new List<string>();
 *              - Go To Default Validation of This Error 
 *              - Go To Program Class in ConfigurationServices
 *                  - builder.Services.Configure<ApiBehaviourOptions> (options => ... implement)
 * -----------------------------------------------------------------------------------------------------------
 * 3) Server Error [Exception] :
 * ------------------------------
 *     - Create New Folder in APIs Project Named "Middlewares"
 *     - Add Class to Folder "Middlewares" Named 'ExceptionMiddleware'
 *     - Has CTOR Must Take 3 Parameter
 *          - (RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnviroment env)
 *     - Must Have Function Named "InvokeAsync(HttpContext context)"
 *     - Go to Program in Middlewares
 *          - app.UseMiddleware<ExceptionMiddleware>(); [Must be First Middleware]
 *     - Return to Class 'ExceptionMiddleware' and Implement Function InvokeAsync
 *     
 *     
 *     
 *     
 *     - Add Class to Folder Errors Named 'ApiExceptionResponse' Inherit From ApiResponse
 *          - Nullable Property Details
 *          - CTOR (int statusCode , string? message=null , string? details=null):base(statusCode,message)
 *              Details = details;
 *              
 *     - Go to Class 'ExceptionMiddleware' to Create Object(Code , Message , Details) from CTOR Where in 'ApiExceptionResponse'
 *     - Check if is in Production OR NOT
 *     - Change Request to JSON with JsonSeriallizer
 * -----------------------------------------------------------------------------------------------------------
 * 4) Handling NotFound End Point Error :
 * ---------------------------------------
 *     - I want to Make if User of Front-end Input Wrong EndPoint ByDefault Redirect Him to Specific EndPoint
 *          1. Add New Controller Named 'ErrorsController' to Controllers Folders
 *          2. Change [Route("erros/{code}") => StatusCode]
 *          3. Create EndPoint lly h3ml Redirect Leha
 *              - Return ActionResult
 *              - Named Error
 *              - Take Code
 *              - Return NotFound(new ApiResponse(code));
 *              = Add Attribute [ApiExploerSetting(IgnoreApi = true)] ==> mtrkz4 m3ah ya swagger (lw 3ndy controller m4 3awz a3mlo Documention)
 *          4. Go to Program Class
 *              - Configure Middlewares
 *              - Add 'app.UseStatusCodePagesWithReExecute("/errors/{0}");' Before Middleware 'app.UseHttpsRedirection();'
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 *                                          Session 03                                                        |
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * (1) Cleaning Up Startup Configurations :
 * -----------------------------------------
 *     1. Create Folder in APIs Project Named Extensions
 *     2. Add Class to This Folder Named 'ApplicationServicesExtension' 
 *          - Create Function Named ApplicationServices()
 *          - Return IServicesCollection
 *          - Take 'this IServicesCollection' this => 3l4an a3mlha calling 3ltol [builder.Services.FunName();]
 *          - Return services;
 *          - Take Code of OUR BUSINESS in Program => Configure Services and Put it Into this Function
 *          - Call this Function in Program Class
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * (2) Create End Points to GetBrands and GetTypes :
 * --------------------------------------------------
 *      A) Get Brands:
 *      ---------------
 *          # In ProductController
 *              1. Make Function Named GetBrands() to Return IEnumerable<> of Brands
 *              2. Initilize IGenericRepository<ProductBrand> in CTOR
 *              3. Call Function GettAllAsync() to Get All Brands [m4 hst5dm el spec 3l4an m3ndi4 Navigtional Property]
 *              4. Return Ok();
 *          
 *      B) Get Types:
 *      ---------------
 *          # In ProductController
 *              1. Make Function Named GetTypes() to Return IEnumerable<> of Types
 *              2. Initilize IGenericRepository<ProductType> in CTOR
 *              3. Call Function GettAllAsync() to Get All Types [m4 hst5dm el spec 3l4an m3ndi4 Navigtional Property]
 *              4. Return Ok();
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * # Important Notes :
 * --------------------
 *      - IQueryable<>   --> Lw ana 3awz ageb list mo3ina mn Db wa3mlha Display w a5leh y3ml el Filter fe Db yb2a hst5dm 
 *      - ICollection<>  --> Lw ana 3awz ageb list mo3ina mn Db bs m4 display bs w h3mlhom [Add|Update|Delete] hst5dm 
 *      - IEnumerable<>  --> Lw ana 3awz ageb list mo3ina mn Db bs m4 m4 h3ml Filter wla hst5dm CRUD Operation lw ana ha loop 3la elly rag3
 *      - IReadOnlyList<>--> Lw ana 3awz ageb list mo3ina mn Db bs m4 m4 h3ml Filter wla hst5dm CRUD Operation lw hrg3hom bs w5las
 *                           Only in Memory Collection [Support Caching]
 *                           
 *      1. Go to IGenericRepositroy and Convert All Return Type from IEnumerable<> to IReadOnlyList<> [Best Performance]
 *      2. Go to Class GenericRepository and Convert All Return Type from IEnumerable<> to IReadOnlyList<>
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * (3) Sorting :
 * --------------
 *      1. Go to Function GetAllProducts in ProductController and Give it (string sort)
 *      2. Call 'sort' in var spec = ....;
 *      3. Go to CTOR of ProductWithBrandSpecification(string sort) == h3rf el sort fe ctor bta3 el class 
 *      4. Go to ISpecification<> Interface
 *          - Create Property to Carry OrderBy | OrderByDescending
 *              - Expression => Return Func(T , object)
 *      5. Go to BaseSpecification and CTRL + . => Implement Interface
 *          - Create Two Void Functions Takes Expression<Func< , >> [SET Functions ONLY !]
 *      6. Return to Class 'ProductWithBrandSpecification'
 *          - Check if sort IsNullOrEmpty ?
 *          - Make Swicth Case to Sort Data
 *      7. Go to Class SpecificationEvaulator
 *          - Check spec.OrderBy != null
 *              - query = query.OrderBy(spec.OrderBy);
 *          - Check spec.OrderByDecsending != null
 *              - query = query.OrderByDecsending(spec.OrderByDecsending);
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * (4) Filteration :
 * ------------------
 *      1. Go to Function GetAllProducts in ProductController and Give it Two Nullable Options to Filter (int? brandId , int? typeId)
 *      2. Call 'brandId , typeId' in var spec = ....;
 *      3. Go to CTOR of ProductWithBrandSpecification(int? brandId , int?typeId) == h3rf el sort fe ctor bta3 el class 
 *      4. Make CTOR of Class 'ProductWithBrandSpecification' Chain to CTOR of Criteria(take expression)
 *          - base(P =>     
 *                          (!brandId.HasValue || P.ProductBrandId == brandId.Value) &&
 *                          (!typeId.HasValue  || P.ProductTypeId  == typeId.Value)
 *                )
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * (5) Pagination :
 * -----------------
 * # Connected Model    => Lot of Data [3ndy Data kter awi y3ne] Ecommerce,....
 * # DisConnected Model => Less Data   [3ndy Data 2olela]
 *     - Need Two Parameter [PageIndex | PageSize]
 *     - Rules of Clean Code : If Function Has More Than 3 Parameter => Create Object and Make Function Take Object of UnLimited Parameters
 *     
 *     1. Refactor Code GetProducts [3l4an hwa bya5od mni already 3 parameter w ana 3awz azwd 2 kaman]
 *     2. Add Class in Folder 'Specification in CoreProject' Named 'ProductSpecParams'
 *          - Prop? sort
 *          - Prop? BrandId
 *          - Prop? TypeId
 *          - Change Input of Function GetProducts
 *          - Go to CTOR and Change Input
 *     3. Return to Class 'ProductSpecParams'
 *          - Add PropFull [Full Property] to PageSize Has Default Value = 5
 *          - Create private const .... = 10;
 *              - Configure it to Accept [value > .... ? .... : value]
 *          - Add prop for PageIndex Has Default Value = 1
 *     4. Go to ISpecification Interface
 *          - Add Two int Prop for Skip | Take
 *          - Add Bool Prop Named 'IsPaginationEnabled'
 *     5. Go to Class That Implement Interface 'BaseSpecification'
 *          - Implement 3 Properties
 *          - Create Funtion Named 'ApplyPagination'
 *              - Void
 *              - Take 2 Parameter
 *     6. Go to Class 'ProductWithBrandSpecification' and Call the Funtion 'ApplyPagination' in CTOR
 *     7. Go to Class 'SpecificationEvalutor' 
 *          - After Query OrderBy
 *          - Write Query for Pagination
 * ---------------------------------------------------------------------------------------------------------------------------------
 * # Standard Response Pagination For Any End Point Has GetAll() :
 * ----------------------------------------------------------------
 *     1. Add Class Named 'Pagination' in Folder Helpers
 *          # Ay EndPoint GetAll htrg3 el Response de
 *          - Prop for PageIndex
 *          - Prop for PageSize
 *          - Prop for Count
 *          - Prop IReadOnlyList<> for T named Data
 *     2. Go to Class ProductController
 *          - On Function GetProducts()
 *              - Change ReturnType from [IReadOnlyList<>] =to=> [Pagination<ProductToReturnDto>]
 *              - H3ml var data = .. h7ot el code lly kan gwa el return feha
 *              - H3ml return Ok(new Pagination(specParams.PageIndex , specParams.PigeSize , data));
 *              - CTRL + . => Generate CTOR and Edit it
 *     3. To Configure Count...
 *     4. Go to Interface IGenericRepository
 *          - Create Function of Count 
 *              - Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
 *     5. Go to Class That Implement Interface 'GenericRepository'
 *          - CTRL + . => Implement Interface
 *              - return await ApplySpecification(spec).CountAsync();
 *     6. Add Class 'ProductWithFilterationForCountSpecification' in Folder "Specifications"
 *          - Class Inherit From BaseSpecification<Product>
 *          - Generate CTOR Take ProductSpecParams
 *          - Take FilterCriteria from Class 'ProductWithBrands..' and Put it Here
 *     7. Return to Class 'ProductController' 
 *          - Make var countSpec = new 'ProductWithFilterationForCountSpecification'(specParams);
 *              - Make var count = await _productRepository.GetCountWithSpecAsync(countSpec);
 *                  - return Ok(new Pagination(specParams.PageIndex , specParams.PigeSize , count , data));
 *                      - Add count in CTOR in Class 'Pagination'
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * (6) Search By Product Name :
 * -----------------------------
 *     1. Go to Class 'ProductSpecParams'
 *          - Create PropertyFull String "Search"
 *          - Configure Property to Take ToLower()
 *          - Customize Criteria to Allow Search
 *     2. Go to Class 'ProductWithBrandAndTypeSpecification'
 *          - Add new Condition
 *              - (!string.IsNullOrEmpty(productsParams.Search) || P.Name.ToLower().Contains(products.Search)
 *     3. Copy Condition From 'ProductWithBrandAndTypeSpecification' and Put it Into 'ProductWithFilterationForCountSpecification'
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 *                                          Session 04                                                        |
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
 * (1) Identity - Create DataBase :
 * ---------------------------------
 *     1. Download IdentityPackage "Microsoft.AspNetCore.Identity.EntityFrameworkCore" in Core Project
 *          - RepositoryPrj has Reference from Core Project
 *          - ServicesPrj   has Reference from Core Project
 *          - APIs Project  has Reference from Services Prj
 *     2. Create New Folder Named "Identity" in Folder Entities
 *     3. Add Class Named 'AppUser' to Folder "Identity" [To Customzie Data]
 *          - Inherit from Class IdentityUser
 *          - Prop DisplayName
 *          - Prop Address
 *     4. Generete New Class Named 'Address'
 *          - Prop FirstName
 *          - Prop LastName
 *          - Prop Country
 *          - Prop City
 *          - Prop Street
 *     5. Relation Between Address | AppUser [ONE to ONE]
 *     6. Create Navigational Property One in Address and AppUser
 *     7. Take PK of AppUser as FK in Class 'Address' [string]
 *     8. Now We Need to Create Class That Will Talk to Database
 *     9. Create New Folder in Project Repository Named "Identity"
 *     10. Add Class Named 'AppIdentityDbContext' to This Folder 
 *          - Inherit from =IdentityDbContext<Of Class AppUser>=
 *              - Create CTOR Hygelo Object mn Class DbContextOption<'IdentityDbContext'> : base()
 *     11. Allow Dependancy Injection to 'IdentityDbContext'
 *          -> Go to Program => Configure Services
 *              -> Go to AppSetting Make "ConnectionString" Name it "IdentityConnection"
 *                  -> Return to Program => Config Services and Call "IdentityConnection" from AppSetting
 *     12. Add-Migration
 *          + Make Sure That Startup Project is APIs Because it has 'AppSetting' File
 *              + Add-Migration .... -context "DbName elly 3awzha t run 3leh" 
 *                  + Make Default Project "Repository" 
 *                      + -o Identity/Migrations
 *     13. We Need to Make UpdateDatabase...
 *          + Go to Program 
 *              + UpdateDatabase Region
 *                  + In Try{}
 *                      + var identityContext = services.GetRequiredService<IdentityDbContext>();
 *                          + await identityContext.Database.MigrateAsync(); -> UpdateDatabase
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------
 * (2) Identity - User DataSeeding :
 * ----------------------------------
 *     1. Add Class Named 'AppIdentityDbContextSeed' to Folder "Identity"
 *          - Make One Function Static Async
 *              - Named SeedUsersAsync()
 *                  - Take (Object of UserManager<AppUser>)
 *                      - Check if there is user or NOT
 *                          - if (!userManager.Users.Any()) => y3ne lw mfi4 wla user 3ndy h3ml Seeding
 *                              - Create Manual Seed User
 *                                  - var user - new AppUser();
 *                                      {
 *                                          DisplayName , Email , UserName , PhoneNumber
 *                                      }
 *                                      - await userManager.CreateAsync(user , "Pass to user"); => kda hyro7 yseed m3 password lly hb3to
 *     2. Go to Class Program and Call Seeding in Region UpdateDatabase in Try{}
 *          - var userManager = services.GetRequiredService<UserManager<AppUser>>(); = ask clr to create obj from UserManager
 *              - await IdentityDbContextSeed.SeedUsersAsync(userManager);
 *     3. Go to Allow Dependancy Injection
 *          - Go to Folder "Extensions"
 *              - Add Class Named 'IdentityServicesExtension' [Static]
 *                  - Return IServiceCollection
 *                      - Named AddIdentityServices()
 *                          - Take this IServiceCollection
 *                              - services.AddIdentity<AppUser /bymsl el user/ , IdentityRole /bymsl elrole/> 3l4an a7dd men user w men role>(
 *                                  options =>
 *                                  {
 *                                      
 *                                  }).AddEntityFrameworkStores<AppIdentityDbContext>(); => to add implention of Function Create
 *                                  
 *                                  services.AddAuthentication(); = Allow DependancyInjection for UserManager SignInManager RoleManager
 *                                      - Return services;
 *     4. Go to Program Class in ConfigServices Region
 *     5. Call This Function :) 
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (3) Create Login EndPoint :
 * ----------------------------
 *     1. Create AccountController in Folder "Controllers"
 *          - Inherit From BaseApiController
 *          - Create CTOR 
 *              + Inject User From<AppUser> userManager => To Register
 *                  + Inject User From <AppUser> signInManager => To Sign In
 *     2. Create EndPoint
 *          - [HTTP POST("login")]
 *              - Add Class 'UserDto' to Folder "Dtos"
 *                  + Prop DisplayName
 *                  + Prop Emial
 *                  + Prop Token
 *                  - Public Async Task<ActionResult<UserDto>>
 *                      - Named Login (take LoginDto)
 *                          - Add Class 'LoginDto' in Folder "Dtos"
 *                              + Prop Email    [Required , EmailAddress]
 *                              + Prop Password [Required]
 *                                  - var user = hyb3tli _userManager w h3mlo findbyemailaddress(loginDto.Email);
 *                                      - b3d kda h3ml check 3la el email lw gai b null hrg3mlo object mn apiresponse 401
 *                                          - var result = hyb3t _signIn...CheckPassw..(hya5od el user w pass , false);
 *                                              - h3ml check 3la pass w lw 8lt hrg3lo obj ApiResponse error
 *                                                  - Return Ok(new UserDto() 
 *                                                  {
 *                                                      DispName =..
 *                                                      Email = ..
 *                                                      Token =""
 *                                                  });
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (4) Create Register EndPoint :
 * -------------------------------
 *     1. On AccountController
 *     2. Create EndPoint [HttpPost("register")]
 *          - Task of ActionResult
 *          - Named Register()
 *     3. Add Class Named 'RegisterDto'
 *          - Prop DispName    [Required]
 *          - Prop Email       [Required , EmailAddress]
 *          - Prop Pass        [Required , Regular Expression]
 *          - Prop Phone       [Required , Phone]
 *     4. EndPoint Register()
 *          - Take RegisterDto
 *              - var user = .. h3ml new user mn AppUser
 *                  - var result = hklm el userManager to create user he will take user,password
 *                      - check if succeeded lw false hrg3lo badRequest 400
 *                          - return ok(new userDto ......);
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (5) Generate JWT Token :
 * -------------------------
 *     - SSN of User in Website
 *     - Token Consists of 3 Component 
 *          1. Header
 *              - Json Object
 *                  - Algo
 *                  - No3 el Token [JWT]
 *          2. Payload : Data
 *              - Inputs to Function [Sec Algo]
 *              - Hard to Decrypt Token
 *          3. Signature
 * --------------------------------------------------------------------------------------------------------------------------------- 
 * # To Generate Token into Project :
 * -----------------------------------
 *     1. Go to Core Project
 *     2. Add New Folder "Services"
 *     3. Add Interface 'ITokenService'
 *          - Public
 *          - Create Function Named Token()
 *          - Return Task<string>
 *          - Take AppUser
 *     4. Go to Project Services
 *     5. Download Package of JWT On Project Services
 *     6. Add Class 'TokenService'
 *          - Public
 *          - Inherit From ITokenServices
 *          - Add Reference From Project Core to Project Services
 *     7. Implement Interface
 *          + Create Private Claims [User-Defined]
 *              + var authClaims = new List<Claim>()
 *              {
 *                 // m7tag a7dd 7agten el awl el Type w el Value 
 *                 new Claim(ClaimTypes.Email , user.Email) == b7dd no3 el claim email f hyb3to ll email user lly gai
 *                 new Claim(ClaimTypes.GivenName , user.DisolayName) == b7dd no3 el claim givenName f hyb3to ll DispName lly gai
 *              };
 *                  + Generate UserManager<AppUser> in ITokenService w Function [Role y3ne] 3l4an azwd el Claims
 *                      - var userRoles = await userManager.GetRolesAsync();
 *                          + hb2a m7tag a3de 3la kol role w a3mlha add fe function CreateToken
 *                              + foreach (var role in userRoles)
 *                                  + authClaims.Add(new Claim(ClaimTypes.Role , role));
 *          + Create Key
 *              + var authKey = new SymmtricSecurityKey(3awz mny el key b7oto fe appSetting);
 *                  + Go to APIs Prj => AppSetting
 *                      + "JWT" : 
 *                      {
 *                          "Key" : "StrONGAutHENTICATIONKEy"
 *                      },
 *                          + Return to Class 'TokenService'
 *                              + Add CTOR Take IConfiguration to Talk to AppSetting File
 *                                  + Call Function in SymmtricSecurityKey
 *                                      + var authKey = new SymmtricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])); || Encoding.UTF8.GetBytes => to Convert Return into Arry of Bytes
 *         + Registerd Claims => Thabta ll User Kolhom
 *              + Go to AppSetting in "JWT"
 *                  + "ValiderIssuer" : BaseURL
 *                  + "ValidAudience" : "MySecuredApiUsers"
 *                  + "DurationInDays": "30"
 *         + Create Token in Class 'TokenService'
 *              + var token = new JwtSecurityToken(
 *                      issuer   : talk to AppSetting to JWT to Issuer
 *                      Audience : ......
 *                      Expires  : DateTime.Now.AddDays(double.Parse(talk to AppSetting));
 *                          + Send Private Claims
 *                              + claims = authClaims,
 *                                  + signinCredentials : new SignInCredentials(authKey , SecurityAlog.    )
 *                      ); 
 *         + return new JwtSecurityTokenHandler().WriteToken(token); == 3l4an ytl3li el token "DASODJOQWJOFJALKDLKOQWJDOQ"
 *         
 *     8. Add Reference from ServicesPrj to APIs Prj
 *     9. Go to Class 'IdentityServiceExtensions' in APIs Prj
 *     10. In Middleware 'AddAuthentication();
 *          - AddAuthentication(JwtBearerDefaults.AuthenticationSchema)
 *              .AddJwtBearer();
 *     11. Go to 'AccountController'
 *     12. Add ITokenService in CTOR
 *     13. Allow DependancyInjection
 *          - Go to 'ApplicationServicesExtensions'
 *              - services.AddScoped<ITokenService , TokenService>();
 *     14. Return to 'AccountController'
 *          - In Token = "Will Be Token Inshallah"
 *              - Token = await _tokenService.CreateToken(user , _userManager)
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (6) Basket Module :
 * --------------------
 * A) Entity For Basket :
 * -------------------------------
 *     1. Go to Core Project
 *     2. Add New Class Named 'BasketItem' in Folder "Entities"
 *          - Public
 *          - Prop Id => PK for Item
 *          - Prop String => ProductName
 *          - Prop PictureURL => For Picture
 *          - Prop Price
 *          - Prop Quantity => Number of Units
 *          - Prop Brand
 *          - Prop Type
 *     3. Add New Class Named 'CustomerBasket' in Folder "Entities"
 *          - Prop String Id => PK of Class BasketItem (string 3l4an Guid)
 *          - Prop List<BasketItem>
 * ---------------------------------------------------------------------------------------------------------------------------------
 * B) Basket Repository :
 * -----------------------
 *     1. Go to Folder "Repositories"
 *     2. Add Interface 'IBasketRepository'
 *          - Public
 *          - 3 Signature For 3 Method
 *              + Task<CustomerBasket> GetBasket...
 *              + Task <> UpdateBask... [Upadte , Create]
 *              + Task <> DeleteBask...
 *     = To Implement Interface Go to Project Repository
 *     4. Go to Prj Repository
 *     5. Add Class 'BasketRepository'
 *          - Public
 *          - Inherit From IBasketRepo..
 *          - Implement Interface
 *     6. Install Package #Redis#
 *     7. Download it Into Core Prj Because Services & Repository Has Reference From It
 *     8. We Need to ASK CLR to Injec Object From Redis
 *          - Go to Class 'BasketRepository'
 *          - Create CTOR 
 *              - Take Object From "IConnectionMultiplexer"
 *     9. We Need to Allow DependancyInjection For Redis
 *          - Go to Program
 *              - Configure Services
 *                  - AddSingelton() => el connection htfdl mfto7a talma el Session mfto7a 3l4an y2dr y add kaza item
 *                      - Make 'ConnectionString' 
 *                          - Go to AppSetting File
 *                          - Make New ConnectionString Named "Redis" => "localhost"
 *                              - Return ConnectionMultiplexer.Connection()...;
 *     10. Back to Class 'BasketRepository'
 *     11. Add Private ReadOnly IDatabase...
 *     12. Call it into CTOR and Make It Equal redis.GetDatabase();
 *     13. Implement Function of Delete
 *          - Return await _database.KeyDeleteAsync(basketId);
 *     14. Implement Function of Get
 *          - h3rf basket bya5od mni StringGetAsync(...);
 *          - hyreturn basket b null wla la : JsonSerailizer.Deserliaze<CustomerBasket>(basket);
 *     15. Implement Function of Update
 *          - h3rf createdOrUpdated = StringSetAsync(Key , Value , ExpirationDate);
 *                                                  (basket.Id , JsonSerilazer.Serlaize(basket) , TimeSpan.FromDays(1)
 *          - Check if createdOrUpdated is created or not
 *          - Return Get(...);
 * ---------------------------------------------------------------------------------------------------------------------------------     
 * C) Basket Controller :
 * -----------------------
 *     1. Add Controller Named 'BasketController'
 *          - Inherit from BaseApiController
 *     2. Create CTOR to Ask CLR to Inject Object From Class 'IBasketRepository'
 *     3. We Need to Allow DepenedancyInjection for IBasketRepositroy
 *          - Go to Program
 *              - Configure Services
 *                  - builder.Services.AddScoped(typeof(IBasketRepositroy), typeof(BasketRepository));
 *                      - Return to CTOR and Make Create And Assign Field
 *     4. Create EndPoint to Get Customer Basket
 *          - [HttpGet("id")]
 *          - Return ActionResult Of CustomerBasket 
 *          - Named 'GetCustomerBasket'
 *          - Take String id
 *              - h3rf basket = ...GetBasketAsync(id);
 *              - return basket is null ? new CustomerBasket(id) : basket;
 *                  - Go to Class 'CustomerBasket'
 *                      - Add CTOR take String id
 *                          - Id = id;
 *     5. Create EndPoint to UpdateBasket
 *          - [HttpPost]
 *          - Return ActionResult of CustomerBasket 
 *          - Named UpdateBasket
 *          - Take CustomerBasket basket
 *              - h3rf CreatedOrUpdatedBasket = ....UpdateBasketAsync(basket);
 *              - Check if null => return badRequest(400);
 *              - return Ok(CreatedOrUpdatedBasket);
 *     6. Create EndPoint to Delete
 *          - [HttpDelete]
 *          - Return ActionResult of bool
 *          - Named DeleteBasket
 *          - Take string id
 *              - return ....DeleteBasketAsync(id);
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (7) Test Authorization :
 * -------------------------
 *     1. Go to 'Program'
 *     2. In Configure Middlewares
 *          - app.UseAuthentication();
 *          - app.UseAuthorization();
 *     3. Go to Any EndPoint I Want it To Be Authourized and Put Attribute on it [Authourize]
 *     4. Add to Attribue Schema => [Authourize(AuthenticationSchema = JwtBearerDefaults.AuthenticationSchema)]
 *     5. Go to 'IdentityServicesExtension' And Configure .AddJwtBearer
 *          .AddJwtBearer ( take Lambda Expression of one Parameter)
 *          .AddJwtBearer (options => 
 *          {
 *              options.TokenValidationParameters = new ...............()
 *              {
 *                  - TokenValidationParameters ==> h3ml validate beha ll token lly gai ask if its mine or not
 *                  - Inject IConfiguration to this class
 *                  - Go to Class Program on 'AddIdentityServices(builder.Configration);
 *                  ValidateIssuer = true,
 *                  ValidIssuer = configuration["JWT:ValidIssuer"]
 *                  ValidAudience = configuration["JWT:Audience"]
 *                  ValidateIssuerSigningKey = true,
 *                  IssuerSigningKey = "el authKey lly fe TokenServices
 *              };
 *          });
 *     6. To Set Default Schema for Authrization 
 *     7. Go to 'IdentityServicesExtension'
 *          - services.AddAuthintication (options => 
 *          {
 *              options.DefaultAuthenticationSchema = JwtBearerDefaults.AuthenticationSchema;
 *              options.DefaultChallengeSchema = JwtBearerDefaults.AuthenticationSchema;
 *          });
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (8) Get Current User Who SignedIn :
 * ------------------------------------
 *     1. Create EndPoint in 'AccountController'
 *          - [HttpGet]
 *          - [Authorize]
 *          - Public
 *          - Return ActionResult of UserDto
 *          - Named GetCurrentUser
 *              - var email = User.FindFirstValue(ClaimsTypes.Email); => htgblna el email bta3 el user lly 3ml sign in
 *              - var user = _userManager.FindByEmailAsync(email);
 *              - return Ok(new UserDto()
 *                  {
 *                      DisplayName = user......,
 *                      Email = .....,
 *                      Token = _tokenService.CreateTokenAsync(user , _userManager)
 *                  });
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (9) Get User Address :
 * -----------------------
 *     1. Create EndPoint to Get UserAddress Who LoggedIn
 *     2. Add EndPoint in 'AccountController'
 *          - [HttpGet("address")]
 *          - [Autorize]
 *          - Return ActionResult Of Address
 *          - Named GetUserAddress()
 *              - h3rf email = User.FindFirstValue(ClaimTypes.Email); => hygeb el email
 *              - user = _userManager.FindByEmailAsync(email);        => hygeb el user
 *              - return ok(user.address);
 *     3. Go to Folder "Extensions"
 *     4. Add New Class 'UserManagerExtension'
 *          - Public Static
 *          - Return AppUser
 *          - Named FindUserAddressByEmailAsync()
 *          - Take this UserManager<AppUser> userManager => Extension Method for User Manager
 *          - Take ClaimPrincples -> User
 *              - Cut Line 784 and Paste it Here
 *              - hgeb el user nfso => var user = userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);
 *              - return user;
 *     5. Return To Class 'AccountController' At EndPoint GetUserAddress
 *          - h3ml calling ll function lly ana 3mltha w a7otha fe user
 *          - Line 786
 *     6. Add new Dto Named 'AddressDto'
 *          - Id
 *          - Fname
 *          - Lname
 *          - Country
 *          - City
 *          - Street
 *     7. Go to AccountController Make EndPoint Return 'AddressDto'
 *     8. Return Ok(new AddressDto ()
 *          {
 *              - Use AutoMapper
 *                  - Go To Mapping Profiles
 *                      - CreateMap<Address , AddressDto>();
 *                          - Ask CTOR of AccountController to Inject Object From IMapper
 *                              - Add Line in EndPoint => var address = _mapper.Map<Address , AddressDto>(user.Address);
 *          });
 *     9. Return OK(address);
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (10) Update User Address :
 * ---------------------------
 *     1. Add EndPoint to 'AccountController'
 *          - [Autorize]
 *          - [HttpPut("address")]
 *          - Public
 *          - Return Action Result of AddressDto
 *          - Named UpdateUserAddress
 *          - Take AddressDto
 *              # 3awzen ngeb el user lly b address da w n3mlo update
 *              - h3ml mapper ll address lly gai mn AddressDto to Address w hya5od el updatedAddress
 *              - h3rf el user w hyklm el func llly kont 3amlha 
 *              - address.Id = user.Address.Id;
 *              - user.Address = address;
 *              - h3ml update b2a ll address => var result = _userManager.UpdateAsync(user);
 *              - Check if Succeded wla la lw fail hrg3lo badrequest 400
 *              - return ok(updatedAddress);
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (11) Validate Duplication EmailAddress :
 * -----------------------------------------
 *     1. Add EndPoint to 'AccountController'
 *          - [HttpGet("emailexist")]
 *          - Public
 *          - Return ActionResult of Bool
 *          - Named CheckEmailExist
 *          - Take email
 *              # 3awz at check if user bta3 el email da mwgod wla la
 *              - return _userManager.FindByEmailAsync(email) is not null;
 *     2. Use This EndPoint in Register EndPoint
 *     3. Go to RegisterEndPoint
 *          - if (CheckEmailExist(registerDto.Email).Result.Value)
 *              - return BadRequest(new ApiValidationErrorResponse() {Errors = new string[] {"Email Already Exists"}});
 * ---------------------------------------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------------------------------------                   
 * (12) Validate Customer Basket :
 * --------------------------------
 *     1. Add Class to Folder Dtos Named 'CustomerBasketDto'
 *          - Public
 *          - Take Prop of CustomerBasket and Paste it Here
 *              - Id [Required]
 *              - List<BasketItemDto>
 *                  - CTRL + . => Generate Type of ...new file
 *                      - Take Items Where in BasketItem and Paste it Here BasketItemDto all [Requried]
 *                          - Quantity has Range(1, double.MaxValue)
 *     2. Go to BasketController 
 *          - Refactor UpdateBasket
 *              - CustomerBasketDto
 *     3. We Need Make Mapping
 *     4. Go to CTOR and Ask CLR to Inject Object from IMapper
 *     5. h3rf el mapper fe Update
 *     6. Make Profile From CustomerBasketDto => CustomerBasket 
 *     7. Make Profile From BasketItemDto     => BasketItem
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */