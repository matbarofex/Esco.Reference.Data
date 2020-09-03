# Derivatives

Métodos:

**` getDerivatives`**
```r
    /// <summary>
    /// Retorna una lista de derivados
    /// </summary>
    /// <param name="marketSegmentId">(Optional) Id del segmento de mercado (Ej: "DDA", "MATBA", puede incluirse una cadena de búsqueda parcial)</param>
    /// <param name="underlyingSymbol">(Optional) Símbolo del Derivado (Ej: "Indice Novillo", puede incluirse una cadena de búsqueda parcial).</param>
    /// <returns>Derivatives object Result.</returns>
    public async Task<Derivatives> getDerivatives(string marketSegmentId = null, string underlyingSymbol = null)
```

**` searchDerivatives`**
```r
    /// <summary>
    /// Retorna una lista de derivados que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Derivados a filtrar.</param>        
    /// <returns>Instruments object Result.</returns>
    public async Task<Derivatives> searchDerivatives(string id)
```

**` getMarketSegments`**
```
    /// <summary>
    /// Retorna una lista de Segmentos de mercado de derivados (MarketSegmentId).
    /// </summary>       
    /// <returns>MarketSegments object Result.</returns>
    public async Task<MarketSegments> getMarketSegments()
```

**` getUnderlyingSymbols`**
```
	/// <summary>
    /// Retorna una lista de Símbolos de derivados (underlyingSymbol).
    /// </summary>       
    /// <returns>MarketSegments object Result.</returns>
    public async Task<UnderlyingSymbols> getUnderlyingSymbols()
```