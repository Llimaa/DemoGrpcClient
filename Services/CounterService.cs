using DemoGrpcClient;
using Grpc.Core;
using Grpc.Net.Client;

namespace DemoGrpcClient;

public class CounterService 
{
    public async Task<List<int>> GetAsync() 
    {
        
        using var channel = GrpcChannel.ForAddress("http://localhost:5287");
        var client = new Counter.CounterClient(channel);
        
        System.Console.WriteLine("Buscando os dados, aguarde ...");

        var request = new CountRequest{ StartWith= 1, EndWith = 5};
        var resut = await client.CountAsync(request);

        return resut.Value.Select(_ => _.Value).ToList();
    }

    public async Task<List<int>> GetWithStreamAsync() 
    {
        var resultList = new List<int>();
        using var channel = GrpcChannel.ForAddress("http://localhost:5287");
        var client = new Counter.CounterClient(channel);

        var request = new CountRequest{ StartWith= 1, EndWith = 5};
        
        
        using (var call = client.CountStream(request))
        {
            System.Console.WriteLine("Fazendo Stream");
            await foreach (var response in call.ResponseStream.ReadAllAsync())
            {
                System.Console.WriteLine(response.Value);
                resultList.Add(response.Value);
            }
            System.Console.WriteLine("Fim do Stream");
        }        
        return resultList;
    }
}