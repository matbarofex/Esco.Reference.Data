# Fields

Fondos:


**` getFund`**
```r
    /// <summary>
    /// Retorna un fondo por id
    /// </summary>
    /// <param name="id">(Requeried) Id del Fondo a filtrar.</param>
    /// <returns>Fund object result.</returns>
    public async Task<Fund> getFund(string id)
```

**` getFunds`**
```r
    /// <summary>
    /// Retorna una lista de fondos filtrado por campos específicos
    /// </summary>
    /// <param name="managment">(Optional) Filtar por Sociedad Administración (puede incluirse una cadena de búsqueda parcial)</param>
    /// <param name="depositary">(Optional) Filtar por Sociedad Depositoria (puede incluirse una cadena de búsqueda parcial)</param>
    /// <param name="currency">(Optional) Filtar por Moneda (Ejemplo: "ARS", "USD")</param>
    /// <param name="rentType">(Optional) Filtar por Tipo de Renta (puede incluirse una cadena de búsqueda parcial)</param>
    /// <returns>Funds object Result.</returns>
    public async Task<Funds> getFunds(string managment = null, string depositary = null, string currency = null, string rentType = null)
```

**` searchFunds`**
```r
    /// <summary>
    /// Retorna una lista de fondos que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Fondos a filtrar. Si es null devuelve todos los Fondos</param>
    /// <returns>Funds object Result.</returns>
    public async Task<Funds> searchFunds(string id)
```
