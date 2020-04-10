using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Books.Common.Events
{
	public static class EventsRegistrator
	{
		private static readonly Type EventHandlerType = typeof(BaseEventSubscriber);

		public static void ConfigureServices(IServiceCollection services, Assembly assembly)
		{
			assembly
				.GetTypes()
				.Where(t => t.IsSubclassOf(EventHandlerType))
				.ToList()
				.ForEach(eh =>
				{
					var baseType = eh.BaseType;
					if (baseType.IsGenericType)
					{
						//var g = baseType.GetGenericTypeDefinition();
						Console.WriteLine($"ConfigureServices. T: {eh.FullName}, BaseType: {baseType.FullName}");
						//services.AddTransient(GenericEventSubscriber<g>, eh);
						services.AddTransient(baseType, eh);
					}					
				});
		}

		/*public static void Configure(IServiceProvider services, Assembly assembly)
		{
			assembly
				.GetTypes()
				.Where(t => t.IsSubclassOf(EventHandlerType))
				.ToList()
				.ForEach(eh => services.GetService(eh));
		}*/
	}
}
