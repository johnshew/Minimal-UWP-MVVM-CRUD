# Minimal-UWP-MVVM-CRUD
Minimal MVVM UWP App with CRUD over Data Service

## A Minimal MVVM UWP App  Implementing CRUD over Data
With the introduction of Win 10 and Universal Windows apps I wanted to try out some of the new XAML features like compile time binding with x:Bind. 

To start I looked around for a very simple Universal Windows Platform (UWP) sample that showed how to build an MVVM app with create, update, and delete (CRUD) operations over some data source.  Surprisingly, I really didn’t find anything specific for UWPs.  Of course there are many samples for WPF but they required a rewrite for UWP and they don’t leverage the new XAML capabilities making them more complicated than necessary. Similarly the core UWP XAML samples don’t show how to do CRUD and while they covered new capabilities they include a lot of unrelated stuff that makes them less useful as a starting point for your own apps.

So the question is how simple can we make an MVVM UWP example and still support full CRUD?

As you can see below the ViewModel for the resulting app is less than 100 lines of code – which is pretty reasonable since we need to cover all the corner cases associated with adds, updates, and deletes – and get those connected to the data layer.  The XAML View is also simple – only 25 lines of XAML and no code behind other than initialization.

If you work through this example, you can see how it uses the new x:Bind capability. Creating and debugging bindings in XAML has previously been a little touchy but now with x:Bind you get typesafe code a compile-time which makes it easier to get the bindings right and they run faster.  In addition the new x:Bind event capability reduces the complexity of implementing operations on the ViewModel.

# Why MVVM?

Before we look at the code, I wanted to touch on whether it is worth the effort to use an approach like MVVM (Model, View, ViewModel).  The basics of MVVM are pretty simple…

* Model.  Think of the models as the business objects.  A Model should know about nothing about the user experience - specifically the View or how it is implemented using a ViewModel.  The Model only interact with system and data services.  

* ViewModel.  The ViewModel is where you encapsulate any code or data that your UX or View will need.  It is important that ViewModel only knows about and encapsulates the Model – but it shouldn’t be responsible for any Business Logic or Business Constraints – that should all be in the model. And on the other side, the ViewModel should know nothing about the specifics of the Views and UX that use it.

* View.  This is where you create the user experience – in Windows apps you do this declaratively with XAML markup language and design tools.  The View uses properties and actions on the ViewModel to get the job done.

Since the MVVM approach is so conceptually simple, why do a lot of people get frustrated with it?  One challenge was that historically the approach was fairly opinionated requiring base classes like DependencyObject and implementations often required a lot of coding to do simple things like connecting up a command to XAML.  Another challenge was that a lot of developers felt that execution overhead of runtime bindings – and challenges debugging runtime bindings – ultimately meant they needed to abandon binding and simply do imperative code to implement the UX.

With Windows 10 and Visual Studio 2015 many of the challenges are have been addressed. As we show we can now easily build an MVVM app using compile-time bindings that eliminate runtime overhead and reduce the likelihood you will need to debug bindings at runtime. We also show how x:Bind to methods means we don’t need to implement Commands and we show how a new C# language feature [CallerMemberName] can help make it easier to implement INotificationPropertyChanged.

MVVM was already a well-proven approach that has been used to build many sophisticated and great performing apps and now with these new capabilities MVVM is more approachable than ever.

An Overview of the App 

One of the key goals was to keep this example as simple as possible. But we also want the example to represent what a developer would actually need to do to build an app using MVVM. And getting all the CRUD operations in place over a real world database or other system isn’t trivial. So here is how we approach this.

At the core of the app is a Business Object – our Model.  It is called Organization.  It is just an in-memory “database” of People objects and an organization Name.  The People collection is just a list where each Person have a Name and Age. 

In terms of implementing the model we leverage some scaffolding code that implements a FakeDataService that pretends to communicate with a cloud service that maintains the data – but here our stubbed out service to run locally and just prints messages for debugging / logging purposes.  Again, the only reason to do this is so we have a realistic sense of what would be involved with building this over real world data and business objects.

In terms of the user experience all we need is a View that shows the list of people in the Organization and enables the user to Add or Delete People, or Update their Name and Age properties – nothing complicated.  Our View is written entirely in XAML with no code behind other than initializing the ViewModel.

Most of code in the sample is focused on building the ViewModel for the Organization business object. The OrganizationViewModel keeps track of a collection of People that is kept in sync with the model. It also keeps track of a SelectedPersonIndex from the collection of people so the View can perform edit and delete operations on the current selection.

All the code can be found here on GITHUB.
