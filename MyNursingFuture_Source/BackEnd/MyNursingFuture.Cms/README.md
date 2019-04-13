##MyNursingFuture CMS

Empty MVC5 Template Generated with VS2017

Extra Nuget Packages

-SimpleInjector for DI
-Automapper


### Configurations
Allows to insert and delete Domains. True on development
```
<add key="DomainEdition" value="true"/>
```
Connnection string

```
  <connectionStrings>
    <add name="MyNursingFutureConnection" connectionString="Data Source=.\SQLEXPRESS2016;Initial Catalog=MyNursingFuture;User ID=**;Password=*******;Integrated Security=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
```

###Simple Injector Configuration:

Configuration for SimpleInjector in App_Start/SimpleInjectorConfig.cs

Example adding new services:

```
private static void RegisterServices(Container container)
{
    // Register your types, for instance:
    // Other services added...
    container.Register<ISectionManager, SectionManager>(Lifestyle.Transient);
	
}
```

Example using service on a Controller:

```
public class TestController: Controller
{
	private readonly IAddedService _addedService;
	
	public TestController(IAddedService addedService){
		_addedService = addedService;
	}

	public ActionResult DoSomething(){		
		_addedService.doMethod();
		return View();
	}
}
```

###Automapper:

Automapper is added as a service on the DI configuration.

Automapper Profile in the same file as the ViewModel that they are mapping. All profiles are loaded while configuring DI container.

Example  Profile:
```
public class SectionViewModel{
	//properties
}

public class SectionProfile:Profile{
	public SectionProfile(){
		//configure mapper inside the constructor
		CreateMap<Section, SectionViewModel>().ReverseMap();
	}
}
```

Using autommaper in a controller:

```
public class TestController: Controller
{
	private readonly IMapper _mapper;	
	public TestController(IMapper mapper){
		_mapper = mapper
	}

	public ActionResult DoSomething(){
		//get the entity
		var model = _mapper.Map<SectionEntity, SectionViewModel>(entity);
		return View(model);
	}
}
```