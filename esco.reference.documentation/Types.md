# Reference Data by Types

Métodos:

**` GetFondos`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Fondos Comunes de Inversion (MF).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetFondos(string schema)
```

**` GetCedears`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Cedears (CD).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetCedears(string schema)
```

   **` GetAcciones`**
```
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Acciones (CS).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetAcciones(string schema)
```

   **` GetAccionesADRS`**
```
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Acciones A.D.R.S.
   /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
   /// </summary>     
   /// <returns>Modelo de datos json de tipo Acciones.</returns>
   public async Task<Acciones> GetAccionesADRS(string schema)
```

   **` GetAccionesPrivadas`**
```
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Acciones Privadas
   /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
   /// </summary>     
   /// <returns>Modelo de datos json de tipo Acciones.</returns>
   public async Task<Acciones> GetAccionesPrivadas(string schema)
```

   **` GetAccionesPYMES`**
```
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Acciones PYMES
   /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
   /// </summary>     
   /// <returns>Modelo de datos json de tipo Acciones.</returns>
   public async Task<Acciones> GetAccionesPYMES(string schema)
```

**` GetObligaciones`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Obligaciones (CORP).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetObligaciones(string schema)
```

**` GetTitulos`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Títulos Públicos (GO).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetTitulos(string schema) 
```

**` GetFuturos`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Futuros (FUT).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetFuturos(string schema)
```

**` GetOpciones`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Opciones (OPT-OOF).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetOpciones(string schema) 
```

**` GetPases`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Pases (BUYSELL).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetPases(string schema)
```

**` GetCauciones`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Cauciones (REPO).
   /// </summary>     
   /// <returns>ReferenceDatas json.</returns>
   public async Task<ReferenceDatas> GetCauciones(string schema)
```

**` GetPlazos`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Plazos por Lotes.
   /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
   /// </summary>     
   /// <returns>Modelo de datos json de tipo Plazos.</returns>
   public async Task<Plazos> GetPlazos(string schema)
```

**` GetPrestamosValores`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Préstamos de Valores.
   /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
   /// </summary>     
   /// <returns>Modelo de datos json de tipo Plazos.</returns>
   public async Task<Prestamos> GetPrestamosValores(string schema)
```

**` GetIndices`**
```r
   /// <summary>
   /// Retorna la lista de instrumentos financieros de tipo Indice (XLINKD).
   /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
   /// </summary>     
   /// <returns>Modelo de datos json de tipo Indices.</returns>
   public async Task<Indices> GetIndices(string schema)
```
