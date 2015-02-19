# TFS REST API with Nancy

Microsoft offer an API to access TFS programmatically which allow you to write apps and VS extensions to extend TFS. This is fine as long as you don't mind writing .NET code. TFS REST API offers a nice layer between the TFS API and your preferred language of choice. 

This project is written in C# and Nancy. I'm not creating a comprensive mapping so you're welcome to fork and extend as you see fit.

## What can it do?

	// List all changesets of a branch from a point in time. Where $/project is TFS path to search,
	/changesets?projectPath=$/project&from=2015/02/06

	// Details of a single Changeset (by changeset ID),
	/changeset/{id}

	// List of branches of a branch (deleted branches should be ignored). Where $/project is TFS path to search,
	/branches?source=$/project

	// Changesets that have not been merged between two (2) branches,
	/merge/candidates?source=$/project1&destination=$/branch2

## How to build

1. Grab the source and run Build.cmd,
2. Or open in Visual Studio, tweak and build,
3. The only catch will be having Visual Studio installed to resolve GAC references to the TFS API dependencies.

You'll also need to add and populate `appSettings.config` in the website root with connection details for TFS. Here's a sample of the file

```xml
<?xml version="1.0" encoding="UTF-8"?>
<appSettings>
    <add key="TfsUsername" value=""/>
    <add key="TfsPassword" value=""/>
    <add key="TfsDomain" value=""/>
    <add key="TfsUseDomainCredentials" value="true"/>
    <add key="TfsUrl" value="https://url:443/tfs/collection"/>
    <add key="TfsProjectPath" value="$/"/>
</appSettings>
```

## How to Deploy

TBA