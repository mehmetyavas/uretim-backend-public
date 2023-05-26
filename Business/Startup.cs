using Autofac;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Services.Authentication;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Abstract;
using DataAccess.Abstract.DevArchIRepos;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.Giris;
using DataAccess.Concrete.EntityFramework.Netsis;
using DataAccess.Concrete.EntityFramework.Prints;
using DataAccess.Concrete.EntityFramework.Production;
using DataAccess.Concrete.EntityFramework.Production.Enjeksiyon;
using DataAccess.Concrete.EntityFramework.Production.Montaj;
using DataAccess.Concrete.EntityFramework.WorkOrder;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete.Netsis;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ClaimsPrincipal GetPrincipal(IServiceProvider sp) =>
                sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(GetPrincipal);
            services.AddMemoryCache();

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });

            services.AddTransient<IAuthenticationCoordinator, AuthenticationCoordinator>();

            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();

            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<DisplayAttribute>()
                    ?.GetName();
            };
        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();

            //giriþ
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IHygieneRepository, HygieneRepository>();

            //üretim
            services.AddScoped<IAssemblyRepository, AssemblyRepository>();
            services.AddScoped<IEnjeksiyonRepository, EnjeksiyonRepository>();

            services.AddScoped<IPrintlogsRepository, EfPrintLogsRepository>();

            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IWorkOrderInfoRepository, WorkOrderInfoRepository>();
            services.AddScoped<IAssemblyMaterialRepository, AssemblyMaterialRepository>();

            //workOrder
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            services.AddScoped<IAssemblyRawMRepository, AssemblyRawMRepository>();
            services.AddScoped<IEflowRepository, EflowRepository>();

            services.AddScoped<IStokListeRepository, StokListeRepository>();
            //netsis
            services.AddScoped<IEsnekYapilandirmaRepository, EsnekYapilandirmaRepository>();
            services.AddScoped<IIsemriRecRepository, IsemriRecRepository>();
            services.AddScoped<ISeritraRepository, SeritraRepository>();
            services.AddScoped<IStSabitRepository, StSabitRepository>();
            services.AddScoped<IStharRepository, StharRepository>();
            services.AddScoped<IStokUrsRepository, StokUrsRepository>();

            //hammadde kontrol
            services.AddScoped<IEnjeksiyonHammaddeRepository, EnjeksiyonHammaddeRepository>();
            services.AddScoped<IEnjeksiyonBoyaRepository, EnjeksiyonBoyaRepository>();







            //services.AddTransient<ITranslateRepository, TranslateRepository>();
            //services.AddTransient<ILanguageRepository, LanguageRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            //services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            //services.AddTransient<IGroupRepository, GroupRepository>();
            //services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            //services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<EflowContext>(ServiceLifetime.Scoped);
            services.AddDbContext<ProjectDbContext, Peksan23Context>(ServiceLifetime.Scoped);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();

            //giriþ
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IHygieneRepository, HygieneRepository>();

            //üretim
            services.AddScoped<IAssemblyRepository, AssemblyRepository>();
            services.AddScoped<IEnjeksiyonRepository, EnjeksiyonRepository>();

            services.AddScoped<IPrintlogsRepository, EfPrintLogsRepository>();

            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IWorkOrderInfoRepository, WorkOrderInfoRepository>();

            services.AddScoped<IAssemblyMaterialRepository, AssemblyMaterialRepository>();


            //workOrder
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            services.AddScoped<IAssemblyRawMRepository, AssemblyRawMRepository>();
            services.AddScoped<IEflowRepository, EflowRepository>();

            services.AddScoped<IStokListeRepository, StokListeRepository>();

            //netsis
            services.AddScoped<IEsnekYapilandirmaRepository, EsnekYapilandirmaRepository>();
            services.AddScoped<IIsemriRecRepository, IsemriRecRepository>();
            services.AddScoped<ISeritraRepository, SeritraRepository>();
            services.AddScoped<IStSabitRepository, StSabitRepository>();
            services.AddScoped<IStharRepository, StharRepository>();
            services.AddScoped<IStokUrsRepository, StokUrsRepository>();

            //hammadde kontrol
            services.AddScoped<IEnjeksiyonHammaddeRepository, EnjeksiyonHammaddeRepository>();
            services.AddScoped<IEnjeksiyonBoyaRepository, EnjeksiyonBoyaRepository>();







            //services.AddTransient<ITranslateRepository, TranslateRepository>();
            //services.AddTransient<ILanguageRepository, LanguageRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            //services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            //services.AddTransient<IGroupRepository, GroupRepository>();
            //services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            //services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<EflowContext>(ServiceLifetime.Scoped);
            services.AddDbContext<ProjectDbContext, Peksan23Context>(ServiceLifetime.Scoped);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();

            //giriþ
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IHygieneRepository, HygieneRepository>();

            //üretim
            services.AddScoped<IAssemblyRepository, AssemblyRepository>();
            services.AddScoped<IEnjeksiyonRepository, EnjeksiyonRepository>();

            services.AddScoped<IPrintlogsRepository, EfPrintLogsRepository>();

            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IWorkOrderInfoRepository, WorkOrderInfoRepository>();

            services.AddScoped<IAssemblyMaterialRepository, AssemblyMaterialRepository>();

            //workOrder
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            services.AddScoped<IAssemblyRawMRepository, AssemblyRawMRepository>();
            services.AddScoped<IEflowRepository, EflowRepository>();

            services.AddScoped<IStokListeRepository, StokListeRepository>();

            //netsis
            services.AddScoped<IEsnekYapilandirmaRepository, EsnekYapilandirmaRepository>();
            services.AddScoped<IIsemriRecRepository, IsemriRecRepository>();
            services.AddScoped<ISeritraRepository, SeritraRepository>();
            services.AddScoped<IStSabitRepository, StSabitRepository>();
            services.AddScoped<IStharRepository, StharRepository>();
            services.AddScoped<IStokUrsRepository, StokUrsRepository>();

            //hammadde kontrol
            services.AddScoped<IEnjeksiyonHammaddeRepository, EnjeksiyonHammaddeRepository>();
            services.AddScoped<IEnjeksiyonBoyaRepository, EnjeksiyonBoyaRepository>();







            //services.AddTransient<ITranslateRepository, TranslateRepository>();
            //services.AddTransient<ILanguageRepository, LanguageRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            //services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            //services.AddTransient<IGroupRepository, GroupRepository>();
            //services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            //services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<EflowContext>(ServiceLifetime.Scoped);
            services.AddDbContext<ProjectDbContext, Peksan23Context>(ServiceLifetime.Scoped);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}