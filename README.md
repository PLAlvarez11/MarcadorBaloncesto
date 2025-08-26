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

---
