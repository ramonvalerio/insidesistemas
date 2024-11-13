# insidesistemas
Dev Back-end

## 📚 Descrição
InsideSistemas.Api é uma aplicação backend desenvolvida com ASP.NET Core .NET 8 para gerenciamento de pedidos e produtos. A aplicação inclui Web API para operações principais e integração opcional com GraphQL usando Hot Chocolate.

## 🚀 Tecnologias Utilizadas
- **ASP.NET Core .NET 8 (Web API)**
- **Entity Framework Core (InMemory)**
- **GraphQL (Hot Chocolate)**
- **InMemory Database** (armazenamento temporário para desenvolvimento e testes)

## 📂 Estrutura do Projeto
![Arquitetura do Projeto](images/estrutura_do_projeto.jpg)
- **InsideSistemas.Api**: Contém a configuração da API, os controladores e os resolvers de GraphQL. Esta camada é responsável por expor os endpoints da API e serve como a interface de entrada para o sistema. Ela tem acesso apenas à camada **Application**.

- **InsideSistemas.Application**: Contém os serviços, a lógica de negócio e os modelos de resposta. Essa camada coordena o fluxo de dados entre a API e as outras camadas, aplicando regras de negócio conforme necessário. Ela tem acesso às camadas **Domain** e **Infrastructure** para manipular dados e aplicar lógica de negócios.

- **InsideSistemas.Infrastructure**: Contém a implementação dos repositórios usando o Entity Framework InMemory, gerenciando o acesso aos dados. Esta camada se comunica apenas com a camada **Domain** para lidar com operações de persistência e manipulação de dados.

- **InsideSistemas.Domain**: Contém toda regra de negócio, é uma camada pura e isolada pois não conhece as demais camadas. Apenas objetos de domínio e interface dos repositórios.

## ⚙️ Configuração e Execução
1. Clone o repositório:
   ```bash
   git clone https://github.com/ramonvalerio/insidesistemas.git
   ```
2. Execute a aplicação:
	```bash
	dotnet run
	```
3. A API poderá ser acessada utilizando Swagger neste link ```bash https://localhost:7165/swagger/index.html``` (ou na porta configurada).
GraphQL(extra ainda em desenvolvimento) poderá ser acessada neste link ```bash https://localhost:7165/graphql/```

## 📑 Endpoints Principais (Web API)

Pedidos
POST ```bash /api/pedidos```: Inicia um novo pedido.
PUT ```bash /api/pedidos/{id}/produtos```: Adiciona um produto ao pedido.
DELETE ```bash /api/pedidos/{id}/produtos/{produtoId}```: Remove um produto do pedido.
PUT ```bash /api/pedidos/{id}/fechar```: Fecha o pedido.
GET ```bash /api/pedidos/{id}```: Retorna um pedido específico por ID.
GET ```bash /api/pedidos```: Lista todos os pedidos.
GET ```bash /api/pedidos/status?status={status}&pageNumber={pageNumber}&pageSize={pageSize}```: Lista pedidos filtrados por status com paginação.

## 🔍 Funcionalidades Extras (GraphQL)
GraphQL está disponível como uma opção adicional para realizar consultas e mutações de dados.
Endpoints do GraphQL estão configurados para facilitar a interação com pedidos e produtos.
Obs.: Porém ainda não está 100% devido a um problema com incompatibilidade devido a natureza do DbContext do EntityFramework Core não lidar com as chamadas paralelas do GraphQL (Hot Chocolate).
https://chillicream.com/docs/hotchocolate/v14/integrations/entity-framework

## 🛠️ Problemas Conhecidos
A base de dados InMemory é volátil e será reiniciada cada vez que a aplicação for reiniciada. Isso é esperado para ambientes de desenvolvimento e teste.

## 📄 Licença
Este projeto está licenciado sob a MIT License.



