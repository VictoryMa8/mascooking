# Web API w/ C#/ASP.NET Notes (part 1)
We used `dotnet new webapi --use-controllers` to start a new web API project

`dotnet new` creates a project from a template
`webapi` selects an HTTP API project
`--use-controllers` puts request-handling code in controller classes
`--name recipes` names the projec and creates a recipes subfolder

RecipeBox is the project name, 'cause we're creating a "box" of recipes.

# Map of what was created with the commanad
`RecipeBox.slnx` groups our API (RecipeBox.Api) and test projects (RecipeBox.Tests) so tools can work with them together.
