# glass-test


## Description

This is a litlte test project for an interview

## How to run tests
Inside the root folder, run this command:
```sh
dotnet test GlassTest.Documents.Tests/GlassTest.Documents.Tests.csproj;  
```
## How to run the console app

 At the Program.cs file in the `GlassTest.AppConsole` project, you need to expecify the next values:

 The values for your connection string:
```cs
var connectionString = "Server=localhost,1433;Database=MyDocuments;User Id=sa;Password=Gl@ssdbp@ss1;TrustServerCertificate=true";
```
 
 The query words for the matchAll execution:
```cs
var matchAllQuery = "dolor egestas rhoncus";
```

The query words for the matchAll in false execution:
```cs
var notMatchAllQuery = "Donec tempus, lorem fringilla ornare placerat, orci lacus vestibulum lorem, sit amet ultricies";
```

Then execute the app console with the next command:
```sh
dotnet run --project GlassTest.AppConsole/GlassTest.AppConsole.csproj 
```

You will see the count of result matches in both cases and the executed time.

##### Note: All this apply if you are not using Visual Studio. If you do, just run pushing the run button :-).
 