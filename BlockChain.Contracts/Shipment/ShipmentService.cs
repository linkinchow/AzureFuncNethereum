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
using BlockChain.Contracts.Shipment.ContractDefinition;
namespace BlockChain.Contracts.Shipment
{

    public partial class ShipmentService
    {
    
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ShipmentDeployment shipmentDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ShipmentDeployment>().SendRequestAndWaitForReceiptAsync(shipmentDeployment, cancellationTokenSource);
        }
        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ShipmentDeployment shipmentDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ShipmentDeployment>().SendRequestAsync(shipmentDeployment);
        }
        public static async Task<ShipmentService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ShipmentDeployment shipmentDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, shipmentDeployment, cancellationTokenSource);
            return new ShipmentService(web3, receipt.ContractAddress);
        }
    
        protected Nethereum.Web3.Web3 Web3{ get; }
        
        public ContractHandler ContractHandler { get; }
        
        public ShipmentService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }
    
        public Task<string> UpdateAgentRatedShipmentRequestAsync(UpdateAgentRatedShipmentFunction updateAgentRatedShipmentFunction)
        {
             return ContractHandler.SendRequestAsync(updateAgentRatedShipmentFunction);
        }

        public Task<TransactionReceipt> UpdateAgentRatedShipmentRequestAndWaitForReceiptAsync(UpdateAgentRatedShipmentFunction updateAgentRatedShipmentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateAgentRatedShipmentFunction, cancellationToken);
        }

        public Task<string> UpdateAgentRatedShipmentRequestAsync(string ratedShipmentAddress)
        {
            var updateAgentRatedShipmentFunction = new UpdateAgentRatedShipmentFunction();
                updateAgentRatedShipmentFunction.RatedShipmentAddress = ratedShipmentAddress;
            
             return ContractHandler.SendRequestAsync(updateAgentRatedShipmentFunction);
        }

        public Task<TransactionReceipt> UpdateAgentRatedShipmentRequestAndWaitForReceiptAsync(string ratedShipmentAddress, CancellationTokenSource cancellationToken = null)
        {
            var updateAgentRatedShipmentFunction = new UpdateAgentRatedShipmentFunction();
                updateAgentRatedShipmentFunction.RatedShipmentAddress = ratedShipmentAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateAgentRatedShipmentFunction, cancellationToken);
        }

        public Task<GetFlownInfoOutputDTO> GetFlownInfoQueryAsync(GetFlownInfoFunction getFlownInfoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetFlownInfoFunction, GetFlownInfoOutputDTO>(getFlownInfoFunction, blockParameter);
        }

        
        public Task<GetFlownInfoOutputDTO> GetFlownInfoQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetFlownInfoFunction, GetFlownInfoOutputDTO>(null, blockParameter);
        }



        public Task<string> AgentRatedShipmentAddressQueryAsync(AgentRatedShipmentAddressFunction agentRatedShipmentAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AgentRatedShipmentAddressFunction, string>(agentRatedShipmentAddressFunction, blockParameter);
        }

        
        public Task<string> AgentRatedShipmentAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AgentRatedShipmentAddressFunction, string>(null, blockParameter);
        }



        public Task<string> UpdateAirlineRatedShipmentRequestAsync(UpdateAirlineRatedShipmentFunction updateAirlineRatedShipmentFunction)
        {
             return ContractHandler.SendRequestAsync(updateAirlineRatedShipmentFunction);
        }

        public Task<TransactionReceipt> UpdateAirlineRatedShipmentRequestAndWaitForReceiptAsync(UpdateAirlineRatedShipmentFunction updateAirlineRatedShipmentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateAirlineRatedShipmentFunction, cancellationToken);
        }

        public Task<string> UpdateAirlineRatedShipmentRequestAsync(string ratedShipmentAddress)
        {
            var updateAirlineRatedShipmentFunction = new UpdateAirlineRatedShipmentFunction();
                updateAirlineRatedShipmentFunction.RatedShipmentAddress = ratedShipmentAddress;
            
             return ContractHandler.SendRequestAsync(updateAirlineRatedShipmentFunction);
        }

        public Task<TransactionReceipt> UpdateAirlineRatedShipmentRequestAndWaitForReceiptAsync(string ratedShipmentAddress, CancellationTokenSource cancellationToken = null)
        {
            var updateAirlineRatedShipmentFunction = new UpdateAirlineRatedShipmentFunction();
                updateAirlineRatedShipmentFunction.RatedShipmentAddress = ratedShipmentAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateAirlineRatedShipmentFunction, cancellationToken);
        }

        public Task<GetShipmentOwnerOutputDTO> GetShipmentOwnerQueryAsync(GetShipmentOwnerFunction getShipmentOwnerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetShipmentOwnerFunction, GetShipmentOwnerOutputDTO>(getShipmentOwnerFunction, blockParameter);
        }

        
        public Task<GetShipmentOwnerOutputDTO> GetShipmentOwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetShipmentOwnerFunction, GetShipmentOwnerOutputDTO>(null, blockParameter);
        }



        public Task<GetShipmentInfoOutputDTO> GetShipmentInfoQueryAsync(GetShipmentInfoFunction getShipmentInfoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetShipmentInfoFunction, GetShipmentInfoOutputDTO>(getShipmentInfoFunction, blockParameter);
        }

        
        public Task<GetShipmentInfoOutputDTO> GetShipmentInfoQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetShipmentInfoFunction, GetShipmentInfoOutputDTO>(null, blockParameter);
        }



        public Task<string> SaveShipmentRequestAsync(SaveShipmentFunction saveShipmentFunction)
        {
             return ContractHandler.SendRequestAsync(saveShipmentFunction);
        }

        public Task<TransactionReceipt> SaveShipmentRequestAndWaitForReceiptAsync(SaveShipmentFunction saveShipmentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(saveShipmentFunction, cancellationToken);
        }

        public Task<string> SaveShipmentRequestAsync(string bookingFlights, BigInteger totalGrossWeightMiniGrams, BigInteger totalChargableWeightMiniGrams, BigInteger shipmentTransactionDT)
        {
            var saveShipmentFunction = new SaveShipmentFunction();
                saveShipmentFunction.BookingFlights = bookingFlights;
                saveShipmentFunction.TotalGrossWeightMiniGrams = totalGrossWeightMiniGrams;
                saveShipmentFunction.TotalChargableWeightMiniGrams = totalChargableWeightMiniGrams;
                saveShipmentFunction.ShipmentTransactionDT = shipmentTransactionDT;
            
             return ContractHandler.SendRequestAsync(saveShipmentFunction);
        }

        public Task<TransactionReceipt> SaveShipmentRequestAndWaitForReceiptAsync(string bookingFlights, BigInteger totalGrossWeightMiniGrams, BigInteger totalChargableWeightMiniGrams, BigInteger shipmentTransactionDT, CancellationTokenSource cancellationToken = null)
        {
            var saveShipmentFunction = new SaveShipmentFunction();
                saveShipmentFunction.BookingFlights = bookingFlights;
                saveShipmentFunction.TotalGrossWeightMiniGrams = totalGrossWeightMiniGrams;
                saveShipmentFunction.TotalChargableWeightMiniGrams = totalChargableWeightMiniGrams;
                saveShipmentFunction.ShipmentTransactionDT = shipmentTransactionDT;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(saveShipmentFunction, cancellationToken);
        }

        public Task<string> AwbNumberQueryAsync(AwbNumberFunction awbNumberFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AwbNumberFunction, string>(awbNumberFunction, blockParameter);
        }

        
        public Task<string> AwbNumberQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AwbNumberFunction, string>(null, blockParameter);
        }



        public Task<string> InsertFlownRequestAsync(InsertFlownFunction insertFlownFunction)
        {
             return ContractHandler.SendRequestAsync(insertFlownFunction);
        }

        public Task<TransactionReceipt> InsertFlownRequestAndWaitForReceiptAsync(InsertFlownFunction insertFlownFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(insertFlownFunction, cancellationToken);
        }

        public Task<string> InsertFlownRequestAsync(string flightCarrier, string flightNumber, BigInteger flightDate, string boardPoint, string offPoint, string uldInfo)
        {
            var insertFlownFunction = new InsertFlownFunction();
                insertFlownFunction.FlightCarrier = flightCarrier;
                insertFlownFunction.FlightNumber = flightNumber;
                insertFlownFunction.FlightDate = flightDate;
                insertFlownFunction.BoardPoint = boardPoint;
                insertFlownFunction.OffPoint = offPoint;
                insertFlownFunction.UldInfo = uldInfo;
            
             return ContractHandler.SendRequestAsync(insertFlownFunction);
        }

        public Task<TransactionReceipt> InsertFlownRequestAndWaitForReceiptAsync(string flightCarrier, string flightNumber, BigInteger flightDate, string boardPoint, string offPoint, string uldInfo, CancellationTokenSource cancellationToken = null)
        {
            var insertFlownFunction = new InsertFlownFunction();
                insertFlownFunction.FlightCarrier = flightCarrier;
                insertFlownFunction.FlightNumber = flightNumber;
                insertFlownFunction.FlightDate = flightDate;
                insertFlownFunction.BoardPoint = boardPoint;
                insertFlownFunction.OffPoint = offPoint;
                insertFlownFunction.UldInfo = uldInfo;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(insertFlownFunction, cancellationToken);
        }

        public Task<string> AirlineRatedShipmentAddressQueryAsync(AirlineRatedShipmentAddressFunction airlineRatedShipmentAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AirlineRatedShipmentAddressFunction, string>(airlineRatedShipmentAddressFunction, blockParameter);
        }

        
        public Task<string> AirlineRatedShipmentAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AirlineRatedShipmentAddressFunction, string>(null, blockParameter);
        }



        public Task<string> FlownUldsQueryAsync(FlownUldsFunction flownUldsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FlownUldsFunction, string>(flownUldsFunction, blockParameter);
        }

        
        public Task<string> FlownUldsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FlownUldsFunction, string>(null, blockParameter);
        }


    }
}
