using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Log4net
{
    public class EventHelper
    {
        static IptLogger m_log = new IptLogger("EventHelper");
        public static void FireEvent(Delegate del, params object[] args)
        {
            if (del == null)
                return;
            Delegate[] delegates = del.GetInvocationList();
            foreach (Delegate sink in delegates)
            {
                try
                {
                    sink.DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    m_log.Debug(ex.ToString());
                }

            }
        }
        delegate void AsyncFire(Delegate del, object[] args);
        static void InvokeDelegate(Delegate del, object[] args)
        {
            try
            {
                del.DynamicInvoke(args);
            }
            catch (Exception ex)
            {
                m_log.Error(ex.ToString());
            }
        }

        public static void FireEventAsync(Delegate del, params object[] args)
        {
            if (del == null)
                return;
            Delegate[] delegates = del.GetInvocationList();
            AsyncFire asyncFire;
            foreach (Delegate sink in delegates)
            {
                try
                {
                    asyncFire = new AsyncFire(InvokeDelegate);
                    asyncFire.BeginInvoke(sink, args, null, null);
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());
                }
            }
        }

        public static void FireEventAsyncForClient(Delegate del, params object[] args)
        {
            if (del == null)
                return;
            AsyncFire asyncFire;
            try
            {
                asyncFire = new AsyncFire(InvokeDelegate);
                asyncFire.BeginInvoke(del, args, null, null);
            }
            catch (Exception ex)
            {
                m_log.Error(ex.ToString());
            }
        }

        public delegate void EventAction(object target, EventArgs args);
        public static void FireActionAsync(EventAction action, object target, EventArgs args)
        {
            action.BeginInvoke(target, args, null, null);
        }
    }
}
