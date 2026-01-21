# Producto API - GraphQL Integration

API RESTful y GraphQL para gestión de productos desarrollada con .NET 8, implementando arquitectura limpia (Clean Architecture), CQRS (Command Query Responsibility Segregation) y patrón repositorio.

## 🚀 Características

- ✅ **Arquitectura Limpia**: Separación en capas (Domain, Application, Infrastructure, API)
- ✅ **CQRS**: Separación de comandos (escritura) y consultas (lectura) usando MediatR
- ✅ **Patrón Repositorio**: Abstracción del acceso a datos
- ✅ **Entity Framework Core**: ORM para SQL Server con separación Read/Write
- ✅ **Swagger/OpenAPI**: Documentación interactiva de la API REST
- ✅ **GraphQL**: API GraphQL usando HotChocolate
- ✅ **Banana Cake Pop**: IDE integrado para GraphQL
- ✅ **Validaciones**: FluentValidation para validación de comandos
- ✅ **Manejo de Errores**: Manejo global de excepciones

## 📋 Framework y Librerías

- **Framework**: ASP.NET Core 8 (.NET 8)
- **Librería GraphQL**: HotChocolate versión 13.9.7
- **Gestor de Paquetes**: NuGet

## 🔧 Instalación

### Requisitos Previos

- .NET 8 SDK
- SQL Server (LocalDB o SQL Server Express)
- Visual Studio 2022 o Visual Studio Code

### Pasos de Instalación

1. **Clonar el repositorio**
   ```bash
   git clone <url-del-repositorio>
   cd ApiProductoGraphQL/arq_aplicaciones_web_api_producto
   ```

2. **Restaurar paquetes NuGet**
   ```bash
   dotnet restore
   ```

3. **Configurar las cadenas de conexión**

   Editar `src/ProductoAPI.API/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "ReadConnection": "Server=tu-servidor;Database=ProductoDB;...",
     "WriteConnection": "Server=tu-servidor;Database=ProductoDB;..."
   }
   ```

4. **Ejecutar la aplicación**
   ```bash
   cd src/ProductoAPI.API
   dotnet run
   ```

## 🌐 Endpoints Disponibles

### REST API
- **Swagger UI**: `https://localhost:5001/swagger`
- **Base URL**: `https://localhost:5001/api/productos`

### GraphQL
- **GraphQL Endpoint**: `https://localhost:5001/graphql`
- **GraphQL IDE (Banana Cake Pop)**: `https://localhost:5001/graphql/ide`

## 📝 Ejemplos de Uso GraphQL

### Query - Obtener todos los productos:

```graphql
{
  productos {
    id
    nombre
    descripcion
    precio
  }
}
```

### Query - Obtener producto por ID:

```graphql
{
  productoById(id: 1) {
    id
    nombre
    descripcion
    precio
  }
}
```

### Mutation - Crear producto:

```graphql
mutation {
  createProducto(
    nombre: "Laptop Dell XPS 15"
    descripcion: "Laptop de alta gama"
    precio: 1299.99
  ) {
    id
    nombre
    precio
  }
}
```

### Mutation - Actualizar producto:

```graphql
mutation {
  updateProducto(
    id: 1
    nombre: "Laptop Actualizada"
    descripcion: "Nueva descripción"
    precio: 1399.99
  ) {
    id
    nombre
    precio
  }
}
```

### Mutation - Eliminar producto:

```graphql
mutation {
  deleteProducto(id: 1)
}
```

## 📚 Documentación

- **Documentación HotChocolate**: https://chillicream.com/docs/hotchocolate

## 🏗️ Estructura del Proyecto

```
ProductoAPI/
├── src/
│   ├── ProductoAPI.Domain/          # Capa de dominio (entidades, interfaces)
│   ├── ProductoAPI.Application/     # Capa de aplicación (CQRS, validaciones)
│   ├── ProductoAPI.Infrastructure/  # Capa de infraestructura (repositorios, EF Core)
│   └── ProductoAPI.API/             # Capa de presentación
│       ├── Controllers/             # REST Controllers
│       ├── GraphQL/                 # GraphQL
│       │   ├── Queries/             # GraphQL Queries
│       │   └── Mutations/           # GraphQL Mutations
│       └── Program.cs               # Configuración y startup
└── ProductoAPI.sln
```

## 📦 Paquetes NuGet Utilizados

### ProductoAPI.API
- `Swashbuckle.AspNetCore` 6.5.0 - Swagger/OpenAPI
- `HotChocolate.AspNetCore` 13.9.7 - Servidor GraphQL
- `HotChocolate.Data.EntityFramework` 13.9.7 - Integración EF Core

### ProductoAPI.Application
- `MediatR` 12.2.0 - CQRS
- `FluentValidation` 11.9.0 - Validaciones

### ProductoAPI.Infrastructure
- `Microsoft.EntityFrameworkCore` 8.0.0 - EF Core
- `Microsoft.EntityFrameworkCore.SqlServer` 8.0.0 - SQL Server provider

## 🎯 Ventajas de GraphQL

1. **Consultas Declarativas**: El cliente solicita solo los campos que necesita
2. **Una sola solicitud**: Obtener múltiples recursos en una sola petición
3. **Tipado fuerte**: Schema tipado que facilita el desarrollo
4. **Banana Cake Pop**: IDE integrado para probar queries
5. **Evolución del API**: Agregar campos sin afectar consultas existentes

## 📄 Licencia

Este proyecto es de uso académico.
