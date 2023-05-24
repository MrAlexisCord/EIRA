using EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses;
using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class IssueCreateAIRERequestBody : BaseIssueRequestModel
    {
        [JsonProperty("project")]
        public KeyableProp Project { get; set; }
        //public IdentifiableProp Project { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("issuetype")]
        public IdentifiableProp Issuetype { get; set; }

        [JsonProperty("assignee")]
        public IdentifiableProp Assignee { get; set; }

        //[JsonProperty("priority")]
        //public NameableProp Priority { get; set; }

        //[JsonProperty("customfield_10063")]
        //public ValuableProp Compania { get; set; }

        //[JsonProperty("customfield_10064")]
        //public IdentifiableProp Gravedad { get; set; }

        //[JsonProperty("customfield_10065")]
        //public ValuableProp ResponsableCliente { get; set; }

        [JsonProperty("customfield_10066")]
        public DateTime FechaAsignacionAranda { get; set; }

        //[JsonProperty("customfield_10067")]
        //public ValuableProp Frente { get; set; }



        //[JsonProperty("customfield_10080")]
        //public string SistemaCargue { get; set; }

        //[JsonProperty("customfield_10082")]
        //public string EstadoAranda { get; set; }

        //[JsonProperty("customfield_10083")]
        //public string Grupo { get; set; }

        [JsonProperty("customfield_10084")]
        public string NumeroAranda { get; set; }
        //[JsonProperty("customfield_10087")]
        //public ValuableProp Complejidad { get; set; }

        [JsonProperty("customfield_10089")]
        public decimal? Prioridad { get; set; }



        // FECHAS
        //[JsonProperty("customfield_10092")]
        //public DateTime? FechaEntregaAnalisisN1 { get; set; }

        //[JsonProperty("customfield_10093")]
        //public DateTime? FechaEstimadaAnalisisN1 { get; set; }

        [JsonProperty("customfield_10094")]
        public DateTime? FechaEntregaConstruccion { get; set; }

        //[JsonProperty("customfield_10095")]
        //public DateTime? FechaEstimadaConstruccion { get; set; }

        //[JsonProperty("customfield_10096")]
        //public DateTime? FechaEstimadaPropuestaSolucion { get; set; }

        [JsonProperty("customfield_10097")]
        public DateTime? FechaEntregaPropuestaSolucion { get; set; }



        // TIEMPOS
        //[JsonProperty("customfield_10098")]
        //public decimal? TiempoEstimadoPropuestaSolucion { get; set; }
        //[JsonProperty("customfield_10099")]
        //public decimal? TiempoEstimadoConstruccion { get; set; }
        //[JsonProperty("customfield_10100")]
        //public decimal? TiempoEstimadoSoportePruebas { get; set; }


        //[JsonProperty("customfield_10105")]
        //public Customfield1010 DescripcionEstadoCliente { get; set; }

        //[JsonProperty("customfield_10102")] //Antigua Fecha Aranda customfield_10081
        //public DateTime? FechaApertura { get; set; }

        [JsonProperty("customfield_10103")]
        public Customfield1010 HistoriaUsuario { get; set; }


        // AIR-E
        [JsonProperty("customfield_10107")]
        public List<ValuableProp> Reporte { get; set; }
        [JsonProperty("customfield_10110")]
        public List<ValuableProp> ResponsablesMultiples { get; set; }
        [JsonProperty("customfield_10109")]
        public DateTime? FechaSolucion { get; set; }
        [JsonProperty("customfield_10101")]
        public DateTime? FechaCierre { get; set; }
    }
}
