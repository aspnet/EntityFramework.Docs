---
title: Required/optional properties - EF Core
author: rowanmiller
ms.date: 10/27/2016
ms.assetid: ddaa0a54-9f43-4c34-aae3-f95c96c69842
uid: core/modeling/required-optional
---
# Required and Optional Properties

A property is considered optional if it is valid for it to contain `null`. If `null` is not a valid value to be assigned to a property then it is considered to be a required property.

## Conventions

By convention, a property whose .NET type can contain null will be configured as optional (`string`, `int?`, `byte[]`, etc.). Properties whose CLR type cannot contain null will be configured as required (`int`, `decimal`, `bool`, etc.).

> [!NOTE]  
> A property whose .NET type cannot contain null cannot be configured as optional. The property will always be considered required by Entity Framework.

## Data Annotations

You can use Data Annotations to indicate that a property is required.

[!code-csharp[Main](../../../samples/core/Modeling/DataAnnotations/Required.cs?highlight=14)]

## Fluent API

You can use the Fluent API to indicate that a property is required.

[!code-csharp[Main](../../../samples/core/Modeling/FluentAPI/Required.cs?highlight=11-13)]

