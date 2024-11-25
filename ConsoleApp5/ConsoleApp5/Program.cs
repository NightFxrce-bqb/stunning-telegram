using System;
using System.Collections.Generic;

// Абстрактный класс обработчика
public abstract class EventHandler
{
    protected EventHandler _nextHandler;

    public void SetNext(EventHandler handler)
    {
        _nextHandler = handler;
    }

    public virtual void HandleRequest(EventRequest request)
    {
        if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

// Класс запроса
public class EventRequest
{
    public string EventType { get; }
    public string Content { get; }

    public EventRequest(string eventType, string content)
    {
        EventType = eventType;
        Content = content;
    }
}

// Конкретные обработчики
public class LoggingEventHandler : EventHandler
{
    public override void HandleRequest(EventRequest request)
    {
        if (request.EventType == "Log")
        {
            Console.WriteLine($"LoggingEventHandler: {request.Content}");
        }
        else
        {
            base.HandleRequest(request);
        }
    }
}

public class NotificationEventHandler : EventHandler
{
    public override void HandleRequest(EventRequest request)
    {
        if (request.EventType == "Notify")
        {
            Console.WriteLine($"NotificationEventHandler: {request.Content}");
        }
        else
        {
            base.HandleRequest(request);
        }
    }
}

public class ErrorEventHandler : EventHandler
{
    public override void HandleRequest(EventRequest request)
    {
        if (request.EventType == "Error")
        {
            Console.WriteLine($"ErrorEventHandler: {request.Content}");
        }
        else
        {
            base.HandleRequest(request);
        }
    }
}

// Программа
public class Program
{
    public static void Main(string[] args)
    {
        // Создание обработчиков
        var loggingHandler = new LoggingEventHandler();
        var notificationHandler = new NotificationEventHandler();
        var errorHandler = new ErrorEventHandler();

        // Связывание обработчиков в цепочку
        loggingHandler.SetNext(notificationHandler);
        notificationHandler.SetNext(errorHandler);

        // Создание запросов
        var logRequest = new EventRequest("log", "это сообщение логов");
        var notifyRequest = new EventRequest("Notify", "это сообщение уведомлений");
        var errorRequest = new EventRequest("Error", "это сообщение ошибки");
        var unknownRequest = new EventRequest("Unknown", "сообщение неизвестного запроса");

        // Обработка запросов
        Console.WriteLine("обработка запросов");
        loggingHandler.HandleRequest(logRequest);
        loggingHandler.HandleRequest(notifyRequest);
        loggingHandler.HandleRequest(errorRequest);
        loggingHandler.HandleRequest(unknownRequest); // Не будет обработан
    }
}