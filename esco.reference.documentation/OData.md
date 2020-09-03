# OData ReferenceDatas

Métodos:

**` getODataReferenceDatas`**
```
	/// <summary>
    /// Retorna la lista de instrumentos financieros filtrados con Query en formato OData.
    /// </summary>
    /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification("2"). (Ejemplo de consulta:"?$top=5&$filter=type eq 'MF'&$select=Currency,Symbol,UnderlyingSymbol" </param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDatas object Result.</returns>
    public async Task<ODataObject> getODataReferenceDatas(string query = null, string schema = null)
```

**` getODataReferenceDatasById`**
```
    /// <summary>
    /// Retorna la lista de instrumentos financieros filtrados por Id.
    /// </summary>
    /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos por Id.</param>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>OData object Result.</returns>
    public async Task<ODataReferenceDatas> getODataReferenceDatasById(string id, string schema = null)
```

**` searchODataReferenceDatas`**
```
    /// <summary>    
    /// Retorna la lista de instrumentos financieros filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).    
    /// </summary>    
    /// <param name="type">(Optional) Filtrar por tipo de Instrumentos financiero (Ej: "MF","FUT", "OPC", puede incluirse una cadena de búsqueda parcial.</param>    
    /// <param name="currency">(Optional) Filtrar por tipo de Moneda. (Ej: "ARS", puede incluirse una cadena de búsqueda parcial)</param>    
    /// <param name="symbol">(Optional) Filtrar por símbolo de Instrumentos (Ej: "AULA", puede incluirse una cadena de búsqueda parcial).</param>            
    /// <param name="market">(Optional) Filtrar por Tipo de Mercado. (Ej "ROFX", "BYMA", puede incluirse una cadena de búsqueda parcial)</param>           
    /// <param name="country">(Optional) Filtrar por nombre de País (Ej: "ARG", puede incluirse una cadena de búsqueda parcial).</param>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema "2".</param>    
    /// <returns>ReferenceDatas object Result.</returns>    
    public async Task<ODataReferenceDatas> searchODataReferenceDatas(
            string type = null, 
            string currency = null, 
            string symbol = null, 
            string market = null, 
            string country = null, 
            string schema = null)
			
```

**` getCustodians`**
```
    /// <summary>    
    /// Retorna la lista de Sociedades Depositarias o Custodia de Fondos    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Depositary object Result.</returns>    
    public async Task<Custodians> getCustodians(string schema = null)    
```

**` getManagements`**
``` 
    /// <summary>    
    /// Retorna la lista de Sociedades Administradoras de Fondos      
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Managers object Result.</returns>    
    public async Task<Managments> getManagements(string schema = null)    
```

**` getRentType`**
``` 
    /// <summary>    
    /// Retorna la lista de Tipos de Rentas    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Rents object Result.</returns>    
	public async Task<Rents> getRentType(string schema = null)
``` 

**` getRegions`**
```  
    /// <summary>    
    /// Retorna la lista de Regiones    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Regions object Result.</returns>    
    public async Task<Regions> getRegions(string schema = null)    
``` 

**` getCurrencys`**
```   
    /// <summary>    
    /// Retorna la lista de Monedas      
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Currencys object Result.</returns>    
    public async Task<Currencys> getCurrencys(string schema = null)    
``` 

**` getCountrys`**
```     
    /// <summary>    
    /// Retorna la lista de Países      
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Countrys object Result.</returns>    
    public async Task<Countrys> getCountrys(string schema = null)    
``` 

**` getIssuers`**
```     
    /// <summary>    
    /// Retorna la lista de Issuers     
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Issuers object Result.</returns>    
    public async Task<Issuers> getIssuers(string schema = null)    
``` 

**` getHorizons`**
```     
    /// <summary>    
    /// Retorna la lista de Horizons    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Horizons object Result.</returns>    
    public async Task<Horizons> getHorizons(string schema = null)    
``` 

**` getFundTypes`**
```     
    /// <summary>    
    /// Retorna la lista de Tipos de Fondos    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>FundTypes object Result.</returns>    
    public async Task<FundTypes> getFundTypes(string schema = null)    
``` 

**` getBenchmarks`**
```    
    
    /// <summary>    
    /// Retorna la lista de Benchmarks    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Benchmarks object Result.</returns>    
    public async Task<Benchmarks> getBenchmarks(string schema = null)    
``` 

**` getReferenceDataTypes`**
``` 
	/// <summary>
    /// Retorna la lista de Tipos de Instrumentos financieros
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDataTypes object Result.</returns>
    public async Task<ReferenceDataTypes> getReferenceDataTypes(string schema = null)
``` 

**` getReferenceDataSymbols`**
``` 
	/// <summary>
    /// Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDataTypes object Result.</returns>
    public async Task<ReferenceDataSymbols> getReferenceDataSymbols(string schema = null)
``` 

**` getMarkets`**
``` 
	/// <summary>
    /// Retorna la lista de Mercados para los Instrumentos financieros
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>Markets object Result.</returns>
    public async Task<Markets> getMarkets(string schema = null)
``` 
