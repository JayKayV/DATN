using System;
using System.Collections.Generic;

namespace SharedLibrary.Event
{
    public class Message : System.EventArgs
    {
        public Dictionary<string, object> Data;
        public Message()
        {
            Data = new Dictionary<string, object>();
        }

        public Message(Dictionary<string, object> data)
        {
            Data = data;
        }

        public T? GetData<T>(string key)
        {
            if (!Data.ContainsKey(key))
                return default(T?);
            return (T?)Data[key];
        }
    }

    public class EventWrapper
    {
        public event EventHandler<Message>? Event;

        public static EventWrapper operator +(EventWrapper wrapper, Action<Message> handle) {
            wrapper.Event += Convert(handle);
            return wrapper;
        }

        public static EventWrapper operator -(EventWrapper wrapper, Action<Message> handle)
        {
            wrapper.Event -= Convert(handle);
            return wrapper;
        }

        private static EventHandler<Message> Convert(Action<Message> handle)
        {
            return (object sender, Message m) => { handle.Invoke(m); };
        }

        public void Invoke(Message message)
        {
            Event.Invoke(this, message);
        }
    }

    public class EventBus
    {
        public Dictionary<string, Message> Messages { get; set; }
        public Dictionary<string, EventWrapper> Events { get; set; }

        public EventBus() {
            Messages = new Dictionary<string, Message>();
            Events = new Dictionary<string, EventWrapper>();
        }

        public void SetMessage(string key, Message value)
        {
            Messages[key] = value;
        }

        public void RaiseEvent(string key)
        {
            if (Events.ContainsKey(key))
            {
                if (Messages.ContainsKey(key))
                    Events[key].Invoke(Messages[key]);
                else
                    Events[key].Invoke(null);
            }
        }

        public void Subscribe(string key, Action<Message> handle)
        {
            if (!Events.ContainsKey(key))
            {
                Events[key] = new EventWrapper();
                Events[key] += handle;
            }
            else
                Events[key] += handle;
        }

        public void Unsubscribe(string key, Action<Message> handle)
        {
            if (!Events.ContainsKey(key))
            {
                return;
            }
            Events[key] -= handle;
        }

        public void RemoveEvent(string key)
        {
            if (!Events.ContainsKey(key)) 
                return;
            Events.Remove(key);
        }

        public void Clear()
        {
            Messages.Clear();
            Events.Clear();
        }
    }
}
