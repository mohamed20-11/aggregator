using Aggregator.GrpClientsConfig;
using ControlPanel.ORGHierararchy.Protos;
using ControlPanel.UserManagement.Protos;
using DynamicWorkflow.Aggregator.Protos;
using TicketingSystem.SLA.Protos;


namespace _4EPlatform_DynamicWorkflow_Aggregator.GrpClientsConfig
{

    public static class GrpcClients
    {
        #region controlPanelAggregator
        public static void AddControlPanelAggGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<UserOrgProtoService.UserOrgProtoServiceClient>(url);
            services.AddGrpcServiceClient<UserProtoService.UserProtoServiceClient>(url);
            services.AddGrpcServiceClient<RoleWorkspaceProtoService.RoleWorkspaceProtoServiceClient>(url);

        }
        #endregion

        #region SLA
        public static void AddSlaGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<EscalationLevelProtoService.EscalationLevelProtoServiceClient>(url);
            services.AddGrpcServiceClient<SlaProtoServices.SlaProtoServicesClient>(url);
            //services.AddGrpcServiceClientWithClientCredentials<SLAControllerAndActionNameProto.SLAControllerAndActionNameProtoClient>(url);

        }
        #endregion
        #region DynmaicWorkFlow
        public static void AddDynamicWorkFlowGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<RequestTemplateProtoService.RequestTemplateProtoServiceClient>(url);
            services.AddGrpcServiceClient<RequestInstanceManager.RequestInstanceManagerClient>(url);
        }
        #endregion
        public static void AddOrgGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<OrgProtoService.OrgProtoServiceClient>(url);

        }

    }
}
