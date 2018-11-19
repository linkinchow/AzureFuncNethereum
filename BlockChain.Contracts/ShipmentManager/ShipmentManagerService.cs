using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using BlockChain.Contracts.ShipmentManager.ContractDefinition;
namespace BlockChain.Contracts.ShipmentManager
{

    public partial class ShipmentManagerService
    {
    
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ShipmentManagerDeployment shipmentManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ShipmentManagerDeployment>().SendRequestAndWaitForReceiptAsync(shipmentManagerDeployment, cancellationTokenSource);
        }
        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ShipmentManagerDeployment shipmentManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ShipmentManagerDeployment>().SendRequestAsync(shipmentManagerDeployment);
        }
        public static async Task<ShipmentManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ShipmentManagerDeployment shipmentManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, shipmentManagerDeployment, cancellationTokenSource);
            return new ShipmentManagerService(web3, receipt.ContractAddress);
        }
    
        protected Nethereum.Web3.Web3 Web3{ get; }
        
        public ContractHandler ContractHandler { get; }
        
        public ShipmentManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }
    
        public Task<string> CreateShipmentRequestAsync(CreateShipmentFunction createShipmentFunction)
        {
             return ContractHandler.SendRequestAsync(createShipmentFunction);
        }

        public Task<TransactionReceipt> CreateShipmentRequestAndWaitForReceiptAsync(CreateShipmentFunction createShipmentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createShipmentFunction, cancellationToken);
        }

        public Task<string> CreateShipmentRequestAsync(string airlineAdd, string agentAdd, string ghaAdd, string awbNumber, string airlineCompanyID, string airlineCity, string agentCompanyID, string agentCity)
        {
            var createShipmentFunction = new CreateShipmentFunction();
                createShipmentFunction.AirlineAdd = airlineAdd;
                createShipmentFunction.AgentAdd = agentAdd;
                createShipmentFunction.GhaAdd = ghaAdd;
                createShipmentFunction.AwbNumber = awbNumber;
                createShipmentFunction.AirlineCompanyID = airlineCompanyID;
                createShipmentFunction.AirlineCity = airlineCity;
                createShipmentFunction.AgentCompanyID = agentCompanyID;
                createShipmentFunction.AgentCity = agentCity;
            
             return ContractHandler.SendRequestAsync(createShipmentFunction);
        }

        public Task<TransactionReceipt> CreateShipmentRequestAndWaitForReceiptAsync(string airlineAdd, string agentAdd, string ghaAdd, string awbNumber, string airlineCompanyID, string airlineCity, string agentCompanyID, string agentCity, CancellationTokenSource cancellationToken = null)
        {
            var createShipmentFunction = new CreateShipmentFunction();
                createShipmentFunction.AirlineAdd = airlineAdd;
                createShipmentFunction.AgentAdd = agentAdd;
                createShipmentFunction.GhaAdd = ghaAdd;
                createShipmentFunction.AwbNumber = awbNumber;
                createShipmentFunction.AirlineCompanyID = airlineCompanyID;
                createShipmentFunction.AirlineCity = airlineCity;
                createShipmentFunction.AgentCompanyID = agentCompanyID;
                createShipmentFunction.AgentCity = agentCity;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createShipmentFunction, cancellationToken);
        }

        public Task<string> GetShipmentQueryAsync(GetShipmentFunction getShipmentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetShipmentFunction, string>(getShipmentFunction, blockParameter);
        }

        
        public Task<string> GetShipmentQueryAsync(string awbNumber, BlockParameter blockParameter = null)
        {
            var getShipmentFunction = new GetShipmentFunction();
                getShipmentFunction.AwbNumber = awbNumber;
            
            return ContractHandler.QueryAsync<GetShipmentFunction, string>(getShipmentFunction, blockParameter);
        }


    }
}
