# Mappings Schemas

Métodos:

**` GetMapping`**
```r
    /// <summary>
    /// Devuelve el mapping que tiene un schema.
    /// </summary>       
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDatas json.</returns>
    public async Task<Mappings> GetMapping(string schema)
```