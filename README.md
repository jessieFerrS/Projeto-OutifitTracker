# 👗👔 OutFit Tracker 👖👚
O projeto OutFit Tracker consiste em um software desenvolvido para auxiliar no gerenciamento de vendas condicionais, também conhecida como consignação, "malinha" ou bag delivery. Esse modelo de venda consiste em o cliente receber no conforto da sua casa ou levar para sua casa produtos para que possa provar com mais calma e posteriormente devolver as peças que não deseja ou efetuar a compra das que se interessou. Pensando em melhorar o gerenciamento desse sistema e buscando estratégias para otimizar esse modelo de venda de modo que permitisse o acompanhamento de possíveis vendas e o controle de entrada e saída dessas vendas para melhor gerenciamento de estoque e fidelização de clientes e vendas é que desenvolvemos esse sistema como projeto final para a disciplina de Paradigmas de Linguagem de Programação da 6ª Etapa do Curso de Ciência da Computação, cujo o objetivo consistia em escolher um dos paradigmas apresentados em aula e criar um software baseado nesse paradigma.

## Escolha dos Paradigmas
Primeiramente nosso grupo pensou na aplicação, para posteriormente com base no que queriamos criar podermos escolher qual paradigma seria o ideal. Assim, uma vez pensado no projeto partimos para a escolha dos paradigmas, e vimos que nosso sistema seria um sistema multiparadigma, isto é, utilizaria mais de um paradigma de linguagem de programação em sua constituição.

### Paradigmas do Backend
Para o backend da nossa aplicação utilizamos o `Paradigma Funcional` e o `Paradigma Orientado a Objetos`, uma vez que o backend foi feito utilizando **C# com .NET** e o banco de dados com **MySQL**. Essa escolha foi motivada primeiramente porque o paradigma funcional é especialmente útil para lidar com coleções de dados e consultas, aumentando a expressividade e reduzindo a verbosidade do código. Além disso, utilizar o paradigma orientado a objetos no backend, permitiu uma maior organização e estruturação do código, ao utilizar classes e herança; o encapsulamento dos dados garantindo sua proteção e acesso controlados e uma maior flexibilidade e extensibilidade na implementação de funcionalidades graças ao polimorfismo.

### Paradigmas do Frontend
Para o frontend da aplicação como optamos por utilizar o framework **Vue.js**, sendo aplicado mais precisamente o **Vuetify 3**, utilizamos o `Paradigma Declarativo`, onde descreve "o que" se quer que a interface faça, sem detalhar "como" o comportamento deve ser implementado, o que facilita a manutenção da interface e torna o código mais legível e expressivo. Utilizamos também o `Paradigma Component-Based` que promove a modularidade da aplicação ao dividir a interface em blocos independentes, a reutilização do código permitindo o uso de componentes em diferentes partes da aplicação e também a separação de procupações, pois cada componente é responsável apenas pela sua funcionalidade e apresentação. E por fim o `Paradigma Baseado em Eventos`, responsável pelas interações do usuário como cliques de botões, envios de formulários e requisições HTTP, que são capturadas e tratadas com listeners (@click, @input), proporcionando uma experiência dinâmica e responsiva. Além disso, os métodos do ciclo de vida como `mounted` e `updated`, são "ganchos"(hooks) que o Vue.js disponibiliza para reagir a eventos específicos na existência de um componente. O ciclo de vida dos componentes no Vue.js permite reagir a eventos relevantes no fluxo da aplicação, como a criação, a atualização ou destruição de um componente. Essa capacidade é diretamente ligada ao paradigma baseado em eventos, pois eventos como "montar no DOM" ou "atualizar o estado" são tratados de forma explícita no código, possibilitando maior controle e dinamismo no comportamento da aplicação.

## 🛠️ Tecnologias Utilizadas
### 🖥️ Frontend
No frontend da nossa aplicação optamos por utilizar o **Vuetify 3**, um framework UI construído sobre o **Vue.js** que oferece uma série de componentes pré-construídos e personalizaveis, o que permitiu acelerar significativamente o desenvolvimento das interfaces de usuário de forma responsiva. Além disso, por ser baseado no **Material Design** do Google, o Vuetify 3 garantiu a criação de interfaces intuitivas aos usuários.

Optar por essa tecnologia nos ofereceu uma maior produtividade porque acelerou o desenvolvimento de interfaces de usuários um pouco mais complexas; uma maior flexibilidade, pois permitiu a criação de interfaces personalizadas e adaptadas às necessidades da nossa aplicação; a facilidade de manutenção do código e adição de possíveis novas funcionalidades.

### 🏗️ Backend
No caso do backend a combinação de **C# .NET** e **MySQL** ofereceu a nossa aplicação uma base sólida para o desenvolvimento de uma aplicação web robusta e escalável. Ao escolher essa tecnologia aproveitamos diversas vantagens, tanto em termos de desenvolvimento quanto de performance e manutenção, pois o como o C# é uma linguagem fortemente tipada, isso ajuda a prevenit erros comuns em tempo de execução e torna o código mais confiável, aém disso o C# se integra facilmente com outras tecnologias, como o JavasScript (nesse caso utilizado no Vuetify 3) e outros bancos de dados. Além da api construída em C# fizemos uso da api [BrasilApi](https://brasilapi.com.br/docs#tag/CEP-V2) para retornar os dados da rua e cidade pelo cep inserido ao cadastrar o cliente no nosso sistema.

Optar por essas tecnologias em uma aplicação como essa possibilitou além da escalabilidade, visto que tanto o C# quanto o MySQL são capazes de lidar com grandes volumes de dados e usuários, tornando a aplicação escalável para atender às necessidades de um negócio em crescimento, a manutenibilidade, pois a organização do código em camadas facilita a manutenção e o desenavolvimento de novas funcionalidades.

## 🔩Instalação e Execução
1. Clone este repositório e siga os próximos passos.

2. Uma vez com o diretório devidamente aberto no VSCode ou no seu editor de código preferido, acesse o diretório da API
```
cd outfit-track-master
cd .\outfit-track-master\OutfitTrack.Api\
```

3. Uma dentro do diretório indicado, para rodar a API em ambiente de desenvolvimento, digite o seguinte comando
```
dotnet run
```
4. Agora para rodar o frontend da aplicação, abra um novo terminal vá até o diretório da aplicação
```
cd vuetify-project
cd vuetify-project\condicional-sales 
``` 
5. Uma vez no diretório indicado digite o comando para rodar a aplicação
```
npm run dev
```
6. Agora é só clicar no link `http://localhost:3000/` para abrir a aplicação na web.

##### 📌OSERVAÇÕES:
Lembre-se de ter instalado em sua máquina o MySQL para que a aplicação possa funcionar adequadamente. E estar com ele ativo pois do contrário as alterações feitas não serão salvas.

Caso você encontre problemas para executar a aplicação, instale o .NET e o Vue (vuetify) na sua máquina. 👇🏼
- [.NET Framework](https://dotnet.microsoft.com/pt-br/download/dotnet-framework)

- [VUE.js](https://vuejs.org/)

- [Vuetify 3](https://vuetifyjs.com/en/getting-started/installation/)

## ⚙️Funcionalidades
- Cadastro de clientes;
- Cadastro de produtos;
- Gerenciar clientes cadastrados - editar ou deletar;
- Gerenciar produtos cadastrados - editar ou deletar;
- Criação de condicional, onde ao entrar com o id do cliente já é retornado seus dados no formulário e o mesmo vale para o id dos produtos em condicionais;
- Controle de condincionais, sendo possível editar (categorizando cada peça em: comprada, pendente e devolvida), modificar o produto caso o cliente solicite e posteriormente alterar o status da condicional de acordo com a situação (pendente, aguardando encerramento e fechada).

## 📸Capturas de Tela
![imagem tela inicial do projeto](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-03%20222638.png)
![imagem tela cadastro de produto](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-04%20143618.png)
![imagem tela criar condicional](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-04%20143704.png)
![imagem tela cadastro de cliente](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-04%20143718.png)
![imagem tela lista de clientes](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-04%20143730.png)
![imagem tela lista de produtos](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-04%20143634.png)
![imagem tela histórico de condicionais](https://github.com/jessieFerrS/Projeto-OutifitTracker/blob/main/PROJETO%20OUTFITTRACKER/vuetify-project/Capturas%20OutifitTracker/Captura%20de%20tela%202024-12-04%20143821.png)


## 🤜🏼🤛🏼Equipe
- [**Caroline Ferreira**](https://github.com/CarolFerr) - *Frontend*
- [**Danielle Barros**](https://github.com/daniellebassetto/) - *Backend*
- [**Jéssica Ferreira**](https://github.com/jessieFerrS)- *Frontend*


## ®️Licença
This project is under the MIT license.
