﻿using EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses;
using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class IssueUpdateRequest
    {
        //[JsonProperty("project")]
        //public IdentifiableProp Project { get; set; }

        //[JsonProperty("summary")]
        //public string Summary { get; set; }

        ////[JsonProperty("description")]
        ////public Description Description { get; set; }

        //[JsonProperty("issuetype")]
        //public IdentifiableProp Issuetype { get; set; }

        //[JsonProperty("assignee")]
        //public IdentifiableProp Assignee { get; set; }

        //[JsonProperty("priority")]
        //public NameableProp Priority { get; set; }

        [JsonProperty("customfield_10063")]
        public ValuableProp Compania { get; set; }

        //[JsonProperty("customfield_10065")]
        //public ValuableProp ResponsableCliente { get; set; }

        //[JsonProperty("customfield_10066")]
        //public DateTime FechaAsignacionAranda { get; set; }

        //[JsonProperty("customfield_10067")]
        //public ValuableProp Frente { get; set; }

        [JsonProperty("customfield_10084")]
        public string NumeroAranda { get; set; }

        [JsonProperty("customfield_10064")]
        public IdentifiableProp Gravedad { get; set; }

        [JsonProperty("customfield_10080")]
        public string SistemaCargue { get; set; }

        [JsonProperty("customfield_10081")]
        public DateTime FechaRegistroAranda { get; set; }

        [JsonProperty("customfield_10082")]
        public string EstadoAranda { get; set; }

        //[JsonProperty("customfield_10083")]
        //public string Grupo { get; set; }
    }
}