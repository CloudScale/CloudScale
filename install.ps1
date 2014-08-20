
$messageProjects = @("CloudScale.Movies.Messages")
$serviceProjects = @("CloudScale.Api", "CloudScale.SignalR", "CloudScale.Movies.DataService", "CloudScale.Movies.LookupService", "CloudScale.Movies.Simulator")

foreach ($project in $messageProjects) 
{ 
	Install-Package Nimbus.MessageContracts -Pre -Project $project 
} 

foreach ($project in $serviceProjects) 
{
	Install-Package Nimbus.MessageContracts -Pre -Project $project
	Install-Package Nimbus.InfrastructureContracts -Pre -Project $project
	Install-Package Nimbus -Pre -Project $project
	Install-Package Autofac -Project $project
	Install-Package Nimbus.Autofac -Pre -Project $project
	Install-Package Serilog -Project $project
	Install-Package Nimbus.Logger.Serilog -Project $project
}


# Install-Package Microsoft.WindowsAzure.ConfigurationManager