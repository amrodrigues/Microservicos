Projeto de Microserviços de Vendas e Estoque
Este é um projeto de arquitetura de microserviços, composto por dois serviços principais: Vendas e Estoque. A comunicação entre eles é feita usando requisições HTTP para verificação de estoque e um sistema de mensageria RabbitMQ para a notificação de atualizações.

Tecnologias e Ferramentas
.NET 6.0: Framework para construir as APIs.

Entity Framework Core: ORM (Object-Relational Mapper) para o acesso a banco de dados.

SQL Server: Banco de dados relacional para persistir os dados.

RabbitMQ: Ferramenta de mensageria para comunicação assíncrona entre os serviços.

Swagger/OpenAPI: Para documentação e teste interativo das APIs.

Identity: Para o gerenciamento de usuários e autenticação via JWT.

Estrutura do Projeto
O projeto é organizado em duas pastas de projeto, cada uma com sua própria API e responsabilidades:

Vendas: Serviço responsável por gerenciar os pedidos de venda.

VendasController: Controla o fluxo de criação e consulta de pedidos.

AuthController: Gerencia o registro e login de usuários, usando autenticação JWT.

VendasDbContext: Contexto do banco de dados para os pedidos, integrado com Identity para dados de usuários.

Estoque: Serviço responsável por gerenciar o estoque de produtos.

ProdutosController: Oferece endpoints para a consulta e atualização do estoque.

EstoqueDbContext: Contexto do banco de dados para os produtos.

Worker: Um serviço em segundo plano que escuta mensagens do RabbitMQ para atualizar o estoque após um pedido.

Configuração e Execução
Pré-requisitos
Certifique-se de ter instalado:

.NET 6 SDK

SQL Server

RabbitMQ Server: Em execução em localhost.

Ferramenta dotnet-ef: Para migrações do Entity Framework (dotnet tool install --global dotnet-ef).

Strings de Conexão
Ambos os projetos possuem strings de conexão em seus arquivos appsettings.json. Certifique-se de que estão configuradas para apontar para suas instâncias do SQL Server. Os nomes dos bancos de dados são VendasIdentityDb e EstoqueDb.

Migrações de Banco de Dados
Para cada projeto, abra um terminal na respectiva pasta e execute os comandos para criar e aplicar as migrações:

Para o projeto Vendas:

dotnet ef migrations add InitialVendas --project .\src\VendasService\
dotnet ef database update --project .\src\VendasService\

Para o projeto Estoque:

dotnet ef migrations add InitialEstoque --project .\src\EstoqueService\
dotnet ef database update --project .\src\EstoqueService\

Note: Você precisa ter a solução aberta no Visual Studio ou usar a flag --startup-project para que o EF encontre o projeto principal.

Executar a Aplicação
Para executar todos os projetos, use o Visual Studio ou a linha de comando. No terminal, execute dotnet run em cada uma das pastas do projeto (Vendas e Estoque).

Endpoints da API
Vendas: https://localhost:5001/swagger

Estoque: https://localhost:5062/swagger

Use o Swagger UI para testar os endpoints de cada serviço.