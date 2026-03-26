# 🏋️ Gym Management API

**REST API para gestión de gimnasios con seguridad empresarial**

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white) ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black) ![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)

---

## 📌 Descripción

API RESTful desarrollada en **.NET** orientada a la gestión de gimnasios. Implementa un sistema completo de seguridad con **autenticación JWT**, **autorización RBAC con políticas dinámicas**, control de tráfico mediante **Rate Limiting** y documentación interactiva con **Swagger**.

Diseñada con **arquitectura por capas**, separación de responsabilidades y buenas prácticas de desarrollo backend.

---

## ✨ Características principales

| Categoría                | Detalle                                                             |
| ------------------------ | ------------------------------------------------------------------- |
| 🔐 **Autenticación**     | JWT con configuración por `appsettings`                             |
| 🛡️ **Autorización**      | RBAC + Claims + Policies dinámicas (`IAuthorizationPolicyProvider`) |
| ⚡ **Rate Limiting**     | Control de tráfico configurable por endpoint                        |
| 📄 **Documentación**     | Swagger con XML comments y versionado                               |
| 🌐 **Versionado de API** | Soporte multi-versión desde la arquitectura base                    |
| ❌ **Manejo de errores** | Middleware global de excepciones con respuestas estandarizadas      |
| 🗄️ **Base de datos**     | PostgreSQL con Entity Framework Core y Migrations                   |
| 🌍 **CORS**              | Configuración por entorno (Development / Production)                |

---

## 🔒 Sistema de Seguridad

Este proyecto implementa un modelo de seguridad en dos capas:

### 1. RBAC clásico (Roles y Permisos)

Control de acceso basado en roles asignados al usuario. Cada rol agrupa un conjunto de permisos que determinan las acciones disponibles.

### 2. RBAC + Claims + Policies dinámicas

Las políticas de autorización se generan **en tiempo de ejecución** mediante un `IAuthorizationPolicyProvider` personalizado, eliminando la necesidad de declarar cada política de forma estática.

```csharp
// Las policies se crean dinámicamente según el permiso requerido
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermisoPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermisoHandler>();
```

---

## 🏗️ Arquitectura por capas

```
Backend_Gimnacio/
├── Controllers/        → Endpoints HTTP, validación de entrada
├── Services/           → Lógica de negocio y orquestación
├── Repositories/       → Acceso a datos, operaciones CRUD
├── Models/             → Entidades del dominio
├── DTOs/               → Objetos de transferencia de datos
├── Data/               → DbContext y configuración EF Core
├── Extensions/         → Métodos de extensión para Program.cs
├── Middleware/         → Manejo de Middlewares
├── Security/
│   ├── Policies/       → PermisoPolicyProvider (policies dinámicas)
│   └── Handlers/       → PermisoHandler (lógica de validación)
├── Enums/              → Enumeraciones del dominio
└── Program.cs          → Composición de servicios y pipeline
```

### Pipeline de Middleware

```
Request → GlobalExceptionHandler → HTTPS Redirect → CORS
        → RateLimiter → Authentication (JWT) → Authorization → Controller
```

---

## 🛠️ Stack tecnológico

- **Framework:** ASP.NET Core (.NET 8)
- **Lenguaje:** C#
- **ORM:** Entity Framework Core
- **Base de datos:** PostgreSQL
- **Autenticación:** JWT Bearer
- **Documentación:** Swashbuckle (Swagger)
- **Control de acceso:** Políticas dinámicas con `IAuthorizationPolicyProvider`

---

## 👤 Autor

Desarrollado como proyecto de práctica para consolidar habilidades en backend con .NET.

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/kevin-torrez-pillco-513770323)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/ketopi)
