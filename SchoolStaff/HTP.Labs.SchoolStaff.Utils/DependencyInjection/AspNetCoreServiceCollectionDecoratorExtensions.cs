using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace HTP.Labs.SchoolStaff.Utils.DependencyInjection
{
    public static class AspNetCoreServiceCollectionDecoratorExtensions
    {
        public static IServiceCollection AddDecorator<TService, TDecorator>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
    where TDecorator : TService
        {
            var serviceType = typeof(TService);
            var existingRegistrations = services.Where(sd => sd.ServiceType == serviceType).ToList();
            foreach (var existingRegistration in existingRegistrations)
            {
                Func<IServiceProvider, object> instanceGetter;
                var isRegistrationByType = false;
                if (existingRegistration.ImplementationInstance != null)
                {
                    instanceGetter = sp => existingRegistration.ImplementationInstance;
                }
                else if (existingRegistration.ImplementationFactory != null)
                {
                    instanceGetter = sp => existingRegistration.ImplementationFactory(sp);
                }
                // ReSharper disable once AssignmentInConditionalExpression - I really need an assignment here
                else if (isRegistrationByType = existingRegistration.ImplementationType != null)
                {
                    instanceGetter = sp => sp.GetRequiredService(existingRegistration.ImplementationType);
                }
                else
                {
                    throw new InvalidOperationException("Invalid service descriptor");
                }

                var constructor = typeof(TDecorator).GetConstructors(BindingFlags.Instance | BindingFlags.Public).Single();
                var newRegistration = new ServiceDescriptor(serviceType,
                    sp =>
                    {
                        var constructorParameters = constructor.GetParameters();
                        var parameterValues = new object[constructorParameters.Length];
                        for (var parameterNumber = 0; parameterNumber < constructorParameters.Length; parameterNumber++)
                        {
                            var parameter = constructorParameters[parameterNumber];
                            var parameterType = parameter.ParameterType;
                            if (parameterType != serviceType)
                            {
                                parameterValues[parameterNumber] = sp.GetRequiredService(parameterType);
                            }
                            else
                            {
                                parameterValues[parameterNumber] = instanceGetter(sp);
                            }
                        }
                        return constructor.Invoke(parameterValues);
                    }, lifetime);

                services.Remove(existingRegistration);
                services.Add(newRegistration);
                if (isRegistrationByType)
                {
                    services.Add(new ServiceDescriptor(existingRegistration.ImplementationType, existingRegistration.ImplementationType, existingRegistration.Lifetime));
                }

                /*TODO: answer these questions:                 
                1. Is simply calling sp.GetRequiredService enough for all cases? E.g., Lazy, IEnumerable etc. Is it handled already inside? Or do we need another method that hanles this?                 
                2. What about open generics?
                3. Are applied BindingFlags correct? Is this flexible enough?
                */
            }
            return services;
        }
    }
}
