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
    public static IServiceCollection AddExtensionsDotIO(this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<IFileFinder, FileFinder>();
                services.AddSingleton<IFileAppender, FileAppender>();
                services.AddSingleton<IFileConcatenator, FileConcatenator>();
                services.AddSingleton<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                services.AddSingleton<IParentDirectoryManager, ParentDirectoryManager>();
                services.AddSingleton<IFilePathResolver, FilePathResolver>();
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<IFileFinder, FileFinder>();
                services.AddScoped<IFileAppender, FileAppender>();
                services.AddScoped<IFileConcatenator, FileConcatenator>();
                services.AddScoped<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                services.AddScoped<IParentDirectoryManager, ParentDirectoryManager>();
                services.AddScoped<IFilePathResolver, FilePathResolver>();
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<IFileFinder, FileFinder>();
                services.AddTransient<IFileAppender, FileAppender>();
                services.AddTransient<IFileConcatenator, FileConcatenator>();
                services.AddTransient<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                services.AddTransient<IParentDirectoryManager, ParentDirectoryManager>();
                services.AddTransient<IFilePathResolver, FilePathResolver>();
                break;
        }
        
        return services;
    }
}