# MVVMCorssHandsOn

Hands On onde desenvolvemos uma pequena aplicação em Xamarin utilizando a abordagem tradicional e o Framework MVVM Cross.

# Projeto

Primeiramente devemos criar uma solução para conter nossos projetos.

## Criar uma solution

File -> New -> Project -> Other Projects Types -> Visual Studio Solutions

Em "Name" Informe o nome "HOMvvmCross"

## Criando o projeto "CORE"

Assim como na abordagem do Xamarin.Forms, vamos criar um projeto Core que para concentrar tudo o que será comum em relação a nossa aplicação para as plataformas.

Na solution, clique com o botão direito do mouse e selecione:

Add -> New Project -> Cross-Platform -> Class Library

Defina o nome para "HOMvvmCross.Core"

* Se apareceu somente a opção  "Class Library (Xamarin.Forms)", pode criar o projeto com esta opçao e após a criação, exclui a classe padrão que foi criada e também remova o pacote Nuget Xamarin.Forms.

Após a criação do projeto, vamos adicionar o pacote do MVVMCross.

Clique com o botão direito do mouse sobre o projeto e em seguida em "Manage Nuget Packages".

Em "Browse" informe: MVVMCross Starter Pack

Clique em Install

## Criando o projeto "Android"

O projeto Android será criado manualmente e iremos adicionar as referências do projeto Core e também do framework MVVMCross.

Na solution, clique com o botão direito do mouse e selecione:

Add -> New Project -> Android -> Blank App

Defina o nome para "HOMvvmCross.Droid"

Após a criação do projeto, vamos adicionar o pacote do MVVMCross.

Clique com o botão direito do mouse sobre o projeto e em seguida em "Manage Nuget Packages".

Em "Browse" informe: MVVMCross Starter Pack

Adicione a referência do projeto Core:

Clique com o botão direito do mouse no projeto android, em seguida:

Add -> Reference ... -> Projects -> HOMvvmCross.Core

Exclua o arquivo **MainActivity.cs**.


## Criando o projeto "IOS"

O projeto Ios também será criado manualmente com as referências do projeto Core e também do framework MVVMCross.

Na solution, clique com o botão direito do mouse e selecione:

Add -> New Project -> iOS -> IPhone -> Blank App (IPhone)

Defina o nome para "HOMvvmCross.Touch"

Após a criação do projeto, vamos adicionar o pacote do MVVMCross.

Clique com o botão direito do mouse sobre o projeto e em seguida em "Manage Nuget Packages".

Em "Browse" informe: MVVMCross Starter Pack

Adicione a referência do projeto Core:

Clique com o botão direito do mouse no projeto android, em seguida:

Add -> Reference ... -> Projects -> HOMvvmCross.Core

No projeto, foi criado um arquivo **AppDelegate.txt**.

Copie todo o conteúdo deste arquivo e substritua no arquivo **AppDelegate.cs**.

Exclua o arquivo **AppDelegate.txt**.

## Dependências

Algumas dependências serão adicionadas aos projetos:

- [Refit - versão 3.10](https://www.nuget.org/packages/Refit/4.0.1) - Abstrair o consumo de serviços REST.

# API 

Não é intuito deste Hands On desenvolver toda a infraestrutura da nossa aplicação. Entretanto, devemos comunicar com uma API e, portanto, utilizaremos uma api pública.

[Dog API](https://dog.ceo/dog-api) - Retornar informações sobre cachorros.

Endereço base: https://dog.ceo


# Integrando com a API

Conforme descrito no tópico anterior, será utilizada uma API pública como backend da nossa aplicação.

## Criação do Model

Ao executar a API [Dogs](https://dog.ceo) em seu navegador identificamos um padrão no objeto de retorno que consiste, basicamente, um objeto com duas propriedades - **status** e **Message**.

Conforme o objeto JSON abaixo:
```
{
    "status": "<valor>,
    "message": any // qualquer coisa
}
```

A propriedade **message** é um tipo genérico, podendo conter uma lista de strings, um objeto, ou simplesmente uma string. O seu valor de retorno depende do endpoint executado e também do comportamento obtido na execução(sucesso ou falha).

Será abstraído um model para encapsular os retornos da **Dog API**.

Clique com o botão direito do mouse em seu projeto **HOMvvmCross.Core** e em seguida:

Add -> New Folder

Defina a pasta com o nome **Models**.

Clique com o botão direito do mouse na pasta criada e em seguida:

Add -> Class ...

Defina o nome da classe para **DogApiObjectReturn**.

Abra a classe criada e insira o seguinte código:

```
using Newtonsoft.Json;

public class DogApiObjectReturn<TMessage>
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        public bool Success
        {
            get { return Status.ToLower() == "success"; }
        }

        [JsonProperty("message")]
        public TMessage Message
        {
            get; set;
        }
    }

```

A utilização do atributo ou annotation **JsonProperty** (pacote Newtonsoft.Json) é para informar ao "desserializador" qual propriedade do objeto Json será atribuída à propriedade do nosso objeto Model.

## Services

Os serviços para consumo da API serão distribuídos em 3 aqruivos, basicamente:
- IDogApiService.cs - interface
- DogApiService.cs - classe
- IDogBreedApi.cs - interfaces

Clique com o botão direito do mouse sobre o projeto Core e, em seguida:

Add -> New Folder

Defina o nome da pasta para **Service**.

Clique com o botão direito do mouse sobre a pasta **Service** e adicione duas novas pastas com os seguintes nomes: **Intercafes** e **RestContract**, respectivamente.

### Interface - DogBreedApi.cs

Clique com o botão direito do mouse sobre a pasta **RestContract** e em seguida:

Add -> Class ..

Defina o nome da classe para **DogBreedApi.cs**.

Esta interface terá como responsabilidade o encapsulamento das chamadas a serem feitas na API. Será um contrato que define métodos anotados com o endereço da API a ser consumida e também o Verbo REST para execução.

Abra a interface criada e adicione o seguinte código como conteúdo do arquivo:

```
using HOMvvmCross.Core.Model;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service.RestContract
{   
    public interface IDogBreedApi
    {
        [Get("/api/breeds/list")]
        Task<DogApiObjectReturn<List<string>>> ListBreeds();

        [Get("/api/breed/{breedName}/images")]
        Task<DogApiObjectReturn<List<string>>> GetImagesOfBreed(string breedName);
    }
}

```

Observe que os dois métodos, ListBreeds e GetImagesOfBreed, estão anotados com com atributos que dizem que será consumido um endereço de API utilizando o verbo **GET**.

Este contrato será utilizado, como padrão, juntamente com objetos disponíveis no pacote REFIT, instalado anteriormente.

### Interface - IDogApiService.cs

Clique com o botão direito do mouse sobre a pasta **Interfaces** e em seguida:

Add -> Class ..

Defina o nome da classe para **IDogApiService.cs**.

Esta interface terá como responsabilidade de ser um contrato de serviço que será utilizado pelas ViewModels da aplicação. Assim, toda a necessidade de consumo de informações pelas ViewModels serão feitas através deste contrato que irá representar uma fachada a quaisquer  chamada direta a serviços fora das "fronteiras" do aplicativo, como consumo de serviços RESTs, entre outros.

Abra a interface criada e adicione o seguinte código como conteúdo do arquivo:

```

using System.Collections.Generic;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service.Interfaces
{
    public interface IDogApiService
    {
        Task<List<string>> ListBreeds();

        Task<List<string>> GetImagesOfBreed(string breedName);
    }
}

```

Coincidentemente os métodos disponíveis nesta interface possuem o mesmo nome dos métodos disponíveis na interface **IDogBreedApi**, porém não existe qualquer relação entre os contratos que determine a similaridade nas assinaturas. Mesmo com as assinaturas idênticas, observamos o tipo de reteorno diferente. Isso é importante para definir o que é de responsabilidade da API e o que é de resposabilidade do consumo de informação em sistemas externos. Havendo a necessidade de refatoração do serviço REST para consumo em outra fonte de informações ou até mesmo um cache local, a refatoração será feita, apenas, na classe que implementa o contrato **IDogApiService**, de forma que a aplicação que consome somente a interface, muitas vezes, não sofrerá qualquer alteração.

### Classe - DogApiService.cs

Agora que definimos os contratos que serão utilizados para consumo de um recurso externo através do protocolo REST e para consumo na aplicação, devemos criar a classe para executar, de fato, o trabalho "pesado".

Clique com o botão direito do mouse sobre a pasta **Service** e em seguida:

Add -> Class ..

Defina o nome da classe para **DogApiService.cs**.

Abra a classe criada e adicione o seguinte código como conteúdo do arquivo:

```

using HOMvvmCross.Core.Service.Interfaces;
using HOMvvmCross.Core.Service.RestContract;
using Refit;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service
{
    public class DogApiService : IDogApiService
    {
        private const string BASE_URL = "https://dog.ceo";

        private IDogBreedApi _restService;

        public DogApiService()
        {
            _restService = RestService.For<IDogBreedApi>(BASE_URL);
        }
        
        public async Task<List<string>> GetImagesOfBreed(string breedName)
        {
            
            List<string> breedsImages = new List<string>();

            try
            {
                var breedsMessage = await _restService.GetImagesOfBreed(breedName);

                if (breedsMessage.Success && breedsMessage.Message != null)
                {
                    breedsImages = breedsMessage.Message;
                }
            }
            catch (Exception ex)
            {
                WriteConsole(ex);
            }

            return breedsImages;
        }

        public async Task<List<string>> ListBreeds()
        {
            List<string> breeds = new List<string>();

            try
            {
                var breedsMessage = await _restService.ListBreeds();

                if (breedsMessage.Success && breedsMessage.Message != null)
                {
                    breeds = breedsMessage.Message;
                }
            }
            catch(Exception ex)
            {
                WriteConsole(ex);
            }

            return breeds;
        }

        private void WriteConsole(Exception ex, [CallerMemberName]string methodName = "")
        {
            System.Diagnostics.Debug.WriteLine($"ERRO - {methodName} {ex.Message}");
        }
    }
}


```

Esta classe possui implementações básicas, para fins didáticos. Porém, o seu conteúdo pode ser utilizado em um sistema real com algumas alterações, como por exemplo a verificação de internet e melhor tratamento de erros. Mas para o exemplo proposto, a implementação atende perfeitamente.

Observe a utilização da classe **RestService**, provida pelo pacote do Refit. Sua documentação poderá ser consultada clicando [aqui](https://github.com/paulcbetts/refit).

# Implementação das ViewModels

Até o momento todo o trabaho feito foi, praticamente, na camada **Core**. Toda a infraestrutura necessária para alimentar a aplicação já foi implementada. Neste momento será necessária a implementação de duas novas classes ViewModel. Tais classes serão as controladoras das nossas Views que serão desenvolvidas nas plataformas Androido e IOS.

No projeto **Core**, clique com o botão direito do mouse na pasta ViewModels e adicione duas classes - **BreedsListViewModel.cs** e **BreedImagesViewModel.cs**.

Adicione o seguinte código na classe **BreedsListViewModel.cs**:

```

using HOMvvmCross.Core.Service.Interfaces;
using MvvmCross.Core.ViewModels;
using System;

namespace HOMvvmCross.Core.ViewModels
{
    public class BreedsListViewModel : MvxViewModel
    {
        private MvxObservableCollection<string> _breeds;
        private IDogApiService _dogApiService;

        public MvxObservableCollection<string> Breeds
        {
            get { return _breeds; }        
        }

        private IMvxCommand<string> _breedClick;

        public IMvxCommand<string> BreedClick
        {
            get
            {
                return _breedClick ?? (_breedClick = new MvxCommand<string>(ShowBreedImagesPage));
            }
        }

        private void ShowBreedImagesPage(string breedName)
        {
            ShowViewModel<BreedImagesViewModel>(breedName);
        }


        public BreedsListViewModel(IDogApiService dogApiService)
        {
            _breeds = new MvxObservableCollection<string>();
            _dogApiService = dogApiService;
        }

        public override async void Start()
        {
            base.Start();

            try
            {

                var models = await _dogApiService.ListBreeds();

                Breeds.AddRange(models);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERRO - Carregar listagem - {ex.Message}");
            }

        }

    }
}


```

Uma ViewModel é, basicamente, uma controladora. Ela não leva em consideração algum elemento ou objeto de tela. na verdade a ViewModel possui atributos e comportamentos. A classe acima possui um atributo chamado **Breeds** que é, na verdade, uma lista de raças. Além disso possui um comportamento, **BreedClick**, que é a ação a ser executada ao selecionar uma raça.

Adicione o seguinte código na classe **BreedImagesViewModel.cs**:

```

using HOMvvmCross.Core.Service.Interfaces;
using MvvmCross.Core.ViewModels;
using System;

namespace HOMvvmCross.Core.ViewModels
{
    public class BreedImagesViewModel : MvxViewModel
    {
        private MvxObservableCollection<string> _images;
        private IDogApiService _dogApiService;

        public MvxObservableCollection<string> Images
        {
            get { return _images; }
        }

        private string _breedName;

        public void Init(string breedName)
        {
            _breedName = breedName;
        }

        public BreedImagesViewModel(IDogApiService dogApiService)
        {
            _images = new MvxObservableCollection<string>();
            _dogApiService = dogApiService;
        }

        public override async void Start()
        {
            base.Start();

            try
            {

                var models = await _dogApiService.GetImagesOfBreed(_breedName);

                Images.AddRange(models);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERRO - Carregar imagens - {ex.Message}");
            }

        }
    }
}


```

A ViewModel criada acima possui uma diferença em relação à criada anteriormente. Observe o método **Init**. Ele recebe como parâmetro uma string representando uma raça. O framework MVVMCross possui um padrâo de navegação que é utilizado ao navegar entre as "ViewModels" através do método **ShowViewModel**. Os parâmetros informados neste método serão atribuídos, respectivamente, aos parâmetros definidos no métod **Init** da ViewModel na qual será exibida. É uma convenção. Para maiores detalhes, consultar o seguinte link: [Api Lifecycle](https://www.mvvmcross.com/documentation/fundamentals/app-lifecycle).

# Criando as telas no projeto Android

EM BREVE

# Criando as telas no projeto IOS

EM BREVE