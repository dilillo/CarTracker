# Overview

This example code demonstrates how an application built on an Event Sourcing architecture could be scaled to meet the demands of a larger user base in a more production ready stance.

Specifically, it leverages CosmosDB as a datastore and takes advantage of the Change Feed feature to achieve background view updates.  While in this example the work done by the projectors in the background is still relatively trivial, it does serve to highlight the possibilities.

Additionally, this code begins to explore one of the key considerations for how Event Sourcing based applications need to be constructed in order to manage the needs of consumers for data of varying freshness.  This is of course use case driven but ultimately most applications will likely need to support realtime view updates, deferred view updates, and deferred view updates that must appear realtime (aka predictions) to one or more specific consumers.

In this work realtime and predictions are implemented using command handler and query handler implementations.  Deferred view updates are handled by Azure Functions reading the CosmosDB change feed and reacting accordingly.

While all of this works well there would be addition considerations to support other use cases like API consumers or SPA based applications.

At the end of the day the core concepts are very portable but individual implementations vary.

# Getting Started

1. Clone the repository
1. Either run the `scripts/setup-cosmosdb.ps1` (specify subscriptionName parameter if not Midmark Personal Subscription) or download the [Azure CosmosDB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator) and configure per the code in the script.
1. Open the solution in Visual Studio
1. Update the src/CarTacker.Projector/local.settings.json with the appropriate value for `CarTrackerOptions:CosmosDBConnectionString`
1. Update the src/CarTacker.Web/appsettings.Development.json with the appropriate value for `CarTrackerOptions:CosmosDBConnectionString`
1. Configure the solution for multiple startup projects and select `CarTracker.Web` and `CarTracker.Projector`

# Other Points of Interest

For a simpler example of Event Sourcing in action see the [SimpleEventSourcingExample](https://github.com/dilillo/SimpleEventSourcingExample) repo

For a fully baked example of CQRS with Mediator see the [MediatrSamples](https://github.com/dilillo/MediatrSamples) repo
