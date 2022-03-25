# OData ReferenceDatas

Métodos:

**` GetReferenceDataByOData`**
```r
	/// <summary>
    /// Retorna la lista de instrumentos financieros filtrados con Query en formato OData.
    /// </summary>
    /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification("2"). (Ejemplo de consulta:"?$top=5&$filter=type eq 'MF'&$select=Currency,Symbol,UnderlyingSymbol" </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDatas object Result.</returns>
    public async Task<ODataObject> GetReferenceDataByOData(string query, string schema)
```

**` GetConsolidatedByOData`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos financieros consolidados filtrados con Query en formato OData.
    /// </summary>
    /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification(). (Ejemplo de consulta:"?$top=5 & $filter=type eq 'MF' & $select=currency,name,region" </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
	/// <returns>OData object Result.</returns>
    public async Task<ODataReferenceDatas> GetConsolidatedByOData(string id, string schema)
```

**` GetCSVByOData`**
```r
    /// <summary>
    /// Retorna la lista de instrumentos financieros filtrados en un CSV con Query en formato OData.
    /// </summary>
    /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification(). (Ejemplo de consulta:"?$top=5 & $filter=type eq 'MF' & $select=currency,name,region" </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDatas object Result.</returns>
    public async Task<Stream> GetCSVByOData(string query, string schema)
```

**` SaveCSVByOData`**
```r
	/// <summary>
    /// Retorna la lista de instrumentos financieros en un CSV (compactado en archivo ZIP) filtrados con Query en formato OData.
    /// </summary>
    /// <param name="filePath">(Required) Ruta del directorio donde se guarda el archivo exportado a formato .csv </param>
    /// <param name="fileName">(Required) Nombre del archivo donde se guarda la exportación en formato .csv </param>
    /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification(). (Ejemplo de consulta:"?$top=5 & $filter=type eq 'MF' & $select=currency,name,region" </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDatas object Result.</returns>
    public async Task<bool> SaveCSVByOData(string filePath, string fileName, string query, string schema)
```


