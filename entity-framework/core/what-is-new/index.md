---
title: What's new - EF Core
author: divega
ms.date: 02/20/2018
ms.assetid: C21F89EE-FB08-4ED9-A2A0-76CB7656E6E4
uid: core/what-is-new/index
---

# What's new in EF Core

## Recent releases

- **EF Core 3.0** (latest stable release) 
  - [New features](xref:core/what-is-new/ef-core-3.0/index) 
  - [Breaking changes](xref:core/what-is-new/ef-core-3.0/breaking-changes) that you should be aware of when upgrading
- [EF Core 2.2](xref:core/what-is-new/ef-core-2.2)
- [EF Core 2.1](xref:core/what-is-new/ef-core-2.1) (latest long-term support release)

## Product roadmap

> [!IMPORTANT]
> Please note that the feature sets and schedules of future releases are always subject to change, and although we will try to keep this page up to date, it may not reflect our latest plans at all times.

### Future releases

- **EF Core 3.1**  
  - Under active development
  - Will contain [small performance, quality and stability improvements](https://github.com/aspnet/EntityFrameworkCore/issues?q=is%3Aopen+is%3Aissue+milestone%3A3.1.0+sort%3Areactions-desc)
  - Planned as a long-term support (LTS) release
  - Currently scheduled for December 2019
- **EF Core "vNext"**   
  - Next major release of EF Core to ship alongside .NET 5
  - Planning for this release hasn't started yet and no features have been announced.  

### Schedule

The release schedule for EF Core is in-sync with the [.NET Core release schedule](https://github.com/dotnet/core/blob/master/roadmap.md).

### Backlog

The [Backlog Milestone](https://github.com/aspnet/EntityFrameworkCore/issues?q=is%3Aopen+is%3Aissue+milestone%3ABacklog+sort%3Areactions-%2B1-desc) in our issue tracker contains issues that we either expect to work on someday, or we think someone from the community could tackle.
Customers are welcome to submit comments and votes on these issues.
Contributors looking to work on any of these issues are encouraged to first start a discussion on how to approach them.

There's never a guarantee that we'll work on any given feature in a specific version of EF Core.
As in all software projects, priorities, release schedules, and available resources can change at any point.
But if we intend to resolve an issue in a specific timeframe, we'll assign it to a release milestone instead of the backlog milestone.
We routinely move issues between the backlog and release milestones as part of our [release planning process](#release-planning-process).

We'll likely close an issue if we don't plan to ever address it.
But we can reconsider an issue that we previously closed if we get new information about it.

### Release planning process

We often get questions about how we choose specific features to go into a particular release.
Our backlog certainly doesn't automatically translate into release plans.
The presence of a feature in EF6 also doesn't automatically mean that the feature needs to be implemented in EF Core.

It's difficult to detail the whole process we follow to plan a release.
Much of it is discussing the specific features, opportunities and priorities, and the process itself also evolves with every release.
However, we can summarize the common questions we try to answer when deciding what to work on next:

1. **How many developers we think will use the feature and how much better will it make their applications or experience?** To answer this question, we collect feedback from many sources — Comments and votes on issues is one of those sources.

2. **What are the workarounds people can use if we don't implement this feature yet?** For example, many developers can map a join table to work around lack of native many-to-many support. Obviously, not all developers want to do it, but many can, and that counts as a factor in our decision.

3. **Does implementing this feature evolve the architecture of EF Core such that it moves us closer to implementing other features?** We tend to favor features that act as building blocks for other features. For instance, property bag entities can help us move towards many-to-many support, and entity constructors enabled our lazy loading support.

4. **Is the feature an extensibility point?** We tend to favor extensibility points over normal features because they enable developers to hook their own behaviors and compensate for any missing functionality.

5. **What is the synergy of the feature when used in combination with other products?** We favor features that enable or significantly improve the experience of using EF Core with other products, such as .NET Core, the latest version of Visual Studio, Microsoft Azure, and so on.

6. **What are the skills of the people available to work on a feature, and how to best leverage these resources?** Each member of the EF team and our community contributors has different levels of experience in different areas, so we have to plan accordingly. Even if we wanted to have "all hands on deck" to work on a specific feature like GroupBy translations, or many-to-many, that wouldn't be practical.

As mentioned before, the process evolves on every release.
In the future, we'll try to add more opportunities for members of the community to provide inputs into our release plans.
For example, we would like to make it easier to review draft designs of the features and of the release plan itself.