/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.Extensions.IO.Directories;
using AlastairLundy.Extensions.IO.Directories.Abstractions;
using AlastairLundy.Extensions.IO.Files;
using AlastairLundy.Extensions.IO.Files.Abstractions;
using AlastairLundy.Extensions.IO.Files.Concatenation;
using AlastairLundy.Extensions.IO.Files.Concatenation.Abstractions;

using Microsoft.Extensions.DependencyInjection;
// ReSharper disable InconsistentNaming

namespace AlastairLundy.Extensions.IO.DependencyInjection;

public static class DependencyInjectionExtensions
{

    /// <summary>
    /// Sets up Dependency Injection for IO Extensions' interface-able types.
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="lifetime">The service lifetime to use if specified; Singleton otherwise.</param>
    /// <returns>The updated service collection with the added IO Extensions dependency injection setup.</returns>
    public static IServiceCollection AddIOExtensions(this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services = services.AddSingleton<IFileFinder, FileFinder>();
                services = services.AddSingleton<IFileAppender, FileAppender>();
                services = services.AddSingleton<IFileConcatenator, FileConcatenator>();
                services = services.AddSingleton<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                services = services.AddSingleton<IParentDirectoryManager, ParentDirectoryManager>();
                services = services.AddSingleton<IFilePathResolver, FilePathResolver>();
                break;
            case ServiceLifetime.Scoped:
                services = services.AddScoped<IFileFinder, FileFinder>();
                services = services.AddScoped<IFileAppender, FileAppender>();
                services = services.AddScoped<IFileConcatenator, FileConcatenator>();
                services = services.AddScoped<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                services = services.AddScoped<IParentDirectoryManager, ParentDirectoryManager>();
                services = services.AddScoped<IFilePathResolver, FilePathResolver>();
                break;
            case ServiceLifetime.Transient:
                services = services.AddTransient<IFileFinder, FileFinder>();
                services = services.AddTransient<IFileAppender, FileAppender>();
                services = services.AddTransient<IFileConcatenator, FileConcatenator>();
                services = services.AddTransient<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                services = services.AddTransient<IParentDirectoryManager, ParentDirectoryManager>();
                services = services.AddTransient<IFilePathResolver, FilePathResolver>();
                break;
        }
        
        return services;
    }
}