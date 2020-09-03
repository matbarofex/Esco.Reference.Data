# Fields

Métodos:

**` getFields`**
```r
    /// <summary>
    /// Devuelve la lista completa de fields.
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns> FieldsList object result.</returns>
    public async Task<FieldsList> getFields(string schema )        
```

**` getField`**
```r
    /// <summary>
    /// Devuelve un field con un id específico.
    /// </summary>
    /// <param name="id">(Required) Id del Field a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns> Field object result.</returns>
    public async Task<Field> getField(string id, string schema)
```
