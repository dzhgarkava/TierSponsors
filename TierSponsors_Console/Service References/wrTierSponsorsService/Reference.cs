﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TierSponsors_Console.wrTierSponsorsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wrTierSponsorsService.ITierSponsorsService")]
    public interface ITierSponsorsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITierSponsorsService/GetOrganisations", ReplyAction="http://tempuri.org/ITierSponsorsService/GetOrganisationsResponse")]
        string GetOrganisations(string name, string city, string county, string tier, string subtier);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITierSponsorsService/GetOrganisationsByName", ReplyAction="http://tempuri.org/ITierSponsorsService/GetOrganisationsByNameResponse")]
        string GetOrganisationsByName(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITierSponsorsService/GetOrganisationsByCity", ReplyAction="http://tempuri.org/ITierSponsorsService/GetOrganisationsByCityResponse")]
        string GetOrganisationsByCity(string city);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITierSponsorsService/GetOrganisationsByTier", ReplyAction="http://tempuri.org/ITierSponsorsService/GetOrganisationsByTierResponse")]
        string GetOrganisationsByTier(string tier);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITierSponsorsService/GetOrganisationsBySubTier", ReplyAction="http://tempuri.org/ITierSponsorsService/GetOrganisationsBySubTierResponse")]
        string GetOrganisationsBySubTier(string subtier);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITierSponsorsServiceChannel : TierSponsors_Console.wrTierSponsorsService.ITierSponsorsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TierSponsorsServiceClient : System.ServiceModel.ClientBase<TierSponsors_Console.wrTierSponsorsService.ITierSponsorsService>, TierSponsors_Console.wrTierSponsorsService.ITierSponsorsService {
        
        public TierSponsorsServiceClient() {
        }
        
        public TierSponsorsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TierSponsorsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TierSponsorsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TierSponsorsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetOrganisations(string name, string city, string county, string tier, string subtier) {
            return base.Channel.GetOrganisations(name, city, county, tier, subtier);
        }
        
        public string GetOrganisationsByName(string name) {
            return base.Channel.GetOrganisationsByName(name);
        }
        
        public string GetOrganisationsByCity(string city) {
            return base.Channel.GetOrganisationsByCity(city);
        }
        
        public string GetOrganisationsByTier(string tier) {
            return base.Channel.GetOrganisationsByTier(tier);
        }
        
        public string GetOrganisationsBySubTier(string subtier) {
            return base.Channel.GetOrganisationsBySubTier(subtier);
        }
    }
}