---
title: MySQL Database Provider - EF Core
author: rowanmiller
ms.author: divega

ms.date: 10/27/2016

ms.assetid: 4900b882-79c5-40d2-a44a-ccb0292f6ed9
ms.technology: entity-framework-core

uid: core/providers/mysql/index
---
# MySQL EF Core Database Provider

This database provider allows Entity Framework Core to be used with MySQL. The provider is maintained as part of the [MySQL project](http://dev.mysql.com).

> [!WARNING]  
> This provider is pre-release.

> [!NOTE]  
> This provider is not maintained as part of the Entity Framework Core project. When considering a third party provider, be sure to evaluate quality, licensing, support, etc. to ensure they meet your requirements.

## Install

Install the [MySql.Data.EntityFrameworkCore NuGet package](https://www.nuget.org/packages/MySql.Data.EntityFrameworkCore).

``` powershell
Install-Package MySql.Data.EntityFrameworkCore -Pre
```

## Get Started

See [Starting with MySQL EF Core provider and Connector/Net 7.0.4](http://insidemysql.com/howto-starting-with-mysql-ef-core-provider-and-connectornet-7-0-4/).

## Supported Database Engines

* MySQL

## Supported Platforms

* .NET Framework (4.5.1 onwards)

* .NET Core

Be sure to review the MySQL documentation for version compatibility information [here](https://dev.mysql.com/doc/connector-net/en/connector-net-versions.html) and [here](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core.html)
