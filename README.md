# MVVMCorssHandsOn

Hands On onde desenvolvemos uma pequena aplicação em Xamarin utilizando a abordagem tradicional e o Framework MVVM Cross.

## Criação do Projeto

Primeiramente devemos criar uma solução para conter nossos projetos.

### Criar uma solution:

File -> New -> Project -> Other Projects Types -> Visual Studio Solutions

Em "Name" Informe o nome "HOMvvmCross"

### Criando o projeto "CORE"

Assim como na abordagem do Xamarin.Forms, vamos criar um projeto Core que para concentrar tudo o que será comum em relação a nossa aplicação para as plataformas.

Na solution, clique com o botão direito do mouse e selecione:

Add -> New Project -> Cross-Platform -> Class Library

Defina o nome para "HOMvvmCross.Core"

* Se apareceu somente a opção  "Class Library (Xamarin.Forms)", pode criar o projeto com esta opçao e após a criação, exclui a classe padrão que foi criada e também remova o pacote Nuget Xamarin.Forms.

Após a criação do projeto, vamos adicionar o pacote do MVVMCross.

Clique com o botão direito do mouse sobre o projeto e em seguida em "Manage Nuget Packages".

Em "Browse" informe: MVVMCross Starter Pack

Clique em Install

### Criando o projeto "Android"

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


### Criando o projeto "IOS"

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

**EM DESENVOLVIMENTO** conclusão no dia 05/11/2017