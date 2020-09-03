# Mappings

Métodos:

**` getMapping`**
```r
    /// <summary>
    /// Devuelve un mapping para un id específico.
    /// </summary>
    /// <param name="id">(Requeried) Id del Mapping a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se el activo.</param>
    /// <returns> Mapping object result.</returns>
    public async Task<Mapping> getMapping(string id, string schema )      
```

**` getMappings`**
```r
    /// <summary>
    /// Devuelve una lista de mappings.
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se el activo.</param>
    /// <returns> Mappings object result.</returns> 
    public async Task<Mappings> getMappings(string schema )
```
