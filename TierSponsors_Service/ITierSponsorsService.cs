﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TierSponsors_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITierSponsorsService" in both code and config file together.
    [ServiceContract]
    public interface ITierSponsorsService
    {

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetOrganisations(string name, string city, string county, string tier, string subtier);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetOrganisationsByName(string name);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetOrganisationsByCity(string city);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetOrganisationsByTier(string tier);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetOrganisationsBySubTier(string subtier);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    
    [DataContract]
    public class OrganisationsCollection
    {
        [DataMember]
        public int TotalOrgs { get { return OrgList.Count; } }
        [DataMember]
        public List<Organisation> OrgList { get; set; }
    }

    [DataContract]
    public class Organisation
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string County { get; set; }
        [DataMember]
        public List<Tier> TierList { get; set; }
    }

    [DataContract]
    public class Tier
    {
        [DataMember]
        public string TierType { get; set; }
        [DataMember]
        public string Rating { get; set; }
        [DataMember]
        public string SubTier { get; set; }
    }
}
