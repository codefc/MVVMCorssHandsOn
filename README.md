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