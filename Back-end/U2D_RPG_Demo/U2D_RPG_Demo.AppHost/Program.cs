var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.U2D_RPG_Demo_ApiServer>("u2d-rpg-demo-apiserver");

builder.Build().Run();
