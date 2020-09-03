# Instruments

Métodos:

**` getInstrumentsSuggestedFields`**
```r
    /// <summary>
    /// Obtiene una lista de campos sugeridos.
    /// </summary>        
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>SuggestedFields object result.</returns>
    public async Task<SuggestedFields> getInstrumentsSuggestedFields(string schema )        
```

**` getInstrumentsTodayUpdated`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos actualizados en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns> Instruments object result.</returns> 
    public async Task<Instruments> getInstrumentsTodayUpdated(string type , string source , string schema )       
 ```
 
 **` searchInstrumentsTodayUpdated`**
 ```r
    /// <summary>
    /// Retorna los instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos actualizados en el día a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns> object result.</returns>
    public async Task<Instruments> searchInstrumentsTodayUpdated(string id, string schema )    
```

**` getInstrumentsTodayAdded`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos dados de alta en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Instruments object result.</returns>
    public async Task<Instruments> getInstrumentsTodayAdded(string type , string source , string schema )        
```

**` searchInstrumentsTodayAdded`**
```r
    /// <summary>
    /// Retorna los instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos dados de alta en el día a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Instruments object result.</returns>
    public async Task<Instruments> searchInstrumentsTodayAdded(string id, string schema )
```

**` getInstrumentsTodayRemoved`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos dados de baja en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Instruments object result.</returns>
    public async Task<Instruments> getInstrumentsTodayRemoved(string type , string source , string schema )        
```

**` searchInstrumentsTodayRemoved`**
```r
    /// <summary>
    /// Retorna los instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos dados de baja en el día a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Instruments object result.</returns>
    public async Task<Instruments> searchInstrumentsTodayRemoved(string id, string schema )     
```

**` getInstrumentsReport`**
```r
    /// <summary>
    /// Retorna un reporte resumido de instrumentos.
    /// </summary>
    /// <param name="source">(Optional) Filtrar por tipo de mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>      
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>InstrumentsReport object result.</returns>
    public async Task<InstrumentsReport> getInstrumentsReport(string source , string schema )    
```  

**` searchInstrumentsReport`**
```r
    /// <summary>
    /// Retorna los instrumentos del reporte resumido contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos del reporte resumido a filtrar.</param>   
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>InstrumentsReport object result.</returns>
    public async Task<InstrumentsReport> searchInstrumentsReport(string id, string schema = null)
```

**` getInstrument`**
```r
    /// <summary>
    /// Retorna una instrumento por id.
    /// </summary>
    /// <param name="id">(Requeried) Id del Instrumento a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns> Instrument object result.</returns>
    public async Task<Instrument> getInstrument(string id, string schema )      
```

**` searchInstruments`**
```r
    /// <summary>
    /// Retorna los instrumentos que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns> Instruments object result.</returns> 
    public async Task<Instruments> searchInstruments(string id, string schema )       
```

**` getInstruments`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve todos los tipos de Instrumentos.</param>
    /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Instruments object result.</returns>
    public async Task<Instruments> getInstruments(string type , string source , string schema )        
```
