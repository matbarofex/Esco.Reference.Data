# Securities

Métodos:


**` getSecuritie`**
```r
    /// <summary>
    /// Retorna un titulo valor por id
    /// </summary>
    /// <param name="id">(Requeried) Id del título valor a filtrar.</param>
    /// <returns> Securitie object result.</returns>
    public async Task<Securitie> getSecuritie(string id)
```

**` getSecurities`**
```r
    /// <summary>
    /// Retorna una lista de títulos valores
    /// </summary>
    /// <param name="id">(Optional) Cadena de búsqueda de los títulos valores a filtrar. Si es null devuelve todos los títulos valores</param>
    /// <returns> Securities object result.</returns>
    public async Task<Securities> getSecurities(string id = null)
```
