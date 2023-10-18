# Esco Reference Data

[![N|Solid](esco.png)](https://www.sistemasesco.com.ar)

Conector que se integra con el Servicio [**Primary Information Reference**](https://i.primary.com.ar/) que ofrece Provisión automática de las características de los valores negociables de los distintos segmentos disponibles de las principales fuentes del mercado de capitales, como así también información de los Fondos Comunes de Inversión publicados en la CAFCI.

#### DESCRIPCIÓN DE MÉTODOS

- [Reference Data](ReferenceData.md)
- [Reference Data by OData](OData.md)
- [Reference Data by Types](Types.md)
- [Reference Data ESCO Endpoints](Instruments.md)
- [Mappings Schemas](Schemas.md)
- [Fields](Fields.md)

**` ReferenceDataServices`**
```r
/// <summary>
/// Inicialización del servicio API de Reference Datas.
/// </summary>
/// <param name="key"> (Requeried) Suscription key del usuario de Primary Information Reference. </param>        
/// <param name="host"> (Optional) Dirección url de la API de Primary Information Reference (Default: https://apids.primary.com.ar).</param>
/// <param name="paginated">(Optional) Habilitación del paginado de registros (hasta 500 por página) de cada endpoints (por defecto: false) </param>
 
public ReferenceDataServices (string id, string host, bool paginated)
```

## Ejemplo de uso

Esta solución contiene el proyecto [**esco.reference.data.App**](http://devops.sisesco.com/Tecnolog%C3%ADa/Proyectos/_git/esco.reference.data?path=%2Fesco.reference.data.app) como ejemplo de uso de las dll en una aplicación de prueba.
Se brinda además la posibilidad de descarga de esta aplicación de manera independiente:

#### PROGRAMA DE TEST

- [Descargar Instalador (.zip)](reference.data.zip)


## Acknowledgements

Development of this software was driven by
[Sistemas Esco](https://www.sistemasesco.com.ar/) as part of an Open Source
initiative of [Grupo Rofex](https://www.rofex.com.ar/).

#### Author/Maintainer

  - [René Hernández](https://github.com/matbarofex/Esco.Reference.Data)
