# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2025-06-17

### Added

- **Separation of Concerns**: New modular architecture with distinct interfaces

  - Added `IModule` marker interface for type safety across all module types
  - Added `IServiceModule` interface for dependency injection configuration
  - Added `IMiddlewareModule` interface for middleware pipeline configuration

- **Explicit Middleware Ordering**: Full control over middleware execution order

  - Added `MiddlewarePipelineBuilder` class for fluent middleware pipeline configuration
  - Added `ConfigureMiddlewarePipeline()` extension method for explicit middleware ordering
  - Support for mixing module-based middleware with regular middleware configuration

- **New Service Registration**: Improved module registration approach

  - Added `AddServiceModules()` extension method for registering service modules
  - Service modules are registered independently from middleware configuration
  - Order of service module registration doesn't affect middleware execution order

- **Enhanced Examples**: Comprehensive examples showing best practices
  - Added examples demonstrating service and middleware module separation
  - Added examples showing explicit middleware pipeline configuration
  - Added usage patterns and development tools modules

### Changed

- **BREAKING CHANGE**: Recommended architecture pattern has fundamentally changed
  - Service configuration and middleware configuration are now separate concerns
  - Middleware ordering is now explicit rather than implicit
  - Module registration pattern has been redesigned for better control

### Deprecated

- **AppModule class**: The original `AppModule` base class is now marked as obsolete

  - Still functional for backward compatibility but shows deprecation warnings
  - Will be removed in a future major version
  - Migration path: Split into `IServiceModule` and `IMiddlewareModule` implementations

- **Legacy extension methods**: Original module registration methods are now obsolete
  - `AddModules()` method marked as obsolete, use `AddServiceModules()` instead
  - `UseModules()` method marked as obsolete, use `ConfigureMiddlewarePipeline()` instead
  - Both methods still work but show deprecation warnings

### Compatibility

- **Full Backward Compatibility**: All existing code continues to work

  - Legacy `AppModule` implementations still function correctly
  - Original `AddModules()` and `UseModules()` methods remain operational
  - Deprecation warnings guide users toward the improved architecture

- **Migration Path**: Clear upgrade path to the new architecture

  ```csharp
  // Legacy approach (still works, shows warnings)
  builder.AddModules(typeof(MyModule));
  app.UseModules();

  // New approach (recommended)
  builder.AddServiceModules(typeof(MyServiceModule));
  app.ConfigureMiddlewarePipeline()
     .AddModule<MyMiddlewareModule>()
     .Build();
  ```

### Benefits

- **No More Order Dependencies**: Service module registration order no longer affects middleware execution
- **Explicit Control**: Middleware pipeline is clearly visible and controllable in `Program.cs`
- **Better Separation**: Service configuration is completely separate from middleware configuration
- **Enhanced Flexibility**: Mix module-based and traditional middleware configuration seamlessly
- **Improved Testability**: Service and middleware concerns can be tested independently
- **Type Safety**: Strong typing through dedicated interfaces prevents configuration mistakes

## [1.0.0] - 2025-04-18

### Added

- Initial release of Zooper.Squid modular architecture library
- Core `AppModule` base class for creating modular components
- `AddModules()` extension method for module registration
- `UseModules()` extension method for middleware configuration
- Support for automatic service registration and middleware pipeline configuration
- Comprehensive documentation and examples
- MIT license and NuGet package configuration
