// This code (recipes/Program.cs) executes when our app starts

// BUILDER !!!
var builder = WebApplication.CreateBuilder(args); // Creates a `builder`, used to "configure" our web app before starting it

// `args` contains any command-line arguments passed to our app (the server isn't running yet)
// dotnet run -- --potato <-- that potato will be in args as args[0]


/// SERVICES !!!
builder.Services.AddControllers(); // registers the framework services needed to use `controllers`

builder.Services.AddOpenApi(); // registers the services that generate our API's OpenAPI description (list of URLs, inputs, etc.)

/*
    OpenAPI is the "format" describing our API
    Swagger UI turns that description into an interactive webpage where we can try requests
    AddOpenApi() supplies only the description-generation services, no webpage before swagger
*/

// BUILDER.BUILD !!!
var app = builder.Build(); // creates the app using the configuration and services we registered above

// `builder` was for preparing the app, now `app` lets us configure how it handles incoming requests (SERVER STILL NOT STARTED AT THIS POINT)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // ONLY WHEN THE APP's ENVIRONMENT == DEVELOPMENT
{
    app.MapOpenApi(); // Exposes the OpenAPI description at /openapi/v1.json for clients
}

app.UseHttpsRedirection(); // Redirects incoming HTTP requests to HTTPS when an HTTPS port is configured

// HTTPS encrypts traffic between the client and our server (THIS IS AN EXAMPLE OF MIDDLEWARE: code that processes requests before they reach our controller)

app.UseAuthorization(); // adds middleware that checks whether a caller is "allowed" to access an endpoint (based on rules we configure) [it doesn't automatically protect every endpoint, but something like [Authorize] can deny access with this]

app.MapControllers(); // makes controller actions reachable thru their route attributes
// for example, for the default weather app, WeatherForecastController uses [Route("[controller]")] and [HttpGet] to handle GET /weatherforecast

// earlier, AddControllers() registered the supporting services, MapControllers() connects the controller endpoints to request routing

// RUN !!!
app.Run(); // starts the WEB SERVER so it can LISTEN FOR REQUESTS
// keeps the APPLICATION RUNNING until SHUTDOWN (i.e. ctrl+c in terminal)
