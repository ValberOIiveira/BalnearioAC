# Sistema de Gestão para o Balneário Águas Claras

## 📌 Visão Geral
Este projeto tem como objetivo modernizar a gestão do Balneário Águas Claras, oferecendo um sistema que permita controle eficiente de reservas, visitantes, vendas e produtos, além de um site funcional para presença digital.

O sistema será desenvolvido utilizando **ASP.NET Core** no backend, **Entity Framework** para a modelagem de dados e **PostgreSQL** como banco de dados. O front-end será construído com **HTML, CSS e JavaScript**.

## 🎯 Funcionalidades Principais

### 🔹 Gestão de Reservas
- Cadastro e controle de reservas para quiosques e churrasqueiras
- Associação das reservas a visitantes e funcionários responsáveis
- Histórico de reservas e status de pagamento

### 🔹 Controle de Visitantes e Funcionários
- Cadastro e gerenciamento de visitantes
- Login e controle de acesso para funcionários e administradores
- Painel de gestão para acompanhamento das operações

### 🔹 Controle de Vendas
- Registro de produtos vendidos no bar
- Relacionamento entre produtos, vendas e funcionários
- Relatórios de vendas e movimentação financeira

### 🔹 Presença Digital
- Página pública com galeria de imagens e vídeos do local
- Informações de localização e contato
- Sistema de agendamento online para visitantes

## 🛠️ Tecnologias Utilizadas
- **Back-end:** ASP.NET Core
- **Banco de Dados:** PostgreSQL (imagens armazenadas no tipo `bytea`)
- **Front-end:** HTML, CSS, JavaScript
- **ORM:** Entity Framework
- **Gerenciamento:** Trello para organização das tarefas

## 📌 Estrutura do Sistema

O sistema será dividido em três áreas principais:
- **Cliente (/cliente):** Galeria de fotos, localização e agendamento
- **Funcionário (/funcionario):** Área de atendimento e operações diárias
- **Gestor (/gestor):** Painel administrativo com relatórios e gerenciamento

## 📋 Modelagem do Banco de Dados
O sistema terá pelo menos 8 tabelas principais:
- **Usuarios:** Dados de login e permissões
- **Quiosques:** Controle dos espaços disponíveis
- **Reservas:** Registro de reservas associadas a visitantes
- **Produtos:** Cardápio do bar com preços
- **Visitantes:** Cadastro de clientes
- **Funcionarios:** Controle de funcionários do balneário
- **Vendas:** Registro de transações e produtos vendidos
- **Relatorios:** Dados consolidados para análise

## 📅 Cronograma
- **09 de abril:** Início do desenvolvimento
- **16 de abril:** Entrega final do projeto

## 📌 Organização do Projeto
- O código-fonte será disponibilizado no **GitHub**
- As tarefas e progresso serão gerenciados no **Trello**
- A apresentação final incluirá explicações técnicas e visuais do sistema

---
Este sistema proporcionará maior controle operacional e melhor experiência para os visitantes, modernizando a gestão do Balneário Águas Claras.

### Responsáveis

<div style="display: flex; gap: 10px;">
  <a href="https://github.com/ArthurDuGuedes">
    <img src="https://github.com/ArthurDuGuedes.png" alt="Arthur Guedes" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>
  <a href="https://github.com/AM-Maidana">
    <img src="https://github.com/AM-Maidana.png" alt="Amanda Maidana" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>
  <a href="https://github.com/Kaua676 ">
    <img src="https://github.com/Kaua676.png" alt="Kauan" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>
  <a href="https://github.com/Leandro-Oli2">
    <img src="https://github.com/Leandro-Oli2.png" alt="Leandro Oliveira" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>
  <a href="https://github.com/RhyanSKomm">
    <img src="https://github.com/RhyanSKomm.png" alt="Rhyan" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>
  <a href="https://github.com/rodrigo15511 ">
    <img src="https://github.com/rodrigo15511.png" alt="Rodrigo" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>
  <a href="https://github.com/ValberOIiveira ">
    <img src="https://github.com/ValberOIiveira.png" alt="Valber" style="border-radius: 50%; width: 60px; height: 60px; margin: 10%">
  </a>