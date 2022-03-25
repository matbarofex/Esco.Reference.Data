# Reference Data ESCO Endpoints

Métodos:

**` getCustodians`**
```r
    /// <summary>    
    /// Retorna la lista de Sociedades Depositarias o Custodia de Fondos    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Depositary object Result.</returns>    
    public async Task<Custodians> getCustodians(string schema)    
```

**` getManagements`**
```r
    /// <summary>    
    /// Retorna la lista de Sociedades Administradoras de Fondos      
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Managers object Result.</returns>    
    public async Task<Managments> getManagements(string schema)    
```

**` getRentType`**
```r
    /// <summary>    
    /// Retorna la lista de Tipos de Rentas    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Rents object Result.</returns>    
	public async Task<Rents> getRentType(string schema)
``` 

**` getRegions`**
```r
    /// <summary>    
    /// Retorna la lista de Regiones    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Regions object Result.</returns>    
    public async Task<Regions> getRegions(string schema)    
``` 

**` getCurrencys`**
```r 
    /// <summary>    
    /// Retorna la lista de Monedas      
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Currencys object Result.</returns>    
    public async Task<Currencys> getCurrencys(string schema)    
``` 

**` getCountrys`**
```r
    /// <summary>    
    /// Retorna la lista de Países      
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Countrys object Result.</returns>    
    public async Task<Countrys> getCountrys(string schema)    
``` 

**` getIssuers`**
```r
    /// <summary>    
    /// Retorna la lista de Issuers     
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Issuers object Result.</returns>    
    public async Task<Issuers> getIssuers(string schema)    
``` 

**` getHorizons`**
```r
    /// <summary>    
    /// Retorna la lista de Horizons    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Horizons object Result.</returns>    
    public async Task<Horizons> getHorizons(string schema)    
``` 

**` getFundTypes`**
```r
    /// <summary>    
    /// Retorna la lista de Tipos de Fondos    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>FundTypes object Result.</returns>    
    public async Task<FundTypes> getFundTypes(string schema)    
``` 

**` getBenchmarks`**
```r
    
    /// <summary>    
    /// Retorna la lista de Benchmarks    
    /// </summary>    
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>    
    /// <returns>Benchmarks object Result.</returns>    
    public async Task<Benchmarks> getBenchmarks(string schema)    
``` 

**` getReferenceDataTypes`**
```r
	/// <summary>
    /// Retorna la lista de Tipos de Instrumentos financieros
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>ReferenceDataTypes object Result.</returns>
    public async Task<ReferenceDataTypes> getReferenceDataTypes(string schema)
``` 

**` getMarkets`**
```r 
	/// <summary>
    /// Retorna la lista de Mercados para los Instrumentos financieros
    /// </summary>
    /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
    /// <returns>Markets object Result.</returns>
    public async Task<Markets> getMarkets(string schema)
``` 
