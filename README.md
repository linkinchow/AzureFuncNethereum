# Exposing Smart Contract Functions as Azure Functions with Nethereum

This document describes how to expose smart contract functions as C# Azure Functions with [Nethereum](https://nethereum.readthedocs.io/en/latest/), a .Net library for Ethereum. We will discuss Nethereum, Azure Functions and Azure DevOps, but we will not discuss details of the scenario and smart contracts.

Assume we have already deployed a dozen of smart contracts to Azure Westlake landscape for a shippng scenario and we can successfully connect to the server and call smart contract functions with truffle and web3 in node.js, what we want to do is exposing those smart contract functions as Azure Functions for easy use. With Azure Functions, users can just call REST APIs to communicate with smart contracts and it doesn't matter which language they are using.

## Step by step tutorials
1. Suppose there is already a truffle project in Azure DevOps or GitHub, clone it to VS Code. For Azure DevOps, you can install an extension called [Azure Repos](https://marketplace.visualstudio.com/items?itemName=ms-vsts.team) which enables you to work with Azure DevOps seamlessly.
2. Assume all of the smart contracts are located in the "contracts" folder, we can generate C# classes mapping to smart contracts very easily from the extension [solidity](https://marketplace.visualstudio.com/items?itemName=JuanBlanco.solidity). In our case, there are two sub-steps, "Compile all Solidity Contracts" and "Code generate C# project from all compiled files". You can refer to this [guide](https://nethereum.readthedocs.io/en/latest/nethereum-code-generation/).
3. Till now we have generated all C# classes we need for smart contracts. We can start Azure Functions. There are two options, you can keep using VS Code with [Azure Functions](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions) or you can switch to Visual Studio (it also has Auzre Functions plugin) for better coding/testing experience with C#. There is a [sample](https://github.com/Nethereum/AzureFunction.Sample) on GitHub which gets the balance of an address. Although it's not related with smart contract functions, it's a good starting point for Nethereum with Azure Functions.
4. Write your Azure Functions code to expose smart contract functions with Nethereum and generated C# classes.

## Notes
- When you generate C# classes for smart contracts, a dedicated C# project will be created. To keep it clean, it's recommended to have a separated C# project for Azure Functions and you can add the generated C# project as reference to the Azure Functions project.
- Make sure both projects use the same version of Nethereum.Web3 package. You can check it in .csproj file, e.g., `<PackageReference Include="Nethereum.Web3" Version="3.0.0-rc3" />`
- The generated project uses .Net Core, e.g., `<TargetFramework>netstandard2.0</TargetFramework>`, it's recommended to use the same framework for Azure Functions project.
- It's recommended to use latest v2 for [Azure Functions runtime versions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-versions), e.g., `<AzureFunctionsVersion>v2</AzureFunctionsVersion>`
- Currently Nethereum cannot handle HTTPS URL with Basic authentication like format `https://<USERNAME>:<PASSWORD>@HOSTNAME:PORT`, so if you use `var web3 = new Nethereum.Web3.Web3("https://<USERNAME>:<PASSWORD>@HOSTNAME:PORT");`, you will get '401 Unauthorized' error message. Please use following code snippet to connect to the server correctly.

    `var authParameter = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))); var web3 = new Web3(new RpcClient($"https://{dns}:{port}", authParameter));`

- You can place all the credentials and connections in local.settings.json file and please do not push this file to Azure DevOps or GitHub. You can name it in .gitignore file.

## Enable Azure DevOps

We can use Azure DevOps (previously called VSTS) for CI/CD. There are two types of pipelines in Azure DevOps, Build Pipeline for CI and Release Pipeline for CD. You can refer to this [link](https://docs.microsoft.com/en-us/azure/devops/?view=vsts) for more details about Azure DevOps. Following is the steps in our case.

1. Create a feature branch for Azure Functions with Nethereum.
2. Clone the feature branch to VS Code. Actually that's what we did in step 1 of "Step by step tutorials" section.
3. Write your code and test locally.
4. Push to the feature branch and create a pull request.
5. Create Build Pipeline. Since both of our projects use .Net Core, we can use ".Net Core" template with Restore, Build, Test, Publish steps. Please specify the Azure Functions project; otherwise there will be two projects in the zip file which will cause errors in the Release Pipeline.
6. Create Release Pipeline. Since we want to deploy our C# project as Auzre Functions, we can choose "Deploy Azure App Service" template with only one step.
7. Choose the zip file which is generated in the Build Pipeline as Artifact and pick "Latest" for "Default Version", so each time the latest built artifact will be chosen by default.
8. Choose "Function App" for "App Type".
9. Input "App settings" part in "Application and Configuration Settings" tab. Just use key/value pair format to copy everything in local.settings.json file.