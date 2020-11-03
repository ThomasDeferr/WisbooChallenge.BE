# Wisboo Challenge (BackEnd)

## Overview

Wisboo challenge built on ASP.NET Core 3.1 using API REST.

## Features (Endpoints)

- Videos list
- Video comments
- Upload/Update/Delete video

## Running this project locally

1. Clone this project locally
2. Run ```dotnet restore``` in your command line
3. Run ```dotnet build``` in your command line
4. Create a database and execute the scripts that are in ***WisbooChallenge.DatabaseScripts***
5. In ***WisbooChallenge.Api*** create a file ***appsettings.Development.json*** with the same content that ***appsettings.json***
6. Add the following code to ***appsettings.Development.json***: ```"DefaultConnection": { "ConnectionString": "YourConnectionString" }```
3. Run ```dotnet run -p WisbooChallenge.Api``` in your command line

## Dependencies

- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [NewtonsoftJson](https://github.com/JamesNK/Newtonsoft.Json)

