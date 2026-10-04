# CIEE - Cadastro de Currículos | Backend

Backend desenvolvido em ASP.NET Core com SQL Server para cadastro e consulta de currículos e leitura de dados de arquivos PDF.

## Tecnologias

- C#
- ASP.NET Core 10
- .NET 10
- Entity Framework Core
- SQL Server
- PdfPig

---

## Como executar

### 1. Clonar o projeto

```bash
git clone <URL_DO_REPOSITORIO>
cd CIEE-processo-seletivo-BACKEND
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Criar o banco de dados

No SQL Server, execute:

```sql
CREATE DATABASE CIEE_Curriculos;
GO

USE CIEE_Curriculos;
GO
```

### 4. Criar a tabela

```sql
CREATE TABLE Curriculos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NomeCompleto NVARCHAR(150) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Telefone NVARCHAR(20) NULL,
    AreaInteresse NVARCHAR(150) NULL,
    ResumoProfissional NVARCHAR(1000) NULL,
    DataCriacao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    DataAtualizacao DATETIME2 NULL
);
GO
```

### 5. Configurar a conexão

No arquivo `appsettings.json`, configure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CIEE_Curriculos;Trusted_Connection=True;TrustServerCertificate=True;"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "AllowedHosts": "*"
}
```

A configuração acima considera que o SQL Server está rodando localmente na máquina e utilizando a instância padrão.

Caso esteja usando outra instância ou servidor, altere o valor de `Server`.

Exemplos:

```text
Server=localhost
```

```text
Server=localhost\SQLEXPRESS
```

```text
Server=NOME_DO_SERVIDOR
```

### 6. Compilar

```bash
dotnet build
```

### 7. Executar

```bash
dotnet run
```

A API será iniciada na URL exibida no terminal.

Exemplo:

```text
http://localhost:5034
```

---

## Endpoints

### Listar currículos

```http
GET /api/curriculos
```

### Buscar currículo por ID

```http
GET /api/curriculos?id=1
```

### Cadastrar currículo

```http
POST /api/curriculos
```

Body:

```json
{
  "nomeCompleto": "João da Silva",
  "email": "joao@email.com",
  "telefone": "(41) 99999-9999",
  "areaInteresse": "Desenvolvimento de Software",
  "resumoProfissional": "Desenvolvedor de software."
}
```

### Ler dados de currículo em PDF

```http
POST /api/curriculos/ler-pdf
```

Enviar como:

```text
multipart/form-data
```

utilizando a chave:

```text
arquivo
```

O arquivo deve ser PDF e possuir no máximo 5 MB.

Resposta esperada:

```json
{
  "nomeCompleto": "João da Silva",
  "email": "joao@email.com",
  "telefone": "(41) 99999-9999"
}
```