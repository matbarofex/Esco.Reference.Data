# Esco Reference Data

[![N|Solid](esco.reference.documentation/esco.png)](https://www.sistemasesco.com.ar)

Conector que se integra con el Servicio [**PMYDS - Reference Data de Primary**](https://dataservices.primary.com.ar/product/#product=reference-data-read) que ofrece información de referencia de instrumentos financieros en forma consolidada.

#### DESCRIPCIÓN DE MÉTODOS

- [OData Reference Data](esco.reference.documentation/OData.md)
- [Reference Data](esco.reference.documentation/ReferenceData.md)
- [Instruments](esco.reference.documentation/Instruments.md)
- [Fondos](esco.reference.documentation/Fondos.md)
- [Schemas](esco.reference.documentation/Schemas.md)
- [Fields](esco.reference.documentation/Fields.md)
- [Types](esco.reference.documentation/Types.md)
- [Mappings](esco.reference.documentation/Mappings.md)
- [Source Fields](esco.reference.documentation/SourceFields.md)
- [Status Reports](esco.reference.documentation/StatusReports.md)
- [Derivatives](esco.reference.documentation/Derivatives.md)
- [Securities](esco.reference.documentation/Securities.md)

**` ReferenceDataServices`**
```r
/// <summary>
/// Inicialización del servicio API de Reference Datas.
/// </summary>
/// <param name="key"> (Requeried) Suscription key del usuario. </param>        
/// <param name="host"> (Optional) Dirección url de la API .</param>
 
public ReferenceDataServices (string id, string host)
```

#### PROGRAMA DE TEST

- [Descargar Instalador (.zip)](esco.reference.documentation/reference.data.zip)


## Acknowledgements

Development of this software was driven by
[Sistemas Esco](https://www.sistemasesco.com.ar/) as part of an Open Source
initiative of [Grupo Rofex](https://www.rofex.com.ar/).

#### Author/Maintainer

  - [René Hernández](https://github.com/Renyhc)
