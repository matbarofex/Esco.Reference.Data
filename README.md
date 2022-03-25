# Esco Reference Data

[![N|Solid](esco.reference.documentation/esco.png)](https://www.sistemasesco.com.ar)

Conector que se integra con el Servicio [**Primary Information Reference**](https://i.primary.com.ar/) que ofrece Provisión automática de las características de los valores negociables de los distintos segmentos disponibles de las principales fuentes del mercado de capitales, como así también información de los Fondos Comunes de Inversión publicados en la CAFCI.

**` Development by .NET 6.0`**

#### DESCRIPCIÓN DE MÉTODOS

- [Reference Data](esco.reference.documentation/ReferenceData.md)
- [Reference Data by OData](esco.reference.documentation/OData.md)
- [Reference Data by Types](esco.reference.documentation/Types.md)
- [Reference Data ESCO Endpoints](esco.reference.documentation/Instruments.md)
- [Schemas](esco.reference.documentation/Schemas.md)
- [Fields](esco.reference.documentation/Fields.md)
- [Mappings](esco.reference.documentation/Mappings.md)

**` ReferenceDataServices`**
```r
/// <summary>
/// Inicialización del servicio API de Reference Datas.
/// </summary>
/// <param name="key"> (Requeried) Suscription key del usuario de Primary Information Reference. </param>        
/// <param name="host"> (Optional) Dirección url de la API de Primary Information Reference (Default: https://apids.primary.com.ar).</param>
 
public ReferenceDataServices (string id, string host)
```

## Ejemplo de uso

Esta solución contiene en [**esco.reference.data.example**](esco.reference.data.example/) el proyecto [**esco.reference.data.App**](esco.reference.data.example/esco.reference.data.App.csproj) como ejemplo de uso de las dll en una aplicación de prueba.
Se brinda además la posibilidad de descarga de esta aplicación de manera independiente:

#### PROGRAMA DE TEST

- [Descargar Instalador (.zip)](esco.reference.documentation/reference.data.zip)


## Acknowledgements

Development of this software was driven by
[Sistemas Esco](https://www.sistemasesco.com.ar/) as part of an Open Source
initiative of [Grupo Rofex](https://www.rofex.com.ar/).

#### Author/Maintainer

  - [René Hernández](https://github.com/matbarofex/Esco.Reference.Data)
