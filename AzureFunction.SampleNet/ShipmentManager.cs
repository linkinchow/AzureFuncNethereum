using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BlockChain.Contracts.Shipment;
using BlockChain.Contracts.ShipmentManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;

namespace AzureFunction.SampleNet
{
    public static class ShipmentManager
    {
        private static Web3 GetWeb3()
        {
            var account = Nethereum.Web3.Accounts.Account.LoadFromKeyStore(Environment.GetEnvironmentVariable("keystore"), Environment.GetEnvironmentVariable("password"));
            var authParameter = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("usr") + ":" + Environment.GetEnvironmentVariable("pwd"))));
            return new Web3(account, new RpcClient(new Uri("https://" + Environment.GetEnvironmentVariable("hostname") + ":" + Environment.GetEnvironmentVariable("port")), authParameter));
        }

        [FunctionName("GetShipment")]
        public static async Task<IActionResult> GetShipment([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function GetShipment processed a request.");

            // parse query parameter
            string awbNumber = req.Query["awbNumber"];

            if (awbNumber == null)
            {
                return new BadRequestObjectResult("Please pass 'awbNumber' on the query string.");
            }

            //var authParameter = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("usr") + ":" + Environment.GetEnvironmentVariable("pwd"))));
            //var web3 = new Web3(new RpcClient(new Uri("https://" + Environment.GetEnvironmentVariable("hostname") + ":" + Environment.GetEnvironmentVariable("port")), authParameter));
            var svc = new ShipmentManagerService(GetWeb3(), Environment.GetEnvironmentVariable("shipmentManagerAddress"));
            var shipmentAddr = await svc.GetShipmentQueryAsync(awbNumber);
            return new OkObjectResult(shipmentAddr);
        }

        [FunctionName("CreateShipment")]
        public static async Task<IActionResult> CreateShipment([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function CreateShipment processed a request.");

            dynamic data = await req.Content.ReadAsAsync<object>();
            string airlineAddr = data?.airlineAddr;
            string agentAddr = data?.agentAddr;
            string ghaAddr = data?.agentAddr;
            string awbNumber = data?.awbNumber;
            string airlineCompanyID = data?.airlineCompanyID;
            string airlineCity = data?.airlineCity;
            string agentCompanyID = data?.agentCompanyID;
            string agentCity = data?.agentCity;
            
            var svc = new ShipmentManagerService(GetWeb3(), Environment.GetEnvironmentVariable("shipmentManagerAddress"));
            await svc.CreateShipmentRequestAsync(
                airlineAddr,
                agentAddr,
                ghaAddr,
                awbNumber,
                airlineCompanyID,
                airlineCity,
                agentCompanyID,
                agentCity
                );
            return new OkObjectResult("Shipment with awbNumber " + awbNumber + " is created.");
        }

        [FunctionName("GetAwbNumber")]
        public static async Task<IActionResult> GetAwbNumber([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function GetAwbNumber processed a request.");

            // parse query parameter
            string shipmentAddr = req.Query["shipmentAddr"];

            if (shipmentAddr == null)
            {
                return new BadRequestObjectResult("Please pass 'shipmentAddr' on the query string.");
            }

            var svc = new ShipmentService(GetWeb3(), shipmentAddr);
            var awbNumber = await svc.AwbNumberQueryAsync();
            return new OkObjectResult(awbNumber);
        }

        [FunctionName("InsertFlown")]
        public static async Task<IActionResult> InsertFlown([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function InsertFlown processed a request.");

            dynamic data = await req.Content.ReadAsAsync<object>();
            string shipmentAddr = data?.shipmentAddr;
            string flightCarrier = data?.flightCarrier;
            string flightNumber = data?.flightNumber;
            BigInteger flightDate = data?.flightDate;
            string boardPoint = data?.boardPoint;
            string offPoint = data?.offPoint;
            string uldInfo = data?.uldInfo;

            var svc = new ShipmentService(GetWeb3(), shipmentAddr);
            await svc.InsertFlownRequestAsync(
                flightCarrier,
                flightNumber,
                flightDate,
                boardPoint,
                offPoint,
                uldInfo
                );
            return new OkObjectResult("Flown is inserted.");
        }

        [FunctionName("UpdateAirlineRatedShipment")]
        public static async Task<IActionResult> UpdateAirlineRatedShipment([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function UpdateAirlineRatedShipment processed a request.");

            dynamic data = await req.Content.ReadAsAsync<object>();
            string shipmentAddr = data?.shipmentAddr;
            string ratedShipmentAddress = data?.ratedShipmentAddress;

            var svc = new ShipmentService(GetWeb3(), shipmentAddr);
            await svc.UpdateAirlineRatedShipmentRequestAsync(ratedShipmentAddress);
            return new OkObjectResult("Updated");
        }

        [FunctionName("UpdateAgentRatedShipment")]
        public static async Task<IActionResult> UpdateAgentRatedShipment([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function UpdateAgentRatedShipment processed a request.");

            dynamic data = await req.Content.ReadAsAsync<object>();
            string shipmentAddr = data?.shipmentAddr;
            string ratedShipmentAddress = data?.ratedShipmentAddress;

            var svc = new ShipmentService(GetWeb3(), shipmentAddr);
            await svc.UpdateAgentRatedShipmentRequestAsync(ratedShipmentAddress);
            return new OkObjectResult("Updated");
        }
    }
}
