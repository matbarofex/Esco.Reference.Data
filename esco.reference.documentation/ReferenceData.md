# Reference Data

Métodos:

**` getReferenceDataTodayUpdated`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos actualizados en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa. </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo </param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> getReferenceDataTodayUpdated(string type , string schema )
```

**` searchReferenceDataTodayUpdated`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos actualizados en el día a filtrar. </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo. </param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> searchReferenceDataTodayUpdated(string id, string schema )  
```     

**` getReferenceDataTodayAdded`**
``` 
    /// <summary>
    /// Retorna la lista de instrumentos dados de alta en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> getReferenceDataTodayAdded(string type , string schema )     
```   

**` searchReferenceDataTodayAdded`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos dados de alta en el día a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> searchReferenceDataTodayAdded(string id, string schema )
```

**` getReferenceDataTodayRemoved`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos dados de baja en el día.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task <ReferenceDatas> getReferenceDataTodayRemoved(string type , string schema )     
```

**` searchReferenceDataTodayRemoved`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos dados de baja en el día a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> searchReferenceDataTodayRemoved(string id, string schema )      
```

**` getReferenceDatas`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos financieros.
    /// </summary>
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> getReferenceDatas(string type , string schema )              
```

**` searchReferenceDatas`**
```r
    /// <summary>
    /// Retorna los Instrumentos financieros que contengan una cadena de búsqueda como parte del id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos financieros a filtrar.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>        
    /// <returns>ReferenceDatas object result.</returns>
    public async Task<ReferenceDatas> searchReferenceDatas(string id, string schema )     	
```

**` getReferenceDataSpecification`**
```r
    /// <summary>
    /// Retorna una especificación del estado actual.
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo.</param>
    /// <returns>Specification object result.</returns>
    public async Task<Specification> getReferenceDataSpecification(string schema )       
```
