
using DemoGrpcClient;

var service = new CounterService();
var opcao = 1;
View();

while (opcao != 0)
{
    opcao = int.Parse(Console.ReadLine() ?? "");
    Console.Clear();
    switch (opcao)
    {
        case 1 : {
            await GetWithOutStreamAsync();
            break;
        }
        
        case 2 : {
            await GetWithStreamAsync();
            break;
        }
        default: {
            opcao = 0;
            break;
        }
    }
    
    Console.WriteLine("---------------------------------------------------------------------------\n\n");
    View();
}

async Task GetWithStreamAsync() 
{
    await service.GetWithStreamAsync();
} 

async Task GetWithOutStreamAsync() 
{
    var res = await service.GetAsync();

    foreach (var item in res)
    {
        Console.WriteLine(item);
    }
} 

void View() 
{
    Console.WriteLine("Digite o número 1 para buscar os dados sem stream");
    Console.WriteLine("Digite o número 2 para buscar os dados com stream");
    Console.WriteLine("Digite o número 0 para sair");
}