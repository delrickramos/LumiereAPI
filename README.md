# 🎬 Lumiere API

API REST para gerenciamento de cinema, incluindo controle de filmes, salas, sessões e venda de ingressos.

## 📋 Sobre o Projeto

Lumiere é uma API desenvolvida em .NET 10 que implementa um sistema completo de gerenciamento de cinema, permitindo o controle de:
- Filmes e seus gêneros
- Salas de exibição com assentos
- Sessões de filmes
- Venda de ingressos com diferentes tipos e preços
- Relatórios de ocupação

## ⚙️ Tecnologias Utilizadas

- **.NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **Swagger/OpenAPI** para documentação
- **Newtonsoft.Json** para serialização

## 🗄️ Estrutura do Banco de Dados

### Tabelas Principais

**Filmes**
- Id, Titulo, DuracaoMinutos, ClassificacaoIndicativa, Sinopse, Direcao, Distribuidora, GeneroId

**Salas**
- Id, Nome, NumeroLinhas, NumeroColunas, Capacidade

**Sessoes**
- Id, FilmeId (FK), SalaId (FK), FormatoSessaoId (FK), DataHoraInicio, DataHoraFim, Idioma, PrecoBase

**Ingressos**
- Id, SessaoId (FK), AssentoId (FK), TipoIngressoId (FK), PrecoFinal, Status

**Assentos**
- Id, SalaId (FK), Fileira, Coluna, Nome, TipoAssento

**Generos** (Dados Fixos)
- Ação, Comédia, Drama, Terror, Ficção, Animação

**FormatosSessao** (Dados Fixos)
- 2D, 3D, IMAX

**TiposIngresso** (Dados Fixos)
- Inteira (0%), Meia (50%), Estudante (50%), Idoso (50%), Criança (30%)

## 🎯 Requisitos Funcionais Implementados

### 1. Agendamento de Sessões
- ✅ Não permite criar sessão em horário conflitante na mesma sala
- ✅ Validação de interseção de horários entre sessões
- ✅ Cálculo automático do horário de término baseado na duração do filme

### 2. Cartaz de Filmes
- ✅ Endpoint `/api/filmes/em-cartaz` lista filmes com sessões nos próximos 7 dias
- ✅ Consulta otimizada com LINQ/Entity Framework

### 3. Relatório de Ocupação
- ✅ Endpoint `/api/relatorios/salas/ocupacao` retorna taxa de ocupação das salas
- ✅ Cálculo: (ingressos vendidos / capacidade) * 100

### 4. Gestão de Assentos
- ✅ Geração automática de assentos ao criar sala
- ✅ Tipos especiais: Normal, Cadeirante, Obeso
- ✅ Nomenclatura em letras e números (A1, B5, etc)

### 5. Venda de Ingressos
- ✅ Validação de assento disponível
- ✅ Cálculo automático de preço com desconto
- ✅ Bloqueio de venda 30 minutos antes da sessão

## 🚀 Como Executar

### Pré-requisitos
- .NET 10 SDK
- SQL Server (LocalDB ou instância completa)
- Visual Studio 2022 ou Visual Studio Code

### Configuração

1. **Clone o repositório**
```bash
git clone https://github.com/delrickramos/LumiereAPI.git
cd LumiereAPI
```

2. **Configure a connection string**

Edite `Lumiere.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Lumiere": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Lumiere;Integrated Security=True;"
  }
}
```

3. **Execute as migrations**
```bash
cd Lumiere.API
dotnet ef database update
```

4. **Execute a aplicação**
```bash
dotnet run
```

5. **Acesse o Swagger**
```
https://localhost:5001/swagger
```

### Scripts SQL (Opcional)

Para facilitar os testes, o projeto inclui scripts SQL para popular o banco com dados aleatórios. Os scripts estão localizados em `Lumiere.API/Database/Scripts/DadosAleatorios/` e devem ser executados na seguinte ordem:

1. `01_filmes.sql` - Insere 10 filmes aleatórios
2. `02_salas.sql` - Insere 10 salas com capacidades variadas
3. `03_assentos.sql` - Gera assentos automaticamente para as salas
4. `04_sessoes.sql` - Cria sessões futuras para os filmes
5. `05_ingressos.sql` - Simula venda de ingressos para as sessões

**Como executar:**
- Via SQL Server Management Studio (SSMS): Abra cada arquivo e execute contra o banco `Lumiere`
- Via linha de comando:
```bash
sqlcmd -S (localdb)\MSSQLLocalDB -d Lumiere -i "Lumiere.API\Database\Scripts\DadosAleatorios\01_filmes.sql"
sqlcmd -S (localdb)\MSSQLLocalDB -d Lumiere -i "Lumiere.API\Database\Scripts\DadosAleatorios\02_salas.sql"
# ... e assim por diante
```

> **Nota:** Execute os scripts na ordem correta para respeitar as dependências de chaves estrangeiras.

## 📡 Endpoints Principais

### Filmes
- `GET /api/filmes` - Lista todos os filmes
- `GET /api/filmes/{id}` - Busca filme por ID
- `GET /api/filmes/em-cartaz` - Filmes em cartaz (próximos 7 dias)
- `POST /api/filmes` - Cria novo filme
- `PUT /api/filmes/{id}` - Atualiza filme
- `DELETE /api/filmes/{id}` - Remove filme

### Sessões
- `GET /api/sessoes` - Lista todas as sessões
- `GET /api/sessoes/{id}` - Busca sessão por ID
- `POST /api/sessoes` - Cria nova sessão
- `PUT /api/sessoes/{id}` - Atualiza sessão
- `DELETE /api/sessoes/{id}` - Remove sessão

### Salas
- `GET /api/salas` - Lista todas as salas
- `GET /api/salas/{id}` - Busca sala por ID
- `POST /api/salas` - Cria nova sala (gera assentos automaticamente)
- `PUT /api/salas/{id}` - Atualiza sala
- `DELETE /api/salas/{id}` - Remove sala

### Ingressos
- `GET /api/ingressos` - Lista todos os ingressos
- `GET /api/ingressos/{id}` - Busca ingresso por ID
- `GET /api/ingressos/sessao/{sessaoId}` - Lista ingressos de uma sessão
- `POST /api/ingressos` - Vende ingresso

### Relatórios
- `GET /api/relatorios/salas/ocupacao` - Taxa de ocupação das salas

### Dados Auxiliares
- `GET /api/generos` - Lista gêneros
- `GET /api/formatos-sessao` - Lista formatos de sessão
- `GET /api/tipos-ingresso` - Lista tipos de ingresso
- `GET /api/assentos` - Lista assentos

## 📁 Estrutura do Projeto

```
Lumiere/
├── Lumiere.Models/              # Camada de modelos de domínio
│   ├── Filme.cs
│   ├── Sala.cs
│   ├── Sessao.cs
│   ├── Ingresso.cs
│   ├── Assento.cs
│   └── ...
│
├── Lumiere.API/                 # Camada de API
│   ├── Controllers/             # Controllers HTTP
│   │   ├── FilmesController.cs
│   │   ├── SessoesController.cs
│   │   ├── SalasController.cs
│   │   ├── IngressosController.cs
│   │   └── RelatoriosController.cs
│   │
│   ├── Services/                # Lógica de negócio
│   │   ├── FilmeService.cs
│   │   ├── SessaoService.cs
│   │   ├── SalaService.cs
│   │   └── IngressoService.cs
│   │
│   ├── Repository/              # Acesso a dados
│   │   ├── FilmeRepository.cs
│   │   ├── SessaoRepository.cs
│   │   ├── SalaRepository.cs
│   │   ├── IngressoRepository.cs
│   │   └── RelatorioRepository.cs
│   │
│   ├── Interfaces/              # Contratos de serviços e repositórios
│   ├── Dtos/                    # Data Transfer Objects
│   ├── Mappers/                 # Mapeamento entre Models e DTOs
│   ├── Database/                # Contexto do Entity Framework
│   │   └── LumiereContext.cs
│   ├── Common/                  # Classes auxiliares
│   │   └── ServiceResult.cs
│   └── Migrations/              # Migrations do EF Core
```

## 🔒 Regras de Negócio

### Sessões
- Não permite criar sessão no passado
- Não permite horários conflitantes na mesma sala
- Não permite atualizar/excluir sessão com ingressos vendidos
- DataHoraFim calculado automaticamente

### Ingressos
- Assento deve pertencer à sala da sessão
- Não permite vender ingresso duplicado para mesmo assento/sessão
- Bloqueio de venda 30 minutos antes da sessão
- Preço calculado: PrecoBase * (1 - DescontoPercentual/100)

### Salas
- Assentos gerados automaticamente ao criar sala
- Primeira fileira: assentos para cadeirantes
- Cantos da primeira e última fileira: assentos para obesos
- Não permite excluir sala com sessões vinculadas

### Filmes
- Título único (case insensitive)
- Não permite excluir filme com sessões vinculadas

## 🏗️ Padrões Arquiteturais

- **Repository Pattern**: Abstração de acesso a dados
- **Service Layer**: Lógica de negócio isolada
- **DTO Pattern**: Separação entre modelos de domínio e transporte
- **Dependency Injection**: Inversão de controle nativa do ASP.NET Core
- **Result Pattern**: Padronização de retornos com ServiceResult

## 📝 Exemplo de Uso

### Criar uma Sessão
```json
POST /api/sessoes
{
  "filmeId": 1,
  "salaId": 2,
  "formatoSessaoId": 1,
  "dataHoraInicio": "2026-02-15T19:30:00-03:00",
  "idioma": "Português",
  "precoBase": 35.00
}
```

### Vender um Ingresso
```json
POST /api/ingressos
{
  "sessaoId": 5,
  "assentoId": 12,
  "tipoIngressoId": 2
}
```

### Consultar Filmes em Cartaz
```http
GET /api/filmes/em-cartaz
```

### Consultar Taxa de Ocupação
```http
GET /api/relatorios/salas/ocupacao
```

## 👨‍💻 Autor

Desenvolvido por Gabriela Otte e Délrick Ramos como projeto final do Programa de
Captação e Capacitação Tecnológica em Desenvolvimento de Aplicações.
Programa este promovido pela PUCPR em parceria com o grupo VOLVO para
aprendizado de .NET, SQL Server e Entity Framework Core.

## 📄 Licença

Este projeto é de código aberto e está disponível para fins educacionais.

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!
