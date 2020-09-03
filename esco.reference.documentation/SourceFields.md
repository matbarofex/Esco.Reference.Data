# Source Fields

Métodos:

**` getSourceFields`**
```r
    /// <summary>
    /// Devuelve la lista completa de source fields.
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se el activo.</param>
    /// <returns> SourceFields  object result.</returns>
    public async Task<SourceFields> getSourceFields(string schema)    
```

**` getSourceField`**
```r
    /// <summary>
    /// Devuelve un source field con un id específico.
    /// </summary>
    /// <param name="id">(Requeried) Id del Source Field a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se el activo.</param>
    /// <returns> SourceField object result.</returns>
    public async Task<SourceField> getSourceField(string id , string schema)
```
