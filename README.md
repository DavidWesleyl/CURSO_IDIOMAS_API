Criação de uma Web API para um empresa que oferece curso de idiomas
#
A solução possui as seguintes informações:
- Aluno( Nome, CPF, Turma)
- Turma (Código, Nivel)

  A API tem os seguintes EndPoints:

  CRUD de Aluno  

- Cadastro  

- Edição  

- Listagem  

- Exclusão 

 

CRUD de Turma 

- Cadastro  

- Listagem  

- Exclusão 

# 

Algumas Funcionalidades da API:


Restringe o cadastro de aluno repetido (pelo CPF) 

Garante que no cadastro do aluno, ele esteja sendo matriculado em uma turma; 

Permite o mesmo aluno ser matriculado em várias turmas diferentes, porém restringe matrícula repetida na mesma turma; 

Uma turma vai ter um número máximo de 5 alunos. Quando esse número for atingido, não deve ser permitido cadastrar mais nenhum aluno novo; 

Restringe exclusão de turma se ela possuir alunos;  

Utiliza princípios do SOLID 

  

