# CRUDPessoas

![image](https://github.com/Gabriel-Sabino/WakeCommerceCRUDProduct/assets/71478369/a04b19fc-7720-45bd-937c-3c8ec00c0de0)

![Version](https://img.shields.io/badge/Version%201.0-8A2BE2)
[![LinkedIn](https://img.shields.io/badge/Linkedin-blue)](https://www.linkedin.com/in/gabriel-sabino1/)
[![Project Status: Development](https://img.shields.io/badge/Project%20Status-Development-orange)](https://github.com/Gabriel-Sabino/CRUDPessoas)
![Commit Activity](https://img.shields.io/github/commit-activity/t/Gabriel-Sabino/CRUDPessoas?color=darkgreen)
![Last Commit](https://img.shields.io/github/last-commit/Gabriel-Sabino/CRUDPessoas?color=yellow)

## Arquitetura e Princípios

CRUDPessoas foi desenvolvido seguindo os princípios da arquitetura limpa, incorporando conceitos avançados como Domain-Driven Design (DDD), SOLID e Programação Orientada a Objetos (POO). A arquitetura é composta por camadas bem definidas, promovendo uma separação clara de responsabilidades e uma estrutura de código altamente modular.

O projeto utiliza amplamente abstrações para desacoplar componentes e promover a reutilização de código. Este design orientado a interfaces permite que cada componente do sistema seja substituível e testável de forma independente, contribuindo para a robustez e escalabilidade do projeto.

Para promover a reutilização de código e facilitar a herança de entidades, o projeto utiliza uma abordagem de entidade genérica. Essa técnica simplifica o desenvolvimento, eliminando a necessidade de repetir código comum e facilitando a extensibilidade do sistema. Com a entidade genérica, novas entidades podem ser facilmente adicionadas e herdar comportamentos comuns, resultando em um código mais limpo e manutenível.

## Code-First e Migrações

CRUDPessoas adota a abordagem Code-First do Entity Framework Core para gerenciar o banco de dados. Isso significa que o modelo de dados é definido por meio de classes de entidade, e o banco de dados é gerado automaticamente a partir dessas classes durante a inicialização do projeto.

Antes de executar o projeto, é crucial garantir que o banco de dados esteja sincronizado com o modelo de dados atualizado. Para isso, utilize as migrações do Entity Framework Core, que permitem gerar e aplicar alterações no banco de dados de forma controlada e consistente.

Para aplicar as migrações e criar ou atualizar o banco de dados, execute o seguinte comando no terminal: `dotnet ef database update`, ou se estiver utilizando o Package Manager `update-database`

Este comando irá aplicar todas as migrações pendentes e garantir que o banco de dados esteja configurado de acordo com o modelo de dados atualizado. Certifique-se de executar este comando sempre que houver alterações no modelo de dados para manter o banco de dados sincronizado e evitar inconsistências.

## Pré-requisitos

Antes de começar, certifique-se de atender aos seguintes requisitos:

- Ter uma IDE compatível com o .NET 6 instalada no seu sistema. Recomendamos o uso do Visual Studio 2019 ou superior, ou o Visual Studio Code com as extensões adequadas para o desenvolvimento .NET.
- Ter o SQL Server Management Studio (SSMS) instalado para gerenciar e realizar as migrations de criação do banco de dados SQL Server utilizado pelo projeto.
- Caso queira testar os EndPoints localmente via Postman, é recomendado baixa-lo tambem

Certifique-se de ter todas essas ferramentas instaladas e configuradas corretamente antes de prosseguir com a configuração e execução do projeto.


## Instalação

Siga estas etapas para instalar e executar o projeto localmente:

1. Clone o repositório em sua máquina local:
   ```bash
   git clone https://github.com/Gabriel-Sabino/CRUDPessoas.git
   ```
   
2. Navegue até o diretório/pasta do Projeto 
   ```bash
   cd seu-repositorio

3. Abra o projeto e atualize DefaultConnection dentro do arquivo appsettings.json em WakeCommerceCRUDProduct.API
   ```bash
   "ConnectionStrings": {
      "DefaultConnection": "Server=YourServer; Database=CRUDPessoas; Integrated Security=True; trustServerCertificate=true;"},
   
   ```
4. Certifique-se de realizar a atualização do Banco de Dados via Migration:
   ```bash
   dotnet ef database update
   ```
   Ou se você estiver utilizando o Package Manager:
   ```bash
   Update-Database
   ```
5. Execute o projeto pelo bash ou se você estiver usando uma IDE como o Visual Studio, basta executar a API do projeto via Swagger.
   ```bash
   dotnet run

## Entre em Contato

Para entrar em contato ou obter mais informações sobre este projeto, sinta-se à vontade para me enviar uma mensagem pelo E-mail:

[![Gmail](https://img.shields.io/badge/Gmail-Message-blue)](mailto:gabrielsabino1505@gmail.com)
