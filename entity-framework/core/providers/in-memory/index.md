---
title: InMemory Database Provider - EF Core
author: rowanmiller
ms.author: divega

ms.date: 10/27/2016

ms.assetid: 9af0cba7-7605-4f8f-9cfa-dd616fcb880c
ms.technology: entity-framework-core

uid: core/providers/in-memory/index
---
# EF Core In-Memory Database Provider

This database provider allows Entity Framework Core to be used with an in-memory database. This is useful when testing code that uses Entity Framework Core. The provider is maintained as part of the [EntityFramework GitHub project](https://github.com/aspnet/EntityFramework).

## Install

Install the [Microsoft.EntityFrameworkCore.InMemory NuGet package](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/).

``` console
PM> Install-Package Microsoft.EntityFrameworkCore.InMemory
```

## Get Started

The following resources will help you get started with this provider.
* [Testing with InMemory](../../miscellaneous/testing/in-memory.md)

* [UnicornStore Sample Application Tests](https://github.com/rowanmiller/UnicornStore/blob/master/UnicornStore/src/UnicornStore.Tests/Controllers/ShippingControllerTests.cs)

## Supported Database Engines

* Built-in in-memory database (designed for testing purposes only)

## Supported Platforms

* .NET Framework (4.5.1 onwards)

* .NET Core

* Mono (4.2.0 onwards)

* Universal Windows Platform
