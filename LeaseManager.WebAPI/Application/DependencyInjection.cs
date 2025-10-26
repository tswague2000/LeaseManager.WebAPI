using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.FrameWork;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;

namespace LeaseManager.WebAPI.Application
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<ILeaseService, LeaseService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPropertyImageService, PropertyImageService>();
            services.AddScoped<IDocumentService, DocumentService>();

            return services;
        }
    }
}