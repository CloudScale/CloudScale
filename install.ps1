
$messageProjects = @("CloudScale.Movies.Messages")
$serviceProjects = @("CloudScale.Api", "CloudScale.SignalR", "CloudScale.Movies.DataService", "CloudScale.Movies.LookupService", "CloudScale.Movies.Simulator")

foreach ($project in $messageProjects) 
{ 
	Install-Package Nimbus.MessageContracts -Project $project 
} 

foreach ($project in $serviceProjects) 
{
	Install-Package Autofac -Project $project
	Install-Package Nimbus -Project $project
	Install-Package Nimbus.Autofac -Project $project
	Install-Package Nimbus.MessageContracts -Project $project
	Install-Package Nimbus.InfrastructureContracts -Project $project
	Install-Package Serilog -Project $project
	Install-Package Nimbus.Logger.Serilog -Project $project
}


# Install-Package Microsoft.WindowsAzure.ConfigurationManager