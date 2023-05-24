using EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses;
using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public partial class IssueWrapperResponse
    {
        [JsonProperty("expand")]
        public string Expand { get; set; }

        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        [JsonProperty("maxResults")]
        public long MaxResults { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("issues")]
        public List<Issue> Issues { get; set; }
    }

    public partial class Issue
    {
        [JsonProperty("expand")]
        public string Expand { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }

    public partial class Fields
    {
        //[JsonProperty("project")]
        //public ProjectModel Project { get; set; }
        [JsonProperty("customfield_10084")]
        public string NumeroCaso { get; set; }
        [JsonProperty("customfield_10065")]
        public ValuableProp ResponsableCliente { get; set; } //N1
        [JsonProperty("customfield_10087")]
        public ValuableProp Complejidad { get; set; }
        [JsonProperty("customfield_10089")]
        public decimal? Prioridad { get; set; }
        //public string Tarea { get; set; } = "Obtener Tarea";
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("customfield_10103")]
        public Customfield1010 HistoriaUsuario { get; set; } // DescripcionCorta
        [JsonProperty("customfield_10063")]
        public ValuableProp Compania { get; set; } // Empresa
        [JsonProperty("assignee")]
        public DisplayableNameProp Desarrollador { get; set; }
        //public DateTime? FechaEstimada { get; set; } // Cuál fecha estimada es?
        public string Observaciones { get; set; } // Comentarios
        [JsonProperty("customfield_10092")]
        public DateTime? FechaEntregaAnalisisN1 { get; set; }
        [JsonProperty("customfield_10097")]
        public DateTime? FechaEntregaPropuestaSolucion { get; set; }
        [JsonProperty("customfield_10074")]
        public DateTime? FechaEntregaConstruccion { get; set; }
        [JsonProperty("customfield_10101")]
        public DateTime? FechaCierre { get; set; }



        //FULL REPORT
        [JsonProperty("customfield_10105")]
        public Customfield1010 DescripcionEstadoCliente { get; set; }
        [JsonProperty("customfield_10102")] //Antigua Fecha Aranda customfield_10081
        public DateTime? FechaApertura { get; set; }
        [JsonProperty("customfield_10066")]
        public DateTime? FechaAsignacion { get; set; }

        // TIEMPOS
        [JsonProperty("customfield_10098")]
        public decimal? TiempoEstimadoPropuestaSolucion { get; set; }
        [JsonProperty("customfield_10099")]
        public decimal? TiempoEstimadoConstruccion { get; set; }
        [JsonProperty("customfield_10100")]
        public decimal? TiempoEstimadoSoportePruebas { get; set; }


        //[JsonProperty("customfield_10093")]
        //public DateTime? FechaEstimadaAnalisisN1 { get; set; }


        [JsonProperty("customfield_10095")]
        public DateTime? FechaEstimadaConstruccion { get; set; }

        [JsonProperty("customfield_10096")]
        public DateTime? FechaEstimadaPropuestaSolucion { get; set; }


        public class ProjectModel
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("key")]
            public string Key { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
