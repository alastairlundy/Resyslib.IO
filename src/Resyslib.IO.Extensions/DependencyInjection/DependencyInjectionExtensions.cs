﻿/*
    Resyslib.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using AlastairLundy.Resyslib.IO.Core.Directories;
using AlastairLundy.Resyslib.IO.Core.Files;
using AlastairLundy.Resyslib.IO.Core.Files.Concatenation;
using AlastairLundy.Resyslib.IO.Directories;

using AlastairLundy.Resyslib.IO.Files;
using AlastairLundy.Resyslib.IO.Files.Concatenation;

using Microsoft.Extensions.DependencyInjection;

// ReSharper disable InconsistentNaming

namespace AlastairLundy.Resyslib.IO.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {

        /// <summary>
        /// Sets up Dependency Injection for Resyslib.IO's interface-able types.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        /// <param name="lifetime">The service lifetime to use if specified; Singleton otherwise.</param>
        /// <returns>The updated service collection with the added Resyslib.IO's dependency injection setup.</returns>
        public static IServiceCollection AddResyslibIO(this IServiceCollection services,
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
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<IFileFinder, FileFinder>();
                    services.AddScoped<IFileAppender, FileAppender>();
                    services.AddScoped<IFileConcatenator, FileConcatenator>();
                    services.AddScoped<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                    services.AddScoped<IParentDirectoryManager, ParentDirectoryManager>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<IFileFinder, FileFinder>();
                    services.AddTransient<IFileAppender, FileAppender>();
                    services.AddTransient<IFileConcatenator, FileConcatenator>();
                    services.AddTransient<IRecursiveDirectoryManager, RecursiveDirectoryManager>();
                    services.AddTransient<IParentDirectoryManager, ParentDirectoryManager>();
                    break;
            }
        
            return services;
        }
    }
}