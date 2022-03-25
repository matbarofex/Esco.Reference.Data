using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Type
    {
        public int id { get; set; }
        public string type { get; set; }
        public string description { get; set; }
    }

    public static class Types
    {
        public const string Fondos = "MF";         //MF es estándar fix y se refiere a FONDOS COMUNES DE INVERSIÓN
        public const string Cedears = "CD";        //CD es estándar fix y se refiere a CEDEARS
        public const string Acciones = "CS";       //Líderes CS //CS es estándar fix y se refiere a ACCIONES
                                                   //Acciones Pymes CS CS es estándar fix y se refiere a ACCIONES PYMES
                                                   //A.D.R.S(Acciones) //CS Incluido en Type CS
        public const string Obligaciones = "CORP"; //CORP es estándar fix y se refiere a Obligaciones Negociables
        public const string Titulos = "GO";        //Títulos Públicos > Bonos GO GO es estándar fix y se refiere a BONOS EXTERNOS, TITULOS PUBLICOS, BONOS CONSOLIDADOS
                                                   //Títulos Públicos > Letras GO //Incluido en Type GO y se refiere a LETRAS, LETRAS TESORO NACIONAL
                                                   //Especies de Fideicomisos GO // Incluido en Type GO y se refiere a TITULOS DE DEUDA, CERTIF. PARTICIP.
        public const string Futuros = "FUT";       //FUT es estándar fix y se refiere a FUTUROS
        public const string Opciones = "OPT";      //OPT y OOF son estándar fix y se refiere a OPCIONES
        public const string OpcionesF = "OOF";     //OPT y OOF son estándar fix y se refiere a OPCIONES
        public const string Pases = "BUYSELL";     //BUYSELL es estándar fix y se refiere a PASES
        public const string Cauciones = "REPO";    //REPO es estándar fix y se refiere a CAUCIONES
        public const string STN = "STN";           //Acciones Privadas
        public const string T = "T";               //Plazo por Lotes
        public const string TERM = "TERM";         //Préstamos de Valores
        public const string Indices = "XLINKD";    //Índices XLINKD
    }

    public static class TypesDesc
    {
        public const string Fondos = "MF es estándar fix y se refiere a FONDOS COMUNES DE INVERSIÓN";
        public const string Cedears = "CD es estándar fix y se refiere a CEDEARS";
        public const string Acciones = "Líderes CS //CS es estándar fix y se refiere a ACCIONES / Acciones Pymes CS CS es estándar fix y se refiere a ACCIONES PYMES / A.D.R.S(Acciones) //CS Incluido en Type CS";
        public const string Obligaciones = "CORP es estándar fix y se refiere a Obligaciones Negociables";
        public const string Titulos = "Títulos Públicos > Bonos GO GO es estándar fix y se refiere a BONOS EXTERNOS, TITULOS PUBLICOS, BONOS CONSOLIDADOS / Títulos Públicos > Letras GO / Incluido en Type GO y se refiere a LETRAS, LETRAS TESORO NACIONAL / Especies de Fideicomisos GO / Incluido en Type GO y se refiere a TITULOS DE DEUDA, CERTIF. PARTICIP.";
        public const string Futuros = "FUT es estándar fix y se refiere a FUTUROS";
        public const string Opciones = "OPT y OOF son estándar fix y se refiere a OPCIONES";
        public const string OpcionesF = "OPT y OOF son estándar fix y se refiere a OPCIONES";
        public const string Pases = "BUYSELL es estándar fix y se refiere a PASES";
        public const string Cauciones = "REPO es estándar fix y se refiere a CAUCIONES";
        public const string STN = "Acciones Privadas";
        public const string T = "Plazo por Lotes";
        public const string TERM = "Préstamos de Valores";
        public const string Indices = "Índices XLINKD";
    }

    public class ReferenceDataTypes : List<Type> { }

    public static class TypesList
    {
        public static ReferenceDataTypes List
        {
            get => new()
                    {
                        { new Type() { id = 0, type = Types.Fondos, description = TypesDesc.Fondos } },
                        { new Type() { id = 1, type = Types.Cedears, description = TypesDesc.Cedears } },
                        { new Type() { id = 2, type = Types.Acciones, description = TypesDesc.Acciones } },
                        { new Type() { id = 3, type = Types.Obligaciones, description = TypesDesc.Obligaciones } },
                        { new Type() { id = 4, type = Types.Titulos, description = TypesDesc.Titulos } },
                        { new Type() { id = 5, type = Types.Futuros, description = TypesDesc.Futuros } },
                        { new Type() { id = 6, type = Types.Opciones, description = TypesDesc.Opciones } },
                        { new Type() { id = 7, type = Types.OpcionesF, description = TypesDesc.OpcionesF } },
                        { new Type() { id = 8, type = Types.Pases, description = TypesDesc.Pases } },
                        { new Type() { id = 9, type = Types.Cauciones, description = TypesDesc.Cauciones } },
                        { new Type() { id = 10, type = Types.STN, description = TypesDesc.STN } },
                        { new Type() { id = 11, type = Types.T, description = TypesDesc.T } },
                        { new Type() { id = 12, type = Types.TERM, description = TypesDesc.TERM } },
                        { new Type() { id = 13, type = Types.Indices, description = TypesDesc.Indices } },
                };
        }
    }
}
