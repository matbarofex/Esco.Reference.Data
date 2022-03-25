# Reference Data

Métodos:

**` GetReferenceDataTodayUpdated`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos actualizados en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa. </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo </param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> GetReferenceDataTodayUpdated(string type, string schema )
```

**` GetReferenceDataTodayAdded`**
``` 
    /// <summary>
    /// Retorna la lista de instrumentos dados de alta en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> GetReferenceDataTodayAdded(string type , string schema )     
```   

**` GetReferenceDataTodayRemoved`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos dados de baja en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task <ReferenceDatas> GetReferenceDataTodayRemoved(string type , string schema )     
```

**` GetReferenceData`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos financieros.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> GetReferenceData(string type , string schema )              
```

**` SearchReferenceDatas`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos financieros filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos financiero (Ej: "MF","FUT", "OPC", puede incluirse una cadena de búsqueda parcial.</param>
    /// <param name="name">(Optional) Filtrar por nombre de Instrumentos (Ej: "ALUA", puede incluirse una cadena de búsqueda parcial).</param> 
    /// <param name="currency">(Optional) Filtrar por tipo de Moneda. (Ej: "ARS", puede incluirse una cadena de búsqueda parcial)</param>
    /// <param name="market">(Optional) Filtrar por Tipo de Mercado. (Ej "ROFX", "BYMA", puede incluirse una cadena de búsqueda parcial)</param>       
    /// <param name="country">(Optional) Filtrar por nombre de País (Ej: "ARG", puede incluirse una cadena de búsqueda parcial).</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo por defecto.</param>
    /// <returns>ReferenceDatas object Result.</returns>
    public async Task<ReferenceDatas> SearchReferenceData(string type, string name, string currency, string market, string country, string schema)
```

**` SearchReferenceDataById`**
```r
        /// <summary>
        /// Retorna los Instrumentos financieros que contengan una cadena de búsqueda como parte del identificador (puede incluirse cadenas de búsqueda parcial).
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos financieros a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo por defecto.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> SearchReferenceDataById(string id, string schema)
```

**` GetReferenceDataAsString`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos financieros como una cadena.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos financiero (Ej: "MF","FUT", "OPC", puede incluirse una cadena de búsqueda parcial.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo por defecto.</param>
    /// <returns>string</returns>
    public async Task<string> GetReferenceDataAsString(string type, string schema)
```

**` GetReferenceDataSpecification`**
```r
    /// <summary>
    /// Retorna una especificación del estado actual.
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Specification object result.</returns>
    public async Task<Specification> GetReferenceDataSpecification(string schema )       
```
