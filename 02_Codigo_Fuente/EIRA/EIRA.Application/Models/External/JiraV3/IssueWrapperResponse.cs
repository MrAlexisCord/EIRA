using EIRA.Application.DTOs;
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
        /* NUEVOS */
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("issuetype")]
        public NameableProp Issuetype { get; set; }
        [JsonProperty("customfield_10067")]
        public ValuableProp Frente { get; set; }
        [JsonProperty("customfield_10094")]
        public DateTime? FechaEntregaConstruccion { get; set; }
        // AIR-E
        [JsonProperty("customfield_10107")]
        public List<ValuableProp> Reporte { get; set; }
        [JsonProperty("customfield_10110")]
        public List<ValuableProp> ResponsablesMultiples { get; set; }

        [JsonProperty("customfield_10111")]
        public List<ValuableProp> ResponsablesMultiplesTripleaSUI { get; set; }

        [JsonProperty("customfield_10112")]
        public List<ValuableProp> ResponsablesMultiplesTripleaCartas { get; set; }

        [JsonProperty("customfield_10109")]
        public DateTime? FechaSolucion { get; set; }


        [JsonProperty("project")]
        public ProjectModel Project { get; set; }
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
        //[JsonProperty("customfield_10074")]
        //public DateTime? FechaEntregaConstruccion { get; set; }
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


        [JsonProperty("customfield_10093")]
        public DateTime? FechaEstimadaAnalisisN1 { get; set; }


        [JsonProperty("customfield_10095")]
        public DateTime? FechaEstimadaConstruccion { get; set; }

        [JsonProperty("customfield_10096")]
        public DateTime? FechaEstimadaPropuestaSolucion { get; set; }


        // Nuevos
        [JsonProperty("customfield_10085")]
        public TimeTo TimeToAttention { get; set; }

        [JsonProperty("customfield_10045")]
        public TimeTo TimeToResolution { get; set; }

        [JsonProperty("customfield_10070")]
        public TimeTo TiempoEntregaAnalisis { get; set; }


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

    public partial class TimeTo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("_links")]
        //public RequestTypeLinks Links { get; set; }

        [JsonProperty("completedCycles")]
        public List<Cycle> CompletedCycles { get; set; }

        [JsonProperty("ongoingCycle", NullValueHandling = NullValueHandling.Ignore)]
        public Cycle OngoingCycle { get; set; }

        [JsonProperty("slaDisplayFormat")]
        public string SlaDisplayFormat { get; set; }
    }

    public partial class Cycle
    {
        [JsonProperty("startTime")]
        public StatusDate StartTime { get; set; }

        [JsonProperty("breachTime")]
        public StatusDate BreachTime { get; set; }

        [JsonProperty("breached")]
        public bool Breached { get; set; }

        [JsonProperty("paused", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Paused { get; set; }

        [JsonProperty("withinCalendarHours", NullValueHandling = NullValueHandling.Ignore)]
        public bool? WithinCalendarHours { get; set; }

        //[JsonProperty("goalDuration")]
        //public ElapsedTime GoalDuration { get; set; }

        //[JsonProperty("elapsedTime")]
        //public ElapsedTime ElapsedTime { get; set; }

        //[JsonProperty("remainingTime")]
        //public ElapsedTime RemainingTime { get; set; }

        [JsonProperty("stopTime", NullValueHandling = NullValueHandling.Ignore)]
        public StatusDate StopTime { get; set; }
    }

    public partial class StatusDate
    {
        [JsonProperty("iso8601")]
        public string Iso8601 { get; set; }

        [JsonProperty("jira")]
        public string Jira { get; set; }

        [JsonProperty("friendly")]
        public string Friendly { get; set; }

        [JsonProperty("epochMillis")]
        public long EpochMillis { get; set; }
    }
}
