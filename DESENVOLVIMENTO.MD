# DESENVOLVIMENTO

## Registro do desenvolvimento e uso de IA

O desenvolvimento do projeto foi organizado em etapas menores, com o objetivo de reduzir a complexidade e validar cada parte antes de avançar para a próxima.

A estratégia principal foi construir a solução de forma incremental, começando pelo ambiente e pelas funcionalidades mais simples, e só depois integrar frontend, backend, banco de dados e leitura de PDF.

---

## 1. Organização do desenvolvimento

O trabalho foi dividido nas seguintes etapas.

### Etapa 1 - Configuração do ambiente

O primeiro passo foi preparar o ambiente de desenvolvimento e instalar/configurar as tecnologias necessárias para o projeto:

- Angular
- Node.js e npm
- .NET 10
- ASP.NET Core
- SQL Server 2025
- SQL Server Management Studio
- Entity Framework Core

---

### Etapa 2 - Desenvolvimento inicial do frontend

Antes de desenvolver o backend, foi criado um frontend simples em Angular contendo apenas os campos solicitados no documento do desafio:

- Nome completo
- E-mail
- Telefone
- Área ou cargo de interesse
- Resumo profissional

Nesse primeiro momento o objetivo era apenas criar a estrutura inicial do formulário e validar a organização dos componentes.

---

### Etapa 3 - Criação do banco de dados

Depois do frontend inicial, foi criada a estrutura do banco de dados utilizando SSMS.

Foi criado o banco:

```text
CIEE_Curriculos
```

e a tabela:

```text
Curriculos
```

com os campos necessários para armazenar as informações do formulário.

---

### Etapa 4 - Desenvolvimento das APIs

Com o banco configurado, o próximo passo foi desenvolver os endpoints principais da API em ASP.NET Core.

Inicialmente os endpoints foram desenvolvidos sem regras de negócio ou validações.

Foram implementadas operações para:

- cadastrar currículo;
- listar currículos;
- consultar currículo por ID.

Antes de qualquer integração com o frontend, todos os endpoints foram testados individualmente utilizando o Postman.

Somente após confirmar que as APIs estavam funcionando corretamente e persistindo os dados no banco de dados foi iniciada a integração com o frontend.

---

### Etapa 5 - Integração entre frontend e backend

Depois que os endpoints estavam funcionando corretamente no Postman, o frontend Angular foi integrado à API ASP.NET Core utilizando `HttpClient`.

Foi criado um service no Angular responsável pelas requisições HTTP.

A partir desse momento, os dados preenchidos no formulário passaram a ser enviados ao backend e persistidos no SQL Server.

Também foi necessário configurar CORS no backend para permitir a comunicação entre:

```text
Angular
http://localhost:4200
```

e:

```text
ASP.NET Core
http://localhost:5034
```

---

### Etapa 6 - Leitura de currículo em PDF

Após finalizar o fluxo principal de cadastro, foi implementada a funcionalidade de leitura automática do currículo em PDF.

O frontend passou a permitir que o usuário selecione um arquivo PDF.

O arquivo é enviado para um endpoint específico do backend utilizando:

```text
multipart/form-data
```

Para extrair o texto do PDF foi utilizada a biblioteca:

```text
PdfPig
```

---

### Etapa 7 - Extração dos dados do currículo

O requisito dessa etapa era identificar automaticamente apenas:

- Nome completo
- E-mail
- Telefone

Foram utilizadas expressões regulares para identificar e-mail, telefone e nome.

Para o telefone foi criada uma expressão voltada para números brasileiros e considerando os DDDs válidos.

Para validar o formato do e-mail, utilizei a Data Annotation [EmailAddress] do .NET.

A identificação do nome exigiu mais ajustes, porque diferentes PDFs podem retornar o conteúdo em formatos diferentes.

A solução final utiliza o início do documento, os dados de contato encontrados e uma expressão regular compatível com nomes em português.

---

### Etapa 8 - Estilização

Somente depois que o fluxo principal estava funcionando corretamente foi realizada a estilização da interface com css puro.

---

### Etapa 9 - Regras e validações

Depois da integração e estilização foram implementadas as regras da aplicação.

Entre elas:

- Nome completo obrigatório
- E-mail obrigatório
- Validação do formato do e-mail
- Limite de caracteres para o nome
- Arquivo obrigatoriamente em PDF
- Limite máximo de 5 MB para o PDF
- Mensagens de erro retornadas pelo backend e exibidas no frontend

Todas as validaçoes existem tanto no fron-end como no back-end.

---

## 2. Principais decisões técnicas

### Separação entre frontend e backend

O frontend e o backend foram desenvolvidos como projetos independentes.

O Angular é responsável pela interface e interação com o usuário.

O ASP.NET Core é responsável pelas regras, acesso ao banco de dados e processamento do PDF.

A comunicação ocorre através de uma API REST.

---

### SQL Server

O SQL Server foi utilizado como banco de dados relacional conforme solicitado no desafio.

---

### Entity Framework Core

O Entity Framework Core foi utilizado para simplificar o acesso ao SQL Server e permitir que o backend trabalhe com os registros através de objetos.

---

### PdfPig

A biblioteca PdfPig foi utilizada para leitura de arquivos PDF.

Ela foi escolhida com uma alternativa mais simples, sem precisar usar serviços externos

---

### Separação de responsabilidades

Durante o desenvolvimento, procurei seguir princípios de Clean Code e Separation of Concerns, mantendo cada parte da aplicação responsável por uma função específica.

Por esse motivo, a lógica de interpretação dos dados extraídos do currículo foi separada do controller e movida para um service específico:
```CurriculoParserService```


Dessa forma, o controller ficou responsável principalmente pelo fluxo das requisições HTTP, enquanto o service ficou responsável pela extração dos dados

---

## 3. Uso de inteligência artificial

Durante o desenvolvimento foi utilizado apenas o ChatGPT como ferramenta de inteligência artificial.


O uso ocorreu através de conversas sobre o código e a arquitetura da solução, sempre buscando entender o motivo das sugestões.

A IA foi utilizada para:

- como configurar o ambiente na minha maquina
- tirar dúvidas sobre Angular e ASP.NET Core;
- discutir organização de pastas e responsabilidades;
- entender como configurar Entity Framework Core e como usar;
- configurar a conexão com SQL Server;
- revisar endpoints;
- investigar erros;
- criar e ajustar expressões regulares;
- discutir opçoes de como ler de PDFs;
- melhorar tratamento de erros;
- revisar documentação.
- procurar melhorias dentro do codigo.
- estilização do front

---
## 4. Tempo aproximado dedicado



```text
5hrs
```

Esse tempo inclui:

- configuração do ambiente;
- implementação (back, front, banco de dados);
- testes;
- debugging;
- ajustes;
- documentação.

---

## 5. Dificuldades e limitações

A principal dificuldade encontrada durante o desenvolvimento foi a identificação do nome do candidato a partir do texto extraído do PDF.

Foram pesquisadas diferentes abordagens em documentação, fóruns, Stack Overflow e também com ChatGPT, porém nenhuma solução encontrada apresentou resultado 100% confiável para todos os formatos de currículo.


Outra limitação está relacionada ao próprio formato do arquivo. A leitura funciona melhor quando o PDF possui texto real embutido. Caso o arquivo seja composto por imagens, como um PDF gerado a partir de PNG/JPG, seria necessário utilizar OCR para realizar a leitura.


Uma limitação tambem é em relação a leitura de telefone, que funciona apenas para numeros brasileiros, achei mais facil e rapido de implementar assim.

---

## 6. Melhorias com mais tempo

Com mais tempo de desenvolvimento, algumas melhorias que poderiam ser implementadas são:

- testes unitários e de integração automatizados;
- melhoria do parser para diferentes layouts de currículo;
- fallback na leitura do pdf, caso não seja possivel extrair os dados com o ```PdfPig``` utilizar um OCR;
- mensagens visuais mais elaboradas no frontend em vez de `alert`;
- indicador de carregamento durante o processamento do PDF;
- suporte a diferentes formatos de telefone;
- testes com uma variedade maior de modelos de currículo;

---

## Considerações finais

A principal estratégia adotada durante o desafio foi dividir o desenvolvimento em pequenas etapas e validar cada uma antes de avançar.

O frontend inicial foi criado de forma propositalmente simples, sem backend e sem estilização.

Depois foram adicionados banco de dados, APIs, integração, leitura de PDF, regras de negócio e por último a melhoria visual.

O ChatGPT foi utilizado como copiloto durante o processo, principalmente para discussão técnica, explicações e investigação de problemas.

As sugestões foram testadas e adaptadas conforme o comportamento real da aplicação e as necessidades do desafio.