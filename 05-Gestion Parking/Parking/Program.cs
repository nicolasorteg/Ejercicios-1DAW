using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();


Console.WriteLine("Hello, World!");
