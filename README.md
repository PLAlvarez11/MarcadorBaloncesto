
## 📦 Base de Datos (`AppDbContext.cs`)

Este archivo define el **contexto de base de datos** usando **Entity Framework Core**.

### 🔹 Clase `AppDbContext`
- Hereda de `DbContext` y gestiona la conexión con la base de datos.
- Expone la entidad **Partido** mediante `DbSet<Partido>`.

### 🔹 Configuración de la entidad `Partido`
- **Tabla:** `Partidos`
- **Propiedades obligatorias:**
  - `EquipoLocal` (máx. 100 caracteres)
  - `EquipoVisitante` (máx. 100 caracteres)
- **Propiedades con valores por defecto:**
  - `PuntosLocal` → 0  
  - `PuntosVisitante` → 0  
  - `FaltasLocal` → 0  
  - `FaltasVisitante` → 0  
- **Índices creados en:**
  - `EquipoLocal`  
  - `EquipoVisitante`  

----------------





## Global Using en C#

### ¿Qué es un `using`?
En C#, normalmente al inicio de cada archivo agregamos `using` para importar espacios de nombres (namespaces).  




--------------




## 🌐 API Principal (`Program.cs`)

Este archivo define la configuración de la **API en .NET 8** usando **Minimal APIs**.

### 🔹 Configuración inicial
- **DbContext:** se inyecta `AppDbContext` con conexión a SQL Server (cadena `"DefaultConnection"` desde `appsettings.json`).  
- **CORS:** se habilita la política `AllowAngular` para permitir peticiones desde `http://localhost:4200` (el frontend en Angular).  
- **Swagger:** habilitado para probar la API y documentarla automáticamente.  

### 🔹 Middlewares
- `app.UseSwagger()` y `app.UseSwaggerUI()` → habilitan la documentación interactiva.  
- `app.UseCors(CorsPolicy)` → permite el acceso del frontend Angular a la API.  

---

### 🔹 Endpoints definidos

#### ✅ GET `/api/partidos`
Obtiene los **últimos 50 partidos** ordenados por fecha más reciente.  
```csharp
app.MapGet("/api/partidos", async (AppDbContext db) =>
{
    var data = await db.Partidos
        .OrderByDescending(p => p.FechaPartido)
        .Take(50)
        .ToListAsync();
    return Results.Ok(data);
});
