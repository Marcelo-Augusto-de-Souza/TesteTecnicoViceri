# TesteTecnicoViceri

> Projeto desenvolvido para o Teste Tecnico do Processo Seletivo de Desenvolvedor Fullstack Trainee/Junior para a Viceri SEIDOR.

## Tecnologias Utilizadas
- .NET 8.0
- Angular 17
- Entity Framework Core
- SQL Server
- Swashbuckle

## Funcionalidades
- Listagem de super-heróis
- Busca de super-herói por ID
- Adição de super-herói
- Atualização de super-herói
- Remoção de super-herói

## Como Executar
1. Clone o repositório
```bash
git clone https://github.com/Marcelo-Augusto-de-Souza/TesteTecnicoViceri.git
```
2. Execute o backend:
```bash
cd backend
dotnet run
```
3. Execute o frontend:
```bash
cd frontend
ng serve
```

## Endpoints
- GET /lista_superherois
- GET /lista_heroi/{id}
- POST /adiciona_superheroi
- PUT /atualiza_heroi/{id}
- DELETE /deletar_heroi/{id}

## Documentação
- Backend-Swagger: http://localhost:5120/swagger
- Frontend: http://localhost:4200

## Motivos das decisões de implementação
- Foi decidido utilizar minimal API para o backend em vez de uma API tradicional, pois por ser um projeto simples e de baixa escala com tempo de desenvolvimento limitado, a minimal API foi a melhor opção para o backend em comparação com uma API no modelo MVC.
- Foi decidido utilizar SQL Server para o banco de dados, pois é uma opção simples e eficiente para um projeto de baixa escala, além de motivos didaticos.

## Dificuldades encontradas ao longo do desenvolvimento
- Foi encontrada significativa dificuldade na implementação do backend utilizando o Entity Framework Core e o SQL Server pois foi necessario um tempo maior para aprender e implementar as funcionalidades, além de tratar os erros vistos pela primeira vez, que acarretaram em uma dificuldade significantemente maior de implementação, devido a necessidade de buscas relativamente complexas utilizando o Entity Framework Core, relacionado as especificações do SQL Server.
- Foi encontrada tambem uma dificuldade na implementação do frontend utilizando o Angular 17, devido ao tempo limitado de desenvolvimento e a necessidade de revisar conceitos anteriores do Angular a tempos relevantes não utilizados.


## Agradecimentos
- De antemão agradeço a Viceri SEIDOR por me dar a oportunidade de participar do processo seletivo de Desenvolvedor Fullstack Trainee/Junior.

## Contato
- Nome: Marcelo Augusto de Souza
- Email: ademarjsantana49@gmail.com
- LinkedIn: https://www.linkedin.com/in/marcelo-augusto-9182a021a/
