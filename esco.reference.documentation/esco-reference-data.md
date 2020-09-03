# Esco Reference Data

[![N|Solid](esco.png)](https://www.sistemasesco.com.ar)

Conector que se integra con el Servicio [**PMYDS - Reference Data de Primary**](https://dataservices.primary.com.ar/product/#product=reference-data-read) que ofrece información de referencia de instrumentos financieros en forma consolidada.

#### DESCRIPCIÓN DE MÉTODOS

- [OData Reference Data](OData.md)
- [Reference Data](ReferenceData.md)
- [Instruments](Instruments.md)
- [Fondos](Fondos.md)
- [Schemas](Schemas)
- [Fields](Fields.md)
- [Types](Types.md)
- [Mappings](Mappings.md)
- [Source Fields](SourceFields.md)
- [Status Reports](StatusReports.md)
- [Derivatives](Derivatives.md)
- [Securities](Securities.md)

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

- [Descargar Instalador (.zip)](reference.data.zip)

