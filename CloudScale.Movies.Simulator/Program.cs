using Autofac;
using CloudScale.Movies.Messages;
using Nimbus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.ColoredConsole()
                        .CreateLogger();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Program).Assembly);
            var container = builder.Build();

            IBus bus = container.Resolve<IBus>();

            while (true)
            {
                Console.WriteLine("Hit Enter to send Ping");

                string readLine = Console.ReadLine();

                if (string.IsNullOrEmpty(readLine))
                {
                    Task<IEnumerable<PingResponse>> responses = bus.MulticastRequest<PingRequest, PingResponse>(new PingRequest(), TimeSpan.FromSeconds(5));
                    responses.Wait();

                    foreach (PingResponse pingResponse in responses.Result)
                    {
                        Log.Information("Received Response: {From}", pingResponse.Details);
                    }

                    Console.WriteLine("...");
                }
                if (readLine == "import")
                {
                    string filename = @"C:\Jobs\CloudScale\MoviesForThomas.txt";
                    if (System.IO.File.Exists(filename))
                    {
                        string[] lines = System.IO.File.ReadAllLines(filename);

                        string[] headers = lines.First().Split('\t');
                        List<string> people = new List<string>();
                        foreach (string item in headers.Skip(6))
                        {
                            string name = item.Trim();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                Log.Information("Person {Person}", name);
                                people.Add(name);
                            }
                        }

                        foreach (string line in lines.Skip(1))
                        {
                            string[] split = line.Split('\t');

                            string movieName = split[0];
                            if (!string.IsNullOrWhiteSpace(movieName))
                            {
                                Log.Information("Checking Movie {Movie}", movieName);

                                //Task<IsRegisteredResponse> request = bus.Request<IsRegisteredRequest, IsRegisteredResponse>(new IsRegisteredRequest(movieName));
                                //request.Wait(2500);

                                //if (request.IsCompleted && request.Result.Registered)
                                //{
                                //    Log.Information("Already Registered");
                                //}
                                //else
                                //{
                                //    Log.Information("New, Adding");

                                //    bus.Publish<NewMovieEvent>(new NewMovieEvent(movieName));
                                //    System.Threading.Thread.Sleep(1500);
                                //}

                                int index = 0;
                                foreach (string item in split.Skip(6))
                                {
                                    string value = item.Trim();
                                    if (!string.IsNullOrWhiteSpace(value))
                                    {
                                        string name = people[index++];
                                        
                                        if (!string.IsNullOrWhiteSpace(name))
                                        {
                                            double score = 0;
                                            double.TryParse(value, out score);
                                            
                                            Log.Information("Register Score For {Movie} to {Person} for {Score}", movieName, name, score);

                                            //bus.Publish<NewScoreEvent>(new NewScoreEvent(movieName, name, score));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
