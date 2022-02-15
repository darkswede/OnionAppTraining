//using OnionAppTraining.Infrastructure.Commands;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OnionAppTraining.Infrastructure.Extensions
//{
//    public static class CommandDispatcherExtensions
//    {
//        public static void AddCommandQueryHandlers(this CommandDispatcher dispatcher, Type handlerInterface)
//        {
//            var handlers = typeof(ServiceExtensions).Assembly.GetTypes()
//                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
//            );

//            foreach (var handler in handlers)
//            {
//                dispatcher._serviceProvider.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
//            }
//        }
//    }
//}
