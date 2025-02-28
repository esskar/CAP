﻿// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using DotNetCore.CAP.Persistence;
using DotNetCore.CAP.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace DotNetCore.CAP;

internal class PostgreSqlCapOptionsExtension : ICapOptionsExtension
{
    private readonly Action<PostgreSqlOptions> _configure;

    public PostgreSqlCapOptionsExtension(Action<PostgreSqlOptions> configure)
    {
        _configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(new CapStorageMarkerService("PostgreSql"));
        services.Configure(_configure);
        services.AddSingleton<IConfigureOptions<PostgreSqlOptions>, ConfigurePostgreSqlOptions>();

        services.AddSingleton<IDataStorage, PostgreSqlDataStorage>();
        services.TryAddSingleton<IStorageInitializer, PostgreSqlStorageInitializer>();
    }
}