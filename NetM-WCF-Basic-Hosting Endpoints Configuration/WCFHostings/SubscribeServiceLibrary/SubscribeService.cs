using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NorthwindModel;

namespace SubscribeServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class SubscribeService : ISubscribeService
    {
        static List<IOrderStatusChangedCallback> mCallbacks = new List<IOrderStatusChangedCallback>();

        /// <summary>
        /// После вызова этого метода клиент начнет получать уведомления об изменениях статуса заказа в виде Callback-вызовов
        /// </summary>
        public void Subscribe(OrderWithStatus ows)
        {
            IOrderStatusChangedCallback callback = OperationContext.Current.GetCallbackChannel<IOrderStatusChangedCallback>();

            if (!mCallbacks.Contains(callback))
            {
                mCallbacks.Add(callback);
                Console.WriteLine("Подписались на уведомления.");
            }
        }

        public void NotifyEveryone(OrderWithStatus ows)
        {
            mCallbacks.ForEach(t => t.StatusWasChanged(ows));
            Console.WriteLine("Пытаемся уведомить кого-нибудь");
        }

        /// <summary>
        /// После этого уведомления перестают приходить.
        /// </summary>
        public void Unsubscribe(OrderWithStatus ows)
        {
            IOrderStatusChangedCallback callback = OperationContext.Current.GetCallbackChannel<IOrderStatusChangedCallback>();

            if (mCallbacks.Contains(callback))
            {
                mCallbacks.Remove(callback);
                Console.WriteLine("Отписались от уведомлений.");
            }
        }
    }
}
