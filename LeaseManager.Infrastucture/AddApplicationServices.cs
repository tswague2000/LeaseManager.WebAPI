using LeaseManager.Core.FrameWork;
using LeaseManager.Core.FrameWork.Interface;
using LeaseManager.Infrastucture.Interfaces;
using LeaseManager.WebAPI.Application.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LeaseManager.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ILeaseRepository, LeaseRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();

            // Nouveaux services métiers
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<ITenantService, TenantService>();

            return services;
        }
    }

}
